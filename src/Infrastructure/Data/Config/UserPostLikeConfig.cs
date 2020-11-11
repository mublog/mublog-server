using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class UserPostLikeConfig : IEntityTypeConfiguration<UserPostLike>
    {
        public void Configure(EntityTypeBuilder<UserPostLike> builder)
        {
            builder.HasKey(upl => new {upl.PostId, upl.UserId})
                .HasName("user_post_like_id");

            builder.HasOne(upl => upl.Post)
                .WithMany(p => p.Likes)
                .
        }
    }
}