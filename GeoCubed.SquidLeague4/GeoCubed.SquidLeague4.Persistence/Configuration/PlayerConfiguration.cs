using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("player");

            builder.HasIndex(e => e.TeamId, "FK_player_team");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.CbRank)
                .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                .HasColumnName("cb_rank")
                .HasDefaultValueSql("'NA'");

            builder.Property(e => e.InGameName)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("in_game_name");

            builder.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.RmRank)
                .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                .HasColumnName("rm_rank")
                .HasDefaultValueSql("'NA'");

            builder.Property(e => e.SzRank)
                .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                .HasColumnName("sz_rank")
                .HasDefaultValueSql("'NA'");

            builder.Property(e => e.TcRank)
                .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                .HasColumnName("tc_rank")
                .HasDefaultValueSql("'NA'");

            builder.Property(e => e.TeamId)
                .HasColumnType("int(11)")
                .HasColumnName("team_id");

            builder.HasOne(d => d.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_player_team");
        }
    }
}
