using Dapper.FluentMap.Mapping;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper
{
    public class PostMap : EntityMap<Post>
    {
        public PostMap()
        {
            Map(p => p.Id).ToColumn("id");
            Map(p => p.CreatedDate).ToColumn("date_created");
            Map(p => p.UpdatedDate).ToColumn("date_updated");
            Map(p => p.PublicId).ToColumn("public_id");
            Map(p => p.Content).ToColumn("content");
            Map(p => p.OwnerId).ToColumn("owner_id");
            Map(p => p.PostEditedDate).ToColumn("date_post_edited");
        }
    }
}