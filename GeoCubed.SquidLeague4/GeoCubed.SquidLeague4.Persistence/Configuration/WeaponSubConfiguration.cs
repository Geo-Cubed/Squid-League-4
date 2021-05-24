using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class WeaponSubConfiguration : IEntityTypeConfiguration<WeaponSub>
    {
        public void Configure(EntityTypeBuilder<WeaponSub> builder)
        {
            builder.ToTable("weapon_sub");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.PicturePath)
                .HasMaxLength(2000)
                .HasColumnName("picture_path");

            builder.Property(e => e.SubName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("sub_name");
        }
    }
}
