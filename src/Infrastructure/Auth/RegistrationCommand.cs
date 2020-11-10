using MediatR;
using Mublog.Server.Domain.Common;

namespace Mublog.Server.Infrastructure.Auth
{
    public class RegistrationCommand : IRequest<BaseResponse>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public RegistrationCommand(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
}