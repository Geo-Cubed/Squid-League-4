using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class WeaponSpecialConfiguration : IEntityTypeConfiguration<WeaponSpecial>
    {
        public void Configure(EntityTypeBuilder<WeaponSpecial> builder)
        {
            builder.ToTable("weapon_special");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.PicturePath)
                .HasMaxLength(2000)
                .HasColumnName("picture_path");

            builder.Property(e => e.SpecialName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("special_name");
        }
    }
}
