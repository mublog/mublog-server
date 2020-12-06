using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.PublicApi.Common.DTOs;
using Mublog.Server.PublicApi.Common.DTOs.V1.Users;

namespace Mublog.Server.PublicApi.Controllers
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
            var profile = await _profileRepo.GetByUsername(username.ToLower());

            if (profile == null)
            {
                return NotFound(ResponseWrapper.Error($"User {username.ToLower()} does not exist."));
            }

            // var response = _mapper.Map<UserResponseDto>(profile);

            var response = new UserResponseDto
            {
                Username = profile.Username,
                DisplayName = profile.DisplayName,
                Description = "",
                ProfileImageId = Guid.NewGuid().ToString(),
                FollowersCount = 0,
                FollowingCount = 0
            };

            return Ok(ResponseWrapper.Success(response));
        }

        // [HttpGet("{username}/image")]
        // public async Task<IActionResult> GetProfileImage([FromRoute] string username)
        // {
        //     throw new NotImplementedException();
        // }
    }
}