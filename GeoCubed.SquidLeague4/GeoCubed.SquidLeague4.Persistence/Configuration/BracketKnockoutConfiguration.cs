using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class BracketKnockoutConfiguration : IEntityTypeConfiguration<BracketKnockout>
    {
        public void Configure(EntityTypeBuilder<BracketKnockout> builder)
        {
            builder.ToTable("bracket_knockout");

            builder.HasIndex(e => e.MatchId, "FK_bracket_knockout_match");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.MatchId)
                .HasColumnType("int(11)")
                .HasColumnName("match_id");

            builder.Property(e => e.Stage)
                .IsRequired()
                .HasColumnType("enum('Q1','Q2','Q3','Q4','S1','S2','F','T')")
                .HasColumnName("stage");

            builder.HasOne(d => d.Match)
                .WithMany(p => p.BracketKnockouts)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bracket_knockout_match");
        }
    }
}
