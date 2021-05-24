using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            builder.Property(e => e.IsAdmin).HasColumnName("is_admin");

            builder.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .HasColumnName("password_hash");

            builder.Property(e => e.Username)
                .HasColumnType("int(11)")
                .HasColumnName("username");
        }
    }
}
