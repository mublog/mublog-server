using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Infrastructure.Identity;

namespace Mublog.Server.Infrastructure.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string GetUsername();
        Task<Account> GetAccount();
        Task<Profile> GetProfile();
    }
}