using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Infrastructure.Data.Repositories;

namespace Mublog.Server.Infrastructure.Identity
{
    public class AccountManager : BaseRepository, IAccountManager
    {
        public AccountManager(IDbConnection connection) : base(connection)
        {
        }
        
        public async Task<bool> Create(Account account, string password)
        {
            var sql = "INSERT INTO accounts (data_created, date_updated, email, password, profile_id) VALUES (@Date, @Date, @Email, crypt(@Password, gen_salt('bf')), @ProfileId) RETURNING id;";

            var id = await Connection.QueryFirstOrDefaultAsync<long>(sql, new  { Date = DateTime.UtcNow, Email = account.Email, Password = password, ProfileId = account.Profile.Id });

            return id != default;
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