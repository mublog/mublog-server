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

        public AccountsController(UserManager<ApplicationUser> userManager, IJwtService jwtService, IProfileRepository profileRepo)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _profileRepo = profileRepo;
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

        [HttpPost("token/refresh")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}