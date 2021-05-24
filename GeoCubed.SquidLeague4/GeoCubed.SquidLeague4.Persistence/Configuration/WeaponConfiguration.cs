using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class WeaponConfiguration : IEntityTypeConfiguration<Weapon>
    {
        public void Configure(EntityTypeBuilder<Weapon> builder)
        {
            builder.ToTable("weapon");

            builder.HasIndex(e => e.SpecialId, "FK_weapon_special");

            builder.HasIndex(e => e.SubId, "FK_weapon_sub");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.PicturePath)
                .HasMaxLength(2000)
                .HasColumnName("picture_path");

            builder.Property(e => e.SpecialId)
                .HasColumnType("int(11)")
                .HasColumnName("special_id");

            builder.Property(e => e.SubId)
                .HasColumnType("int(11)")
                .HasColumnName("sub_id");

            builder.Property(e => e.WeaponName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("weapon_name");

            builder.Property(e => e.WeaponRole)
                .HasColumnType("enum('Anchor','Frontline','Support','Midline')")
                .HasColumnName("weapon_role");

            builder.Property(e => e.WeaponType)
                .HasColumnType("enum('Shooter','Blaster','Roller','Brush','Charger','Slosher','Splatling','Dualie','Brella')")
                .HasColumnName("weapon_type");

            builder.HasOne(d => d.WeaponSpecial)
                .WithMany(p => p.Weapons)
                .HasForeignKey(d => d.SpecialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weapon_special");

            builder.HasOne(d => d.WeaponSub)
                .WithMany(p => p.Weapons)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weapon_sub");
        }
    }
}
