using Dapper.FluentMap.Mapping;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Infrastructure.Data.TransferEntities;

namespace Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper
{
    public class TransferCommentMap : EntityMap<TransferComment>
    {
        public TransferCommentMap()
        {
            Map(c => c.Id).ToColumn("id");
            Map(c => c.CreatedDate).ToColumn("date_created");
            Map(c => c.UpdatedDate).ToColumn("date_updated");
            Map(c => c.Content).ToColumn("content");
            Map(c => c.ParentPostId).ToColumn("parent_post_id");
            Map(c => c.OwnerId).ToColumn("owner_id");
            Map(c => c.DisplayName).ToColumn("display_name");
            Map(c => c.Username).ToColumn("username");
            Map(c => c.ProfileImageId).ToColumn("profile_image_id");
            Map(c => c.ProfileImagePublicId).ToColumn("profile_image_public_id");
        }
    }
}