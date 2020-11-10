using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Mublog.Server.Domain.Common;

namespace Mublog.Server.Infrastructure.Auth
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, BaseResponse>
    {
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationCommandHandler(ILogger logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<BaseResponse> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded) return await Task.FromResult(new  BaseResponse{ErrorMessage = "User registration was successful.", Error = false});

            var errors = "";

            foreach (var error in result.Errors)
            {
                errors += $"{error.Code}: {error.Description}\n";
            }

            return await Task.FromResult(new  BaseResponse{ErrorMessage = errors, Error = true});
        }
    }
}