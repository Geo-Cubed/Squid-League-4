using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("game");

            builder.HasIndex(e => e.MatchId, "FK_game_set_match");

            builder.HasIndex(e => e.GameSettingId, "FK_game_setting");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.AwayTeamScore).HasColumnName("away_team_score");

            builder.Property(e => e.GameSettingId)
                .HasColumnType("int(11)")
                .HasColumnName("game_setting_id");

            builder.Property(e => e.HomeTeamScore).HasColumnName("home_team_score");

            builder.Property(e => e.MatchId)
                .HasColumnType("int(11)")
                .HasColumnName("match_id");

            builder.HasOne(d => d.GameSetting)
                .WithMany(p => p.Games)
                .HasForeignKey(d => d.GameSettingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_game_setting");

            builder.HasOne(d => d.Match)
                .WithMany(p => p.Games)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_game_set_match");
        }
    }
}
