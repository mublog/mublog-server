using System;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Identity
{
    public class AccountManager : IAccountManager
    {
        public async Task<bool> AddAccount(Account account, string password)
        {
            var sql = $"";

            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAccount(Account account)
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

        public async Task<bool> CheckPasswordCorrect(Account account, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ChangePassword(Account account, string newPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}