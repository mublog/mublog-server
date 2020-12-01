using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class ProfileConfig : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("profiles");

            builder.HasKey(u => u.Id)
                .HasName("pk_profile_id");

            builder.Property(u => u.CreatedDate)
                .HasColumnName("date_created");

            builder.Property(u => u.UpdatedDate)
                .HasColumnName("date_updated");

            builder.Property(u => u.Username)
                .IsUnicode()
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("username");

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.DisplayName)
                .IsUnicode()
                .HasMaxLength(30)
                .IsRequired()
                .HasColumnName("display_name");

            builder.Property(u => u.ProfileImageId)
                .HasColumnName("profile_image_id");

            builder.HasOne(u => u.ProfileImage)
                .WithOne(pi => pi.Owner)
                .HasForeignKey<Profile>(u => u.ProfileImageId)
                .HasConstraintName("fk_profile_image_id");

            builder.Property(u => u.UserState)
                .HasDefaultValue(UserState.Active)
                .IsRequired()
                .HasColumnName("user_state");

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .HasConstraintName("fk_post_id");

            builder.HasMany(u => u.LikedPosts)
                .WithMany(p => p.Likes);

            builder.HasMany(u => u.Mediae)
                .WithOne(m => m.Owner)
                .HasForeignKey(m => m.OwnerId)
                .HasConstraintName("fk_post_image_id");
        }
    }
}