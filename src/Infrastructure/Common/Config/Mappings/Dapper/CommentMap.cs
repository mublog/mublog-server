using Dapper.FluentMap.Mapping;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Common.Config.Mappings.Dapper
{
    public class CommentMap : EntityMap<Comment>
    {
        public CommentMap()
        {
            Map(c => c.Id).ToColumn("id");
            Map(c => c.CreatedDate).ToColumn("date_created");
            Map(c => c.UpdatedDate).ToColumn("date_updated");
            Map(c => c.Content).ToColumn("content");
            Map(c => c.ParentPostId).ToColumn("parent_post_id");
            Map(c => c.OwnerId).ToColumn("owner_id");
        }
    }
}