using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
            builder.ToTable("statistic");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.Alias)
                .HasColumnType("varchar(128)")
                .HasColumnName("alias");

            builder.Property(e => e.Sql)
                .HasColumnType("text")
                .HasColumnName("sql");

            builder.Property(e => e.Modifier)
                .HasColumnType("enum('none','mode','weapon','team')")
                .HasColumnName("modifier");
        }
    }
}
