using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Identity;
using Mublog.Server.Infrastructure.Services.Interfaces;
using Mublog.Server.PublicApi.Common.Helpers;
using Mublog.Server.PublicApi.V1.DTOs.Accounts;

namespace Mublog.Server.PublicApi.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/accounts")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IProfileRepository _profileRepo;
        private readonly ICurrentUserService _currentUserService;

        public AccountsController
            (UserManager<ApplicationUser> userManager,
            IJwtService jwtService,
            IProfileRepository profileRepo,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _profileRepo = profileRepo;
            _currentUserService = currentUserService;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(ResponseWrapper.Error("Invalid Credentials"));
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var token = _jwtService.GetTokenString(user.UserName, user.Email, claims);

            return Ok(ResponseWrapper.Success(new { accessToken = token} ));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseWrapper.Error("Invalid request"));
            }

            request.Username = request.Username.ToLower();
            
            // TODO Create user service

            var existingUsername = await _userManager.FindByNameAsync(request.Username);
            var existingMail = await _userManager.FindByEmailAsync(request.Email);

            if (existingUsername != null)
            {
                return BadRequest(ResponseWrapper.Success($"Username {request.Username} is already taken"));
            }

            if (existingMail != null)
            {
                return BadRequest(ResponseWrapper.Success($"Email {request.Email} is already in use."));
            }
            
            var profile = new Profile
            {
                Username = request.Username,
                DisplayName = request.DisplayName
            };
            
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var profileSuccess = await _profileRepo.AddAsync(profile);
            var identityResult = await _userManager.CreateAsync(user, request.Password);

            if (!profileSuccess)
            {
                await _userManager.DeleteAsync(user);
                return StatusCode(500, ResponseWrapper.Error("Error creating profile"));
            }

            if (!identityResult.Succeeded)
            {
                await _profileRepo.Remove(profile);
                return StatusCode(500, identityResult.Errors.Select(e => (e.ToString() ?? string.Empty).ToList()));
            }
            
            return Ok(ResponseWrapper.Success("Account successfully created."));
        }

        [HttpGet("token")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            var username = _currentUserService.GetUsername();
            var user = await _currentUserService.GetIdentity();

            if (user == null)
            {
                return BadRequest(ResponseWrapper.Error($"Account for {username} does not exist."));
            }
            
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = userRoles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var token = _jwtService.GetTokenString(user.UserName, user.Email, claims);
            
            return Ok(ResponseWrapper.Success(new { accessToken = token} ));
        }

        [HttpPatch("displayname")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeDisplayName([FromBody] ChangeDisplayNameRequestDto request)
        {
            var username = _currentUserService.GetUsername();
            var profile = await _currentUserService.GetProfile();

            if (profile == null)
            {
                return BadRequest(ResponseWrapper.Error($"Account for {username} does not exist."));
            }

            profile.DisplayName = request.DisplayName;

            var success = await _profileRepo.Update(profile);

            if (!success)
            {
                return StatusCode(500,
                    ResponseWrapper.Error("An error occured while pushing the changed to the database."));
            }

            return Ok(ResponseWrapper.Success("Display Name was successfully updated."));
        }

        [HttpPatch("email")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequestDto request)
        {
            return BadRequest("Method not implemented yet");
            
            var username = _currentUserService.GetUsername();
            var user = await _currentUserService.GetProfile();

            if (user == null)
            {
                return BadRequest($"Account for {username} does not exist.");
            }
            
            // var result = await _userManager.ChangeEmailAsync(user, request.Email)
        }

        [HttpPatch("password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var user = await _currentUserService.GetIdentity();

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword1, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(ResponseWrapper.Error(errors));
            }

            return Ok(ResponseWrapper.Success("Password has been changed."));
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveAccount()
        {
            var user = await _currentUserService.GetIdentity();
            var profile = await _currentUserService.GetProfile();
            var username = _currentUserService.GetUsername();

            if (user == null || profile == null)
            {
                return BadRequest($"Account for {username} does not exist.");
            }

            var identityResult = await _userManager.DeleteAsync(user);
            var profileResult = await _profileRepo.Remove(profile);

            if (!profileResult)
            {
                return StatusCode(500,
                    ResponseWrapper.Error("An error occured removing the profile from the database"));
            }

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description);

                return BadRequest(ResponseWrapper.Error(errors));
            }

            return Ok(ResponseWrapper.Success($"Successfully deleted account for {username}."));
        }
        
    }
}