using MediatR;

namespace Mublog.Server.Infrastructure.Auth
{
    public class RegistrationCommand : IRequest<RegistrationRequest> { }
}