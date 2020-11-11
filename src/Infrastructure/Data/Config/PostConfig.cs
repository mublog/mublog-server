using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}