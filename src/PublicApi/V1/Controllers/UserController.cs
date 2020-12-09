using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.PublicApi.Common.Helpers;
using Mublog.Server.PublicApi.V1.DTOs.Users;

namespace Mublog.Server.PublicApi.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/users")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UserController : ControllerBase
    {
        private readonly IProfileRepository _profileRepo;

        public UserController(IProfileRepository profileRepo)
        {
            _profileRepo = profileRepo;
        }
        
        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser([FromRoute] string username)
        {
            username = username.ToLower();
            
            var profile = await _profileRepo.GetByUsername(username);

            if (profile == null)
            {
                return NotFound(ResponseWrapper.Error($"User {username} does not exist."));
            }
            
            var response = new UserResponseDto
            {
                Username = profile.Username,
                DisplayName = profile.DisplayName,
                Description = "",
                ProfileImageId = "",
                HeaderImageId = "",
                FollowersCount = 0,
                FollowingCount = 0
            };

            return Ok(ResponseWrapper.Success(response));
        }

        [HttpPost("follow/{username}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FollowUser([FromRoute] string username)
        {
            return BadRequest(ResponseWrapper.Error("Method not implemented yet"));
        }
        
        [HttpDelete("follow/{username}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnfollowUser([FromRoute] string username)
        {
            return BadRequest(ResponseWrapper.Error("Method not implemented yet"));
        }
        
        [HttpGet("{username}/followers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFollowers([FromRoute] string username)
        {
            return BadRequest(ResponseWrapper.Error("Method not implemented yet"));
        }
        
        [HttpGet("{username}/following")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFollowing([FromRoute] string username)
        {
            return BadRequest(ResponseWrapper.Error("Method not implemented yet"));
        }
    }
}