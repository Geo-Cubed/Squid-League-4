using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class GameModeConfiguration : IEntityTypeConfiguration<GameMode>
    {
        public void Configure(EntityTypeBuilder<GameMode> builder)
        {
            builder.ToTable("game_mode");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.ModeName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("mode_name");

            builder.Property(e => e.PicturePath)
                .HasMaxLength(2000)
                .HasColumnName("picture_path");
        }
    }
}
