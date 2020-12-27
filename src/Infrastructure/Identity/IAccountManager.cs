using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Identity
{
    public interface IAccountManager
    {
        Task<bool> Create(Account account, string password);
        Task<bool> Remove(Account account);
        Task<bool> Update(Account account);
        Task<Account> FindByUsername(string username);
        Task<Account> FindByEmail(string email);
        Task<Account> FindById(int id);
        Task<Account> FindByProfile(Profile profile);
        Task<bool> ValidatePasswordCorrect(Account account, string password);
        Task<bool> ChangePassword(Account account, string currentPassword, string newPassword);
    }
}