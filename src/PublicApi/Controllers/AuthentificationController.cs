using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mublog.Server.PublicApi.DTOs.V1.Requests;
using Mublog.Server.PublicApi.Validators.Interfaces;

namespace Mublog.Server.PublicApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailValidator _emailValidator;


        public AuthController
        (
            ILogger<AuthController> logger,
            UserManager<IdentityUser> userManager,
            IEmailValidator emailValidator
        )
        {
            _logger = logger;
            _userManager = userManager;
            _emailValidator = emailValidator;
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
        {
            if (!_emailValidator.IsValid(request.Email))
            {
                ModelState.AddModelError("", $"The email address is not invalid {request.Email}");
                return BadRequest(ModelState);
            }

            
            
            // await _userManager.CreateAsync();

            return Ok();
        }
    }
}