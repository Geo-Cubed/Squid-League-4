using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class GameSettingConfiguration : IEntityTypeConfiguration<GameSetting>
    {
        public void Configure(EntityTypeBuilder<GameSetting> builder)
        {
            builder.ToTable("game_setting");

            builder.HasIndex(e => e.GameMapId, "FK_game_setting_game_map");

            builder.HasIndex(e => e.GameModeId, "FK_game_setting_game_mode");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.BracketStage)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("bracket_stage");

            builder.Property(e => e.GameMapId)
                .HasColumnType("int(11)")
                .HasColumnName("game_map_id");

            builder.Property(e => e.GameModeId)
                .HasColumnType("int(11)")
                .HasColumnName("game_mode_id");

            builder.Property(e => e.SortOrder)
                .HasColumnType("int(11)")
                .HasColumnName("sort_order");

            builder.HasOne(d => d.GameMap)
                .WithMany(p => p.GameSettings)
                .HasForeignKey(d => d.GameMapId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_game_setting_game_map");

            builder.HasOne(d => d.GameMode)
                .WithMany(p => p.GameSettings)
                .HasForeignKey(d => d.GameModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_game_setting_game_mode");
        }
    }
}
