using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public override PagedList<Post> GetPaged(QueryParameters queryParameters)
        {
            return PagedList<Post>.ToPagedList(Query().AsNoTracking().Include(p => p.Owner),
                queryParameters.Page, queryParameters.Size);
        }

        public async Task<Post> GetByPublicId(int id)
        {
            return await Context.Posts.AsNoTracking().Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.PublicId == id);
        }

        public async Task<Post> GetByUserId(int id)
        {
            return await Context.Posts.FirstOrDefaultAsync(p => p.OwnerId == id);
        }
    }
}