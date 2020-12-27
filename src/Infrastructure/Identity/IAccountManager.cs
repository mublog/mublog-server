using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Identity
{
    public interface IAccountManager
    {
        Task<bool> AddAccount(Account account, string password);
        Task<bool> DeleteAccount(Account account);
        Task<bool> UpdateAccount(Account account);
        Task<Account> FindByUsername(string username);
        Task<Account> FindByEmail(string email);
        Task<Account> FindById(int id);
        Task<Account> FindByProfile(Profile profile);
        Task<bool> CheckPasswordCorrect(Account account, string password);
        Task<bool> ChangePassword(Account account, string newPassword);
    }
}