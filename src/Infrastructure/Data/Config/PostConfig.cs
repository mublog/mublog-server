using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("posts");

            builder.HasKey(p => p.Id)
                .HasName("id");

            builder.Property(p => p.CreatedDate)
                .HasColumnName("date_created");

            builder.Property(p => p.UpdatedDate)
                .HasColumnName("date_updated");

            builder.Property(p => p.PublicId)
                .UseIdentityByDefaultColumn()
                .IsRequired()
                .HasColumnName("public_id");

            builder.Property(p => p.Content)
                .IsUnicode()
                .IsRequired()
                .HasColumnName("content");

            builder.Property(p => p.PostEditedDate)
                .HasColumnName("date_post_edited");

            builder.Property(p => p.OwnerId)
                .IsRequired()
                .HasColumnName("owner_id");

            builder.HasOne(p => p.Owner)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.OwnerId)
                .HasConstraintName("post_media_owner");

            builder.HasMany(p => p.Mediae)
                .WithOne(m => m.ParentPost)
                .HasForeignKey(m => m.PostId)
                .HasConstraintName("child_media_constraint");

            builder.HasMany(p => p.Likes)
                .WithMany(u => u.LikedPosts);
        }
    }
}