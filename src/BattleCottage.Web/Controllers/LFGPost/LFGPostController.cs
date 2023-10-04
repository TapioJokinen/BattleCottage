using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.LFGPosts;
using BattleCottage.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Web.Controllers.LFGPostController
{
    public class LFGPostController : APIControllerBase
    {
        private readonly ILFGPostService _LFGPostService;
        private readonly IUserRepository _userRepository;

        public LFGPostController(ILFGPostService LFGPostService, IUserRepository userRepository)
        {
            _LFGPostService = LFGPostService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/api/[controller]")]
        public async Task<IActionResult> CreateLFGPost([FromBody] LFGPostFormInput LFGPostFormInput)
        {
            var email = HttpContext.User.Identity?.Name;

            if (email == null)
            {
                return Unauthorized(new MessageResponse("Failed to authorize user."));
            }

            var user = await _userRepository.FindByEmailAsync(email);

            if (user == null)
            {
                return Unauthorized(new MessageResponse("Invalid credentials."));
            }

            var lfgPost = await _LFGPostService.CreateLFGPost(user, LFGPostFormInput);

            return Ok(new LFGPostDto(lfgPost));
        }
    }
}