using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class BracketSwissConfiguration : IEntityTypeConfiguration<BracketSwiss>
    {
        public void Configure(EntityTypeBuilder<BracketSwiss> builder)
        {
            builder.ToTable("bracket_swiss");

            builder.HasIndex(e => e.MatchId, "FK_bracket_swiss_match");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.MatchId)
                .HasColumnType("int(11)")
                .HasColumnName("match_id");

            builder.Property(e => e.MatchWeek)
                .HasColumnType("int(11)")
                .HasColumnName("match_week");

            builder.HasOne(d => d.Match)
                .WithMany(p => p.BracketSwisses)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bracket_swiss_match");
        }
    }
}
