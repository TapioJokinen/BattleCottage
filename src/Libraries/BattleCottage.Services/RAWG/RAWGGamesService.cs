using BattleCottage.Core;
using BattleCottage.Core.Entities;
using BattleCottage.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BattleCottage.Services.RAWG
{
    public class RAWGGamesService : IRAWGGamesService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IRepository<Game> _gameRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        // We run this background task once a week.
        private readonly int _delay = 60000 * 60 * 24 * 7;

        public RAWGGamesService(
            ILogger<RAWGGamesService> logger,
            IConfiguration configuration,
            IRepository<Game> gameRepository,
            IHttpClientFactory httpClientFactory
        )
        {
            _logger = logger;
            _configuration = configuration;
            _gameRepository = gameRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                _logger.LogInformation("Dev environment, do not run work.");
                return;
            }

            string gamesUrl = _configuration["RAWG:GamesUrl"] ?? throw new ArgumentException(nameof(gamesUrl));
            string apiKey = _configuration["RAWG:APIKey"] ?? throw new ArgumentException(nameof(apiKey));

            int pageNumber = 1;

            HttpClient client = _httpClientFactory.CreateClient();

            // This could also be done by fetching all the games first,
            // then filtering values which are not found in the database
            // and finally doing the writing operation. However I do not know how AddRange
            // works internally and therefore I'm scared that there might be issues when inserting
            // huge number of entitites. Hence we do database insert per page.
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("RAWGGamesService is working.");

                var games = new List<Game>();

                string url = $"{gamesUrl}?key={apiKey}&page={pageNumber}&page_size=700&platform=4&tags=7";

                _logger.LogInformation("Fetching page {PageNumber}.", pageNumber);

                HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    string stringResponse = await response.Content.ReadAsStringAsync(cancellationToken);

                    if (!string.IsNullOrEmpty(stringResponse))
                    {
                        RAWGGamesResult? result = JsonSerializer.Deserialize<RAWGGamesResult>(
                            stringResponse,
                            new JsonSerializerOptions() { PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance }
                        );

                        if (result != null && result.Results != null)
                        {
                            foreach (Result r in result.Results)
                            {
                                var g = new Game()
                                {
                                    Name = r.Name,
                                    BackgroundImage = r.BackgroundImage,
                                    DateAdded = DateTime.UtcNow,
                                    DateUpdated = DateTime.UtcNow,
                                };

                                games.Add(g);
                            }
                            pageNumber++;
                        }
                    }
                    else
                    {
                        // Break if for some reason the response is empty or null. This should never actually happen
                        // unless the RAWG API is broken.
                        break;
                    }
                }
                else
                {
                    // Break once we get 404 response from the RAWG API.
                    break;
                }

                _logger.LogInformation("Found {GamesCount} games.", games.Count);

                // These are all the game names we fetched with the RAWG API.
                IList<string> fetchedGameNames = games.Select(x => x.Name).Distinct().ToList();

                // Get all Games from the database which names are found in 'fetchedGameNames'.
                IList<Game>? gamesInDb = await _gameRepository.Filter(x => fetchedGameNames.Contains(x.Name));

                // If none of the games are found in the database write them all.
                if (gamesInDb == null)
                {
                    await _gameRepository.AddRangeAsync(games);
                }
                else
                {
                    // Get names of the Games in 'gamesInDb' list.
                    IList<string> gameNamesInDb =
                        gamesInDb != null ? gamesInDb.Select(x => x.Name).ToList() : new List<string>();

                    // Filter out all games which are not in the database yet.
                    List<Game> gamesNotInDb = games.Where(x => !gameNamesInDb.Contains(x.Name)).ToList();

                    await _gameRepository.AddRangeAsync(gamesNotInDb);
                }
                await _gameRepository.SaveChangesAsync();
            }

            await Task.Delay(_delay, cancellationToken);
        }
    }
}
