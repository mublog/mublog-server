using Dapper.FluentMap.Mapping;
using Mublog.Server.Infrastructure.Data.TransferEntities;

namespace Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper
{
    public class TransferProfileMap : EntityMap<TransferProfile>
    {
        public TransferProfileMap()
        {
            Map(p => p.Id).ToColumn("id");
            Map(p => p.CreatedDate).ToColumn("date_created");
            Map(p => p.UpdatedDate).ToColumn("date_updated");
            Map(p => p.Username).ToColumn("username");
            Map(p => p.DisplayName).ToColumn("display_name");
            Map(p => p.Description).ToColumn("description");
            Map(p => p.ProfileImageId).ToColumn("profile_image_id");
            Map(p => p.UserState).ToColumn("user_state");
            Map(p => p.ProfileImagePublicId).ToColumn("profile_image_public_id");
        }
    }
}