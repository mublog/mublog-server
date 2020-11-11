using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Entities;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id)
                .HasName("id");

            builder.Property(p => p.CreatedDate)
                .ValueGeneratedOnAdd()
                .HasColumnName("date_created");

            builder.Property(p => p.UpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("date_updated");

            builder.Property(p => p.PublicId)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("public_id");

            builder.Property(p => p.Content)
                .IsRequired()
                .HasColumnName("content");

            builder.Property(p => p.OwnerId)
                .IsRequired()
                .HasColumnName("owner_id");

            builder.HasOne(p => p.Owner)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.OwnerId);

            builder.HasMany(p => p.Mediae)
                .WithOne(m => m.ParentPost)
                .HasConstraintName("child_media_constraint");
        }
    }
}