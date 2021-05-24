using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class CasterProfileConfiguration : IEntityTypeConfiguration<CasterProfile>
    {
        public void Configure(EntityTypeBuilder<CasterProfile> builder)
        {
            builder.ToTable("caster_profile");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.CasterName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("caster_name");

            builder.Property(e => e.Discord)
                .HasMaxLength(255)
                .HasColumnName("discord");

            builder.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValueSql("'0'")
                .HasComment("If the caster is active or not");

            builder.Property(e => e.ProfilePicturePath)
                .HasMaxLength(2000)
                .HasColumnName("profile_picture_path");

            builder.Property(e => e.Twitch)
                .HasMaxLength(255)
                .HasColumnName("twitch");

            builder.Property(e => e.Twitter)
                .HasMaxLength(255)
                .HasColumnName("twitter");

            builder.Property(e => e.YouTube)
                .HasMaxLength(255)
                .HasColumnName("youtube");
        }
    }
}
