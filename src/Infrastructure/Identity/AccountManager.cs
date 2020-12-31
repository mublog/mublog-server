using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Infrastructure.Data.Repositories;
using Mublog.Server.Infrastructure.Data.TransferEntities;

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
            var sql = "SELECT ac.id, ac.data_created, ac.date_updated, ac.email, pfl.username, pfl.id AS profile_id, pfl.display_name, m.public_id AS profile_image_public_id FROM accounts AS ac LEFT JOIN profiles pfl on ac.profile_id = pfl.id LEFT JOIN mediae m on pfl.id = m.owner_id WHERE pfl.username = @Username LIMIT 1;";

            var transfer = await Connection.QueryFirstOrDefaultAsync<TransferAccount>(sql, new {Username = username});

            return transfer.ToAccount();
        }

        public async Task<Account> FindByEmail(string email)
        {
            var sql = "SELECT ac.id, ac.data_created, ac.date_updated, ac.email, pfl.username, pfl.id AS profile_id, pfl.display_name, m.public_id AS profile_image_public_id FROM accounts AS ac LEFT JOIN profiles pfl on ac.profile_id = pfl.id LEFT JOIN mediae m on pfl.id = m.owner_id WHERE ac.email = @Email LIMIT 1; ";

            var transfer = await Connection.QueryFirstOrDefaultAsync<TransferAccount>(sql, new {Email = email});

            return transfer.ToAccount();
        }

        public async Task<Account> FindById(int id)
        {
            var sql = "SELECT ac.id, ac.data_created, ac.date_updated, ac.email, pfl.username, pfl.id AS profile_id, pfl.display_name, m.public_id AS profile_image_public_id FROM accounts AS ac LEFT JOIN profiles pfl on ac.profile_id = pfl.id LEFT JOIN mediae m on pfl.id = m.owner_id WHERE ac.id = @Id LIMIT 1; ";

            var transfer = await Connection.QueryFirstOrDefaultAsync<TransferAccount>(sql, new {Id = id});

            return transfer.ToAccount();
        }

        public async Task<Account> FindByProfile(Profile profile)
        {
            var sql = "SELECT ac.id, ac.data_created, ac.date_updated, ac.email, pfl.username, pfl.id AS profile_id, pfl.display_name, m.public_id AS profile_image_public_id FROM accounts AS ac LEFT JOIN profiles pfl on ac.profile_id = pfl.id LEFT JOIN mediae m on pfl.id = m.owner_id WHERE pfl.id = @ProfileId LIMIT 1; ";

            var transfer = await Connection.QueryFirstOrDefaultAsync<TransferAccount>(sql, new {ProfileId = profile.Id});

            return transfer.ToAccount();
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