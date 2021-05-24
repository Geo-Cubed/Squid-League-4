using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class WeaponPlayedConfiuration : IEntityTypeConfiguration<WeaponPlayed>
    {
        public void Configure(EntityTypeBuilder<WeaponPlayed> builder)
        {
            builder.ToTable("weapon_played");

            builder.HasIndex(e => e.PlayerId, "FK_weapon_played_player");

            builder.HasIndex(e => e.WeaponId, "FK_weapon_played_weapon");

            builder.HasIndex(e => e.GameId, "FK_wepaon_played_game");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.GameId)
                .HasColumnType("int(11)")
                .HasColumnName("game_id");

            builder.Property(e => e.IsHomeTeam).HasColumnName("is_home_team");

            builder.Property(e => e.PlayerId)
                .HasColumnType("int(11)")
                .HasColumnName("player_id");

            builder.Property(e => e.WeaponId)
                .HasColumnType("int(11)")
                .HasColumnName("weapon_id");

            builder.HasOne(d => d.Game)
                .WithMany(p => p.WeaponPlayeds)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_wepaon_played_game");

            builder.HasOne(d => d.Player)
                .WithMany(p => p.WeaponPlayeds)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weapon_played_player");

            builder.HasOne(d => d.Weapon)
                .WithMany(p => p.WeaponPlayeds)
                .HasForeignKey(d => d.WeaponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weapon_played_weapon");
        }
    }
}
