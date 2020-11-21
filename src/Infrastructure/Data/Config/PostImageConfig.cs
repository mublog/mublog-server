using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class PostImageConfig : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.ToTable("post_image");

            builder.HasKey(m => m.Id)
                .HasName("id");

            builder.Property(m => m.CreatedDate)
                .HasColumnName("data_created");

            builder.Property(m => m.UpdatedDate)
                .HasColumnName("date_updated");

            builder.Property(m => m.PublicId)
                .IsRequired()
                .HasColumnName("public_id");

            builder.Property(m => m.MediaType)
                .IsRequired()
                .HasColumnName("media_type");

            builder.Property(m => m.OwnerId)
                .IsRequired()
                .HasColumnName("owner_id");

            builder.HasOne(m => m.Owner)
                .WithMany(u => u.Mediae)
                .HasForeignKey(m => m.OwnerId)
                .IsRequired()
                .HasConstraintName("owner_user");

            builder.Property(m => m.PostId)
                .HasColumnName("post_id");

            builder.HasOne(m => m.ParentPost)
                .WithMany(u => u.Mediae)
                .HasForeignKey(m => m.PostId)
                .HasConstraintName("parent_post_post");
        }
    }
}