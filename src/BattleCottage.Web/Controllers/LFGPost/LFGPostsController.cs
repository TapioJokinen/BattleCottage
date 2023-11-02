using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.LFGPosts;
using Microsoft.AspNetCore.Mvc;
using BattleCottage.Web.Dtos;
using Microsoft.AspNetCore.Authorization;
using BattleCottage.Core.Entities;

namespace BattleCottage.Web.Controllers.LFGPostController
{
    public class LFGPostsController : APIControllerBase
    {
        private readonly ILFGPostService _lfgPostService;
        private readonly IUserRepository _userRepository;

        public LFGPostsController(ILFGPostService lfgPostService, IUserRepository userRepository)
        {
            _lfgPostService = lfgPostService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/api/[controller]")]
        public async Task<IActionResult> CreateLFGPost([FromBody] LFGPostFormInput LFGPostFormInput)
        {
            string? email = HttpContext.User.Identity?.Name;

            User? user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return Unauthorized(new MessageResponse("Invalid credentials."));
            }

            var lfgPost = await _lfgPostService.CreateLFGPost(user, LFGPostFormInput);

            return Ok(new LFGPostDto(lfgPost));
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("/api/[controller]/options")]
        public async Task<IActionResult> GetLFGPostFormOptions()
        {
            var options = await _lfgPostService.GetLFGPostFormOptions();

            return Ok(LFGPostOptionsDto.FromLFGPostFormOptions(options));
        }
    }
}
