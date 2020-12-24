using Dapper.FluentMap.Mapping;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Mappings.Dapper
{
    public class ProfileMap : EntityMap<Profile>
    {
        public ProfileMap()
        {
            Map(p => p.Id).ToColumn("id");
            Map(p => p.CreatedDate).ToColumn("date_created");
            Map(p => p.UpdatedDate).ToColumn("date_updated");
            Map(p => p.Username).ToColumn("username");
            Map(p => p.DisplayName).ToColumn("display_name");
            Map(p => p.ProfileImageId).ToColumn("profile_image_id");
            Map(p => p.UserState).ToColumn("user_state");
        }
    }
}