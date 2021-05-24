using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class GameMapConfiguration : IEntityTypeConfiguration<GameMap>
    {
        public void Configure(EntityTypeBuilder<GameMap> builder)
        {
            builder.ToTable("game_map");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.MapName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("map_name");

            builder.Property(e => e.PicturePath)
                .HasMaxLength(2000)
                .HasColumnName("picture_path");
        }
    }
}
