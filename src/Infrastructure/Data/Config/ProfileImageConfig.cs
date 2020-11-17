using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class ProfileImageConfig : IEntityTypeConfiguration<ProfileImage>
    {
        public void Configure(EntityTypeBuilder<ProfileImage> builder)
        {
            builder.HasKey(pi => pi.Id)
                .HasName("id");

            builder.Property(pi => pi.CreatedDate)
                .HasColumnName("date_created");

            builder.Property(pi => pi.UpdatedDate)
                .HasColumnName("date_updated");

            builder.Property(pi => pi.PublicId)
                .IsRequired()
                .HasColumnName("public_id");

            builder.Property(pi => pi.MediaType)
                .IsRequired()
                .HasColumnName("media_type");
            
            builder.Property(pi => pi.OwnerId)
                .IsRequired()
                .HasColumnName("owner_id");

            builder.HasOne(pi => pi.Owner)
                .WithOne(u => u.ProfileImage)
                .HasForeignKey<ProfileImage>(pi => pi.OwnerId)
                .HasConstraintName("profile_image_owner");
        }
    }
}