using System;
using System.Linq;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IPostImageRepository : IRepository<PostImage>
    {
        Task<PostImage> GetByPublic(Guid id);
        IQueryable<PostImage> GetByOwner(User owner);
        Task<IQueryable<PostImage>> GetByPostAsync(Post post);
        Task<PostImage> GetByPostId(int postId);
    }
}