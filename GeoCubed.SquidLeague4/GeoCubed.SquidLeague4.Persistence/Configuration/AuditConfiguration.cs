using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable("audit");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.ChangeType)
                .HasColumnType("enum('I','U','D')")
                .HasColumnName("change_type");

            builder.Property(e => e.NewRow)
                .HasMaxLength(2000)
                .HasColumnName("new_row");

            builder.Property(e => e.OldRow)
                .HasMaxLength(2000)
                .HasColumnName("old_row");

            builder.Property(e => e.TableName)
                .HasMaxLength(100)
                .HasColumnName("table_name");

            builder.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        }
    }
}
