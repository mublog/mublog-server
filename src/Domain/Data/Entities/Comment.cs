namespace Mublog.Server.Domain.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public long ParentPostId { get; set; }
        public Post ParentPost { get; set; }
        public int OwnerId { get; set; }
        public Profile Owner { get; set; }
    }
}