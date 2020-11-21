using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;

namespace Mublog.Server.Infrastructure.Data.Repositories
{
    public class PostImageRepository : Repository<PostImage>, IPostImageRepository
    {
        protected PostImageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PostImage> GetByPublic(Guid id)
        {
            return await Context.PostImages.FirstOrDefaultAsync(m => m.PublicId == id);
        }

        public IQueryable<PostImage> GetByOwner(Profile owner)
        {
            return Context.PostImages.Where(m => m.Owner == owner);
        }

        public IQueryable<PostImage> GetByPost(Post post)
        {
            return Context.PostImages.Where(pi => pi.PostId == post.Id);
        }

        public async Task<PostImage> GetByPostId(int postId)
        {
            return await Context.PostImages.FirstOrDefaultAsync(m => m.PostId == postId);
        }
    }
}