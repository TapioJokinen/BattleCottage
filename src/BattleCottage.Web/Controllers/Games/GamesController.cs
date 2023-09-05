using BattleCottage.Core.Entities;
using BattleCottage.Core.Pagination;
using BattleCottage.Services.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Web.Controllers.Games
{
    public class GamesController : APIControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/api/[controller]")]
        public async Task<IActionResult> AllGames([FromQuery] string? contains, int? page, int? pageSize)
        {

            if (string.IsNullOrEmpty(contains))
            {
                throw new NotImplementedException("Getting all games is not supported yet.");
            }

            ICollection<Game>? games = await _gameService.GetGamesWithNameLike(contains);

            if (games == null || games.Count == 0)
            {
                return NotFound(new MessageResponse("No games found."));
            }

            page ??= PageSettings.FirstPageNumber;
            pageSize ??= PageSettings.MaxPageSize;

            try
            {
                PagedCollection<Game> pagedGames = new(games, page, pageSize, Request);
                return Ok(new
                {
                    Next = pagedGames.GetNextUrl(),
                    Previous = pagedGames.GetPreviousUrl(),
                    pagedGames.Results
                });
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return NotFound(new MessageResponse("No games found."));
            }
        }
    }
}
