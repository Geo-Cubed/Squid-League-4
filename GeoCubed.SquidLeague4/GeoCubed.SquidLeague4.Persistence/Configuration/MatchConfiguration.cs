using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("match");

            builder.HasIndex(e => e.AwayTeamId, "FK_match_away_team");

            builder.HasIndex(e => e.CasterProfileId, "FK_match_caster_profile");

            builder.HasIndex(e => e.HomeTeamId, "FK_match_home_team");

            builder.HasIndex(e => e.SecondaryCasterProfileId, "FK_match_second_caster_profile");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.AwayTeamId)
                .HasColumnType("int(11)")
                .HasColumnName("away_team_id");

            builder.Property(e => e.AwayTeamScore)
                .HasColumnType("int(11)")
                .HasColumnName("away_team_score")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.CasterProfileId)
                .HasColumnType("int(11)")
                .HasColumnName("caster_profile_id");

            builder.Property(e => e.HomeTeamId)
                .HasColumnType("int(11)")
                .HasColumnName("home_team_id");

            builder.Property(e => e.HomeTeamScore)
                .HasColumnType("int(11)")
                .HasColumnName("home_team_score")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.MatchVodLink)
                .HasMaxLength(2000)
                .HasColumnName("match_vod_link");

            builder.Property(e => e.MatchDate)
                .HasColumnType("datetime")
                .HasColumnName("match_date");

            builder.Property(e => e.SecondaryCasterProfileId)
                .HasColumnType("int(11)")
                .HasColumnName("secondary_caster_profile_id")
                .HasComment("Id of the secondary caster");

            builder.Property(e => e.Winner)
                .HasColumnType("enum('home', 'away', 'none')")
                .HasColumnName("winner");

            builder.HasOne(d => d.AwayTeam)
                .WithMany(p => p.MatchAwayTeams)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_match_away_team");

            builder.HasOne(d => d.CasterProfile)
                .WithMany(p => p.MatchCasterProfiles)
                .HasForeignKey(d => d.CasterProfileId)
                .HasConstraintName("FK_match_caster_profile");

            builder.HasOne(d => d.HomeTeam)
                .WithMany(p => p.MatchHomeTeams)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_match_home_team");

            builder.HasOne(d => d.SecondaryCasterProfile)
                .WithMany(p => p.MatchSecondaryCasterProfiles)
                .HasForeignKey(d => d.SecondaryCasterProfileId)
                .HasConstraintName("FK_match_second_caster_profile");
        }
    }
}
