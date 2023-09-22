using BattleCottage.Core.Entities;
using BattleCottage.Core.Pagination;
using BattleCottage.Services.Games;
using BattleCottage.Web.Pagination;
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
        public async Task<IActionResult> AllGames([FromQuery] string? contains, string? page, string? pageSize)
        {
            ICollection<Game>? games;

            if (string.IsNullOrEmpty(contains))
            {
                games = await _gameService.GetAllGames();
            }
            else
            {
                games = await _gameService.GetGamesWithNameLike(contains);
            }

            if (games == null || games.Count == 0)
            {
                return NotFound(new MessageResponse("No games found."));
            }

            int pageNumber = string.IsNullOrEmpty(page) ? PageSettings.FirstPageNumber : int.Parse(page);
            int size = string.IsNullOrEmpty(pageSize) ? PageSettings.MaxPageSize : int.Parse(pageSize);

            try
            {
                PagedCollection<Game> pagedGames = new(games, pageNumber, size, Request);

                return Ok(pagedGames.Result);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return NotFound(new MessageResponse(PaginationErrors.InvalidPage));
            }
        }
    }
}
