using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mublog.Server.Domain.Entities;
using Mublog.Server.Domain.Enums;

namespace Mublog.Server.Infrastructure.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id)
                .HasName("id");

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

            builder.Navigation(u => u.ProfileImage)
                .UsePropertyAccessMode(PropertyAccessMode.PreferProperty)
                .HasField("profile_image_id");
            
            // builder.HasOne(u => u.ProfileImage)
            //     .WithOne(m => m.Owner)
            //     .HasForeignKey<User>(u => u.ProfileImageId)
            //     .HasConstraintName("profile_image_media");

            builder.Property(u => u.HeaderImageId)
                .HasColumnName("header_image_id");

            builder.Navigation(u => u.HeaderImage)
                .UsePropertyAccessMode(PropertyAccessMode.PreferProperty)
                .HasField("header_image_id");
            
            // builder.HasOne(u => u.HeaderImage)
            //     .WithOne(m => m.Owner)
            //     .HasForeignKey<User>(u => u.HeaderImageId)
            //     .HasConstraintName("header_image_media");

            builder.Property(u => u.UserState)
                .HasDefaultValue(UserState.Active)
                .IsRequired()
                .HasColumnName("user_state");

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .HasConstraintName("posts_post_owner");

            builder.HasMany(u => u.LikedPosts)
                .WithMany(p => p.Likes);

            builder.HasMany(u => u.Mediae)
                .WithOne(m => m.Owner)
                .HasForeignKey(m => m.OwnerId)
                .HasConstraintName("mediae_media_owner");
        }
    }
}