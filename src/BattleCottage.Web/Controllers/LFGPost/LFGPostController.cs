using BattleCottage.Data.Repositories.UserRepository;
using BattleCottage.Services.LfgPosts;
using BattleCottage.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BattleCottage.Web.Controllers.LfgPostController
{
    public class LfgPostController : APIControllerBase
    {
        private readonly ILfgPostService _LfgPostService;
        private readonly IUserRepository _userRepository;

        public LfgPostController(ILfgPostService LfgPostService, IUserRepository userRepository)
        {
            _LfgPostService = LfgPostService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/api/[controller]")]
        public async Task<IActionResult> CreateLfgPost([FromBody] LfgPostFormInput LfgPostFormInput)
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

            var lfgPost = await _LfgPostService.CreateLfgPost(user, LfgPostFormInput);

            return Ok(new LfgPostDto(lfgPost));
        }
    }
}