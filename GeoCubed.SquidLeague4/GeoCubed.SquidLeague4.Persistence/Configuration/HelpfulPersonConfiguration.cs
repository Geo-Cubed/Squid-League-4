using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoCubed.SquidLeague4.Persistence.Configuration
{
    public class HelpfulPersonConfiguration : IEntityTypeConfiguration<HelpfulPerson>
    {
        public void Configure(EntityTypeBuilder<HelpfulPerson> builder)
        {
            builder.ToTable("helpful_people");

            builder.HasComment("Used to store people who helped with the project so that I can add more people easily");

            builder.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id")
                .HasComment("Id of the helpful_people table");

            builder.Property(e => e.Description)
                .HasMaxLength(128)
                .HasColumnName("description")
                .HasComment("Short description of what the person did to help");

            builder.Property(e => e.ProfilePictureLink)
                .HasMaxLength(2000)
                .HasColumnName("profile_picture_link")
                .HasComment("Link to the users selected profile picture");

            builder.Property(e => e.TwitterLink)
                .HasMaxLength(2000)
                .HasColumnName("twitter_link")
                .HasComment("Link to the users twitter account");

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("user_name")
                .HasComment("Name of the person");

            builder.Property(e => e.Role)
                .HasMaxLength(128)
                .HasColumnName("role")
                .HasComment("Role the person took in helping e.g. Moderator");
        }
    }
}
