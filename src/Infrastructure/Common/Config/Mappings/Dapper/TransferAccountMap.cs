using Dapper.FluentMap.Mapping;
using Mublog.Server.Infrastructure.Data.TransferEntities;

namespace Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper
{
    public class TransferAccountMap : EntityMap<TransferAccount>

    {
        public TransferAccountMap()
        {
            Map(a => a.Id).ToColumn("id");
            Map(a => a.CreatedDate).ToColumn("date_created");
            Map(a => a.UpdatedDate).ToColumn("date_updated");
            Map(a => a.Email).ToColumn("email");
            Map(a => a.ProfileId).ToColumn("profile_id");
            Map(a => a.Username).ToColumn("username");
            Map(a => a.DisplayName).ToColumn("display_name");
            Map(a => a.ProfileImagePublicId).ToColumn("profile_image_public_id");
        }
    }
}