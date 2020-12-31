using System;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Infrastructure.Identity;
using NpgsqlTypes;

namespace Mublog.Server.Infrastructure.Data.TransferEntities
{
    public class TransferAccount
    {
        public long Id { get; set; }
        public NpgsqlDateTime CreatedDate { get; set; }
        public NpgsqlDateTime UpdatedDate { get; set; }
        public string Email { get; set; }
        public long ProfileId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public Guid ProfileImagePublicId { get; set; }

        public Account ToAccount()
        {
            var media = new Media
            {
                PublicId = ProfileImagePublicId
            };

            var profile = new Profile
            {
                Id = ProfileId,
                Username = Username,
                DisplayName = DisplayName,
                ProfileImage = media
            };

            return new Account
            {
                Id = Id,
                CreatedDate = CreatedDate.ToDateTime(),
                UpdatedDate = UpdatedDate.ToDateTime(),
                Email = Email,
                Profile = profile
            };
        }
    }
}