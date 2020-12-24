using Dapper.FluentMap.Mapping;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Mappings.Dapper
{
    public class MediaMap : EntityMap<Media>
    {
        public MediaMap()
        {
            Map(m => m.Id).ToColumn("id");
            Map(m => m.CreatedDate).ToColumn("date_created");
            Map(m => m.UpdatedDate).ToColumn("date_updated");
            Map(m => m.PostId).ToColumn("post_id");
            Map(m => m.PublicId).ToColumn("public_id");
            Map(m => m.MediaType).ToColumn("media_type");
            Map(m => m.OwnerId).ToColumn("owner_id");
        }
    }
}