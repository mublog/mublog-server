namespace Mublog.Server.Domain.Data.Entities
{
    public class PostImage : BaseImageEntity
    {
        public int PostId { get; set; }
        public Post ParentPost { get; set; }
    }
}