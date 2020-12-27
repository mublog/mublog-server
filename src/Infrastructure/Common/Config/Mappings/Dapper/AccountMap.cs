using Dapper.FluentMap.Mapping;
using Mublog.Server.Infrastructure.Identity;

namespace Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper
{
    public class AccountMap : EntityMap<Account>
    {
        public AccountMap()
        {
            Map(a => a.Id).ToColumn("id");
            Map(a => a.CreatedDate).ToColumn("date_created");
            Map(a => a.UpdatedDate).ToColumn("date_updated");
            Map(a => a.Email).ToColumn("email");
            Map(a => a.ProfileId).ToColumn("profile_id");
        }
    }
}