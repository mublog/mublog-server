using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Data.TransferEntities;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<Comment> FindById(long id)
        {
            var sql = "SELECT c.id, c.date_created, c.date_updated, c.content, c.parent_post_id, c.owner_id, pfl.display_name, pfl.username, m.id AS profile_image_id, m.public_id AS profile_image_public_id FROM comments AS c LEFT JOIN profiles pfl on pfl.id = c.owner_id LEFT OUTER JOIN mediae m on pfl.profile_image_id = m.id WHERE c.id = @Id LIMIT 1;";

            var transferComment = await Connection.QueryFirstOrDefaultAsync<TransferComment>(sql, new {Id = id});
            
            return transferComment.ToComment();
        }

        public async Task<long> Create(Comment comment)
        {
            comment.ApplyTimestamps();
            
            var sql = "INSERT INTO comments (date_created, date_updated, content, parent_post_id, owner_id) VALUES (@CreatedDate, @UpdatedDate, @Content, @ParentPostId, @OwnerId) RETURNING id;";

            var id = await Connection.QueryFirstOrDefaultAsync<long>(sql, comment);

            return id;
        }

        public async Task<bool> Remove(Comment comment)
        {
            var sql = "DELETE FROM comments WHERE id = @Id;";

            var rowsAffected = await Connection.ExecuteAsync(sql, comment);

            return rowsAffected >= 1;
        }

        public async Task<ICollection<Comment>> FindByPost(Post parentPost)
        {
            var sql = "SELECT c.id, c.date_created, c.date_updated, c.content, c.parent_post_id, c.owner_id, pfl.display_name, pfl.username, m.id AS profile_image_id, m.public_id AS profile_image_public_id FROM comments AS c LEFT JOIN profiles pfl on pfl.id = c.owner_id LEFT OUTER JOIN mediae m on pfl.profile_image_id = m.id WHERE c.parent_post_id = @PostId ORDER BY c.date_created DESC;";

            var transferComments = await Connection.QueryAsync<TransferComment>(sql, new {PostId = parentPost.Id});
            
            return transferComments.Select(c => c.ToComment()).ToList();
        }
    }
}