using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Services.Interfaces;
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
        private readonly ICurrentUserService _currentUserService;

        public UserController(IProfileRepository profileRepo, ICurrentUserService currentUserService)
        {
            _profileRepo = profileRepo;
            _currentUserService = currentUserService;
        }
        
        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser([FromRoute] string username)
        {
            username = username.ToLower();
            
            var profile = await _profileRepo.FindByUsername(username);

            if (profile == null)
            {
                return NotFound(ResponseWrapper.Error($"User {username} does not exist."));
            }
            
            var response = new FullUserResponseDto
            {
                Username = profile.Username,
                DisplayName = profile.DisplayName,
                Description = "",
                ProfileImageId = "",
                HeaderImageId = "",
                FollowersCount = 0,
                FollowingCount = 0,
                FollowingStatus = false
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
            username = username.ToLower();
            var followingProfile = await _profileRepo.FindByUsername(username);
            
            var currentUser = _currentUserService.Get();

            if (followingProfile == null)
            {
                return NotFound(ResponseWrapper.Error($"User {username} does not exist."));
            }

            var success = await _profileRepo.AddFollowing(followingProfile, currentUser.ToProfile);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error($"An error occured while trying to follow user {followingProfile.Username}."));
            }

            return Ok(ResponseWrapper.Success());
        }
        
        [HttpDelete("follow/{username}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnfollowUser([FromRoute] string username)
        {
            username = username.ToLower();
            var followingProfile = await _profileRepo.FindByUsername(username);
            
            var currentUser = _currentUserService.Get();

            if (followingProfile == null)
            {
                return NotFound(ResponseWrapper.Error($"User {username} does not exist."));
            }

            var success = await _profileRepo.RemoveFollowing(followingProfile, currentUser.ToProfile);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error($"An error occured while trying to unfollow user {followingProfile.Username}."));
            }

            return Ok(ResponseWrapper.Success());
        }
        
        [HttpGet("{username}/followers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFollowers([FromRoute] string username)
        {
            username = username.ToLower();

            var profile = await _profileRepo.FindByUsername(username);

            if (profile == null)
            {
                return NotFound(ResponseWrapper.Error($"User {username} was not found."));
            }

            var followers = await _profileRepo.GetFollowers(profile);

            var response = followers.Select(p => new ShortUserResponseDto
            {
                DisplayName = p.DisplayName, 
                Username = p.Username, 
                ProfileImageId = p.ProfileImage?.PublicId.ToString()
            });
            
            return Ok(ResponseWrapper.Success(response));
        }
        
        [HttpGet("{username}/following")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFollowing([FromRoute] string username)
        {
            username = username.ToLower();
             
            var profile = await _profileRepo.FindByUsername(username);

            if (profile == null)
            {
                return NotFound(ResponseWrapper.Error($"User {username} was not found."));
            }

            var following = await _profileRepo.GetFollowing(profile);

            var response = following.Select(p => new ShortUserResponseDto
            {
                DisplayName = p.DisplayName, 
                Username = p.Username, 
                ProfileImageId = p.ProfileImage?.PublicId.ToString()
            });
            
            return Ok(ResponseWrapper.Success(response));
        }
    }
}