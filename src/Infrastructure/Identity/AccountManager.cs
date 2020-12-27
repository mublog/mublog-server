using System;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Identity
{
    public class AccountManager : IAccountManager
    {
        public async Task<bool> Create(Account account, string password)
        {
            var sql = $"";

            throw new NotImplementedException();
        }

        public async Task<bool> Remove(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Account account)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> FindByUsername(string username)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> FindByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Account> FindByProfile(Profile profile)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ValidatePasswordCorrect(Account account, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ChangePassword(Account account, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}