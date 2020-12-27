using Dapper.FluentMap.Mapping;
using Mublog.Server.Infrastructure.Common.Helpers;

namespace Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper
{
    public class PGPostEntityMap : EntityMap<PGPostEntity>
    {
        public PGPostEntityMap()
        {
            Map(p => p.Id).ToColumn("id");
            Map(p => p.CreatedDate).ToColumn("date_created");
            Map(p => p.UpdatedDate).ToColumn("date_updated");
            Map(p => p.PublicId).ToColumn("public_id");
            Map(p => p.Content).ToColumn("content");
            Map(p => p.OwnerId).ToColumn("owner_id");
            Map(p => p.PostEditedDate).ToColumn("date_post_edited");
            Map(p => p.Username).ToColumn("username");
            Map(p => p.DisplayName).ToColumn("display_name");
            Map(p => p.ProfileImagePublicId).ToColumn("profile_image_id");
            Map(p => p.Liked).ToColumn("liked");
            Map(p => p.LikesCount).ToColumn("likes_amount");
        }
    }
}