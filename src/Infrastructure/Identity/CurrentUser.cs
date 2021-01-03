using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Identity
{
    public class CurrentUser
    {
        public string Username { get; set; }
        public int AccountId { get; set; }
        public int ProfileId { get; set; }

        public override string ToString() => Username;

        public Profile ToProfile => new Profile { Id = ProfileId, Username = Username};

        public Account ToAccount => new Account { Id = AccountId, ProfileId = ProfileId, Profile = ToProfile};
    }
}