using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        protected PostRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Post> GetByPublicId(int id)
        {
            return await Context.Posts.FirstOrDefaultAsync(p => p.PublicId == id);
        }

        public async Task<Post> GetByUserId(int id)
        {
            return await Context.Posts.FirstOrDefaultAsync(p => p.OwnerId == id);
        }
    }
}