using System.Collections.Generic;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<ICollection<Comment>> FindByPost(Post parentPost);
    }
}