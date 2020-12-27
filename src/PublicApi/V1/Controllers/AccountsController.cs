using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IAccountManager _accountManager;
        private readonly IJwtService _jwtService;
        private readonly IProfileRepository _profileRepo;
        private readonly ICurrentUserService _currentUserService;

        public AccountsController
        (
            IAccountManager accountManager,
            IJwtService jwtService,
            IProfileRepository profileRepo,
            ICurrentUserService currentUserService
        )
        {
            _accountManager = accountManager;
            _jwtService = jwtService;
            _profileRepo = profileRepo;
            _currentUserService = currentUserService;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var account = await _accountManager.FindByUsername(request.Username);

            if (account == null || !await _accountManager.ValidatePasswordCorrect(account, request.Password))
            {
                return Unauthorized(ResponseWrapper.Error("Invalid Credentials"));
            }
            
            var token = _jwtService.GetTokenString(account.Profile.Username, account.ProfileId, (int)account.Id, account.Email);

            return Ok(ResponseWrapper.Success(new {accessToken = token}));
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

            var existingUsername = await _accountManager.FindByUsername(request.Username);
            var existingMail = await _accountManager.FindByEmail(request.Email);

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

            var user = new Account
            {
                Email = request.Email,
            };

            var id = await _profileRepo.AddAsync(profile);
            var result = await _accountManager.Create(user, request.Password);

            if (id == default)
            {
                await _accountManager.Remove(user);
                return StatusCode(500, ResponseWrapper.Error("Error creating profile"));
            }

            if (!result)
            {
                await _profileRepo.Remove(profile);
                return StatusCode(500, "An error occured while trying to create the account.");
            }

            return Ok(ResponseWrapper.Success("Account successfully created."));
        }

        [HttpGet("token")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            var currentUser = _currentUserService.Get();
            var user = await _currentUserService.GetAccount();

            if (user == null)
            {
                return BadRequest(ResponseWrapper.Error($"Account for {currentUser} does not exist."));
            }

            var token = _jwtService.GetTokenString(user.Profile.Username, user.ProfileId, user.Id, user.Email);

            return Ok(ResponseWrapper.Success(new {accessToken = token}));
        }

        [HttpPatch("displayname")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeDisplayName([FromBody] ChangeDisplayNameRequestDto request)
        {
            var currentUser = _currentUserService.Get();
            var profile = await _currentUserService.GetProfile();

            if (profile == null)
            {
                return BadRequest(ResponseWrapper.Error($"Account for {currentUser} does not exist."));
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

            var currentUser = _currentUserService.Get();
            var profile = await _currentUserService.GetProfile();

            if (profile == null)
            {
                return BadRequest($"Account for {currentUser} does not exist.");
            }

            // var result = await _userManager.ChangeEmailAsync(user, request.Email)
        }

        [HttpPatch("password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var account = await _currentUserService.GetAccount();

            var success = await _accountManager.ChangePassword(account, request.CurrentPassword1, request.NewPassword);

            if (!success)
            {
                return BadRequest(ResponseWrapper.Error("An error occured while trying to change your password."));
            }

            return Ok(ResponseWrapper.Success("Password has been changed."));
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveAccount()
        {
            var account = await _currentUserService.GetAccount();
            var profile = await _currentUserService.GetProfile();
            var currentUser = _currentUserService.Get();

            if (account == null || profile == null)
            {
                return BadRequest($"Account for {currentUser} does not exist.");
            }

            // Change to account removal SQL transaction
            
            var accountSuccess = await _accountManager.Remove(account);
            var profileSuccess = await _profileRepo.Remove(profile);

            if (!profileSuccess)
            {
                return StatusCode(500,
                    ResponseWrapper.Error("An error occured when trying to remove the profile from the database"));
            }

            if (!accountSuccess)
            {
                return StatusCode(500,ResponseWrapper.Error("An error occured when trying to remove the account from the database"));
            }

            return Ok(ResponseWrapper.Success($"Successfully deleted account for {currentUser}."));
        }
    }
}