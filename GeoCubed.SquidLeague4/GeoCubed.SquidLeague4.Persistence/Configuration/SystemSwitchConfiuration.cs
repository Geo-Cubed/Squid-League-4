using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class SystemSwitchConfiuration : IEntityTypeConfiguration<SystemSwitch>
    {
        public void Configure(EntityTypeBuilder<SystemSwitch> builder)
        {
            builder.ToTable("system_switch");

            builder.HasComment("Used to store settings for the squid league applications");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id")
                .HasComment("Id of the system switch");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("name")
                .HasComment("Name of the setting");

            builder.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("value")
                .HasComment("Value for the setting");
        }
    }
}
