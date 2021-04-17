using System;
using CubedApi.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CubedApi.Api.Data
{
    public partial class SquidLeagueContext : DbContext
    {
        public SquidLeagueContext()
        {
        }

        public SquidLeagueContext(DbContextOptions<SquidLeagueContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<BracketKnockout> BracketKnockouts { get; set; }
        public virtual DbSet<BracketSwiss> BracketSwisses { get; set; }
        public virtual DbSet<CasterProfile> CasterProfiles { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameMap> GameMaps { get; set; }
        public virtual DbSet<GameMode> GameModes { get; set; }
        public virtual DbSet<GameSetting> GameSettings { get; set; }
        public virtual DbSet<HelpfulPerson> HelpfulPeople { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<SystemSwitch> SystemSwitches { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Weapon> Weapons { get; set; }
        public virtual DbSet<WeaponPlayed> WeaponPlayeds { get; set; }
        public virtual DbSet<WeaponSpecial> WeaponSpecials { get; set; }
        public virtual DbSet<WeaponSub> WeaponSubs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Name=ConnectionStrings:squidLeagueDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.ToTable("audit");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ChangeType)
                    .HasColumnType("enum('I','U','D')")
                    .HasColumnName("change_type");

                entity.Property(e => e.NewRow)
                    .HasMaxLength(2000)
                    .HasColumnName("new_row");

                entity.Property(e => e.OldRow)
                    .HasMaxLength(2000)
                    .HasColumnName("old_row");

                entity.Property(e => e.TableName)
                    .HasMaxLength(100)
                    .HasColumnName("table_name");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<BracketKnockout>(entity =>
            {
                entity.ToTable("bracket_knockout");

                entity.HasIndex(e => e.MatchId, "FK_bracket_knockout_match");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.MatchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("match_id");

                entity.Property(e => e.Stage)
                    .IsRequired()
                    .HasColumnType("enum('Q1','Q2','Q3','Q4','S1','S2','F','T')")
                    .HasColumnName("stage");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.BracketKnockouts)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bracket_knockout_match");
            });

            modelBuilder.Entity<BracketSwiss>(entity =>
            {
                entity.ToTable("bracket_swiss");

                entity.HasIndex(e => e.MatchId, "FK_bracket_swiss_match");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.MatchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("match_id");

                entity.Property(e => e.MatchWeek)
                    .HasColumnType("int(11)")
                    .HasColumnName("match_week");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.BracketSwisses)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bracket_swiss_match");
            });

            modelBuilder.Entity<CasterProfile>(entity =>
            {
                entity.ToTable("caster_profile");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CasterName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("caster_name");

                entity.Property(e => e.Discord)
                    .HasMaxLength(255)
                    .HasColumnName("discord");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'0'")
                    .HasComment("If the caster is active or not");

                entity.Property(e => e.ProfilePicturePath)
                    .HasMaxLength(2000)
                    .HasColumnName("profile_picture_path");

                entity.Property(e => e.Twitch)
                    .HasMaxLength(255)
                    .HasColumnName("twitch");

                entity.Property(e => e.Twitter)
                    .HasMaxLength(255)
                    .HasColumnName("twitter");

                entity.Property(e => e.Youtube)
                    .HasMaxLength(255)
                    .HasColumnName("youtube");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.HasIndex(e => e.MatchId, "FK_game_set_match");

                entity.HasIndex(e => e.GameSettingId, "FK_game_setting");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AwayTeamScore).HasColumnName("away_team_score");

                entity.Property(e => e.GameSettingId)
                    .HasColumnType("int(11)")
                    .HasColumnName("game_setting_id");

                entity.Property(e => e.HomeTeamScore).HasColumnName("home_team_score");

                entity.Property(e => e.MatchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("match_id");

                entity.HasOne(d => d.GameSetting)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameSettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_game_setting");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_game_set_match");
            });

            modelBuilder.Entity<GameMap>(entity =>
            {
                entity.ToTable("game_map");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.MapName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("map_name");

                entity.Property(e => e.PicturePath)
                    .HasMaxLength(2000)
                    .HasColumnName("picture_path");
            });

            modelBuilder.Entity<GameMode>(entity =>
            {
                entity.ToTable("game_mode");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ModeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("mode_name");

                entity.Property(e => e.PicturePath)
                    .HasMaxLength(2000)
                    .HasColumnName("picture_path");
            });

            modelBuilder.Entity<GameSetting>(entity =>
            {
                entity.ToTable("game_setting");

                entity.HasIndex(e => e.GameMapId, "FK_game_setting_game_map");

                entity.HasIndex(e => e.GameModeId, "FK_game_setting_game_mode");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.BracketStage)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("bracket_stage");

                entity.Property(e => e.GameMapId)
                    .HasColumnType("int(11)")
                    .HasColumnName("game_map_id");

                entity.Property(e => e.GameModeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("game_mode_id");

                entity.Property(e => e.SortOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("sort_order");

                entity.HasOne(d => d.GameMap)
                    .WithMany(p => p.GameSettings)
                    .HasForeignKey(d => d.GameMapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_game_setting_game_map");

                entity.HasOne(d => d.GameMode)
                    .WithMany(p => p.GameSettings)
                    .HasForeignKey(d => d.GameModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_game_setting_game_mode");
            });

            modelBuilder.Entity<HelpfulPerson>(entity =>
            {
                entity.ToTable("helpful_people");

                entity.HasComment("Used to store people who helped with the project so that I can add more people easily");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("Id of the helpful_people table");

                entity.Property(e => e.Description)
                    .HasMaxLength(128)
                    .HasColumnName("description")
                    .HasComment("Short description of what the person did to help");

                entity.Property(e => e.ProfilePictureLink)
                    .HasMaxLength(2000)
                    .HasColumnName("profile_picture_link")
                    .HasComment("Link to the users selected profile picture");

                entity.Property(e => e.TwitterLink)
                    .HasMaxLength(2000)
                    .HasColumnName("twitter_link")
                    .HasComment("Link to the users twitter account");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("user_name")
                    .HasComment("Name of the person");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable("match");

                entity.HasIndex(e => e.AwayTeamId, "FK_match_away_team");

                entity.HasIndex(e => e.CasterProfileId, "FK_match_caster_profile");

                entity.HasIndex(e => e.HomeTeamId, "FK_match_home_team");

                entity.HasIndex(e => e.SecondaryCasterProfileId, "FK_match_second_caster_profile");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AwayTeamId)
                    .HasColumnType("int(11)")
                    .HasColumnName("away_team_id");

                entity.Property(e => e.AwayTeamScore)
                    .HasColumnType("int(11)")
                    .HasColumnName("away_team_score")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CasterProfileId)
                    .HasColumnType("int(11)")
                    .HasColumnName("caster_profile_id");

                entity.Property(e => e.HomeTeamId)
                    .HasColumnType("int(11)")
                    .HasColumnName("home_team_id");

                entity.Property(e => e.HomeTeamScore)
                    .HasColumnType("int(11)")
                    .HasColumnName("home_team_score")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.MatchVodLink)
                    .HasMaxLength(2000)
                    .HasColumnName("match_vod_link");

                entity.Property(e => e.MatchDate)
                    .HasColumnType("datetime")
                    .HasColumnName("match_date");

                entity.Property(e => e.SecondaryCasterProfileId)
                    .HasColumnType("int(11)")
                    .HasColumnName("secondary_caster_profile_id")
                    .HasComment("Id of the secondary caster");

                entity.HasOne(d => d.AwayTeam)
                    .WithMany(p => p.MatchAwayTeams)
                    .HasForeignKey(d => d.AwayTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_match_away_team");

                entity.HasOne(d => d.CasterProfile)
                    .WithMany(p => p.MatchCasterProfiles)
                    .HasForeignKey(d => d.CasterProfileId)
                    .HasConstraintName("FK_match_caster_profile");

                entity.HasOne(d => d.HomeTeam)
                    .WithMany(p => p.MatchHomeTeams)
                    .HasForeignKey(d => d.HomeTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_match_home_team");

                entity.HasOne(d => d.SecondaryCasterProfile)
                    .WithMany(p => p.MatchSecondaryCasterProfiles)
                    .HasForeignKey(d => d.SecondaryCasterProfileId)
                    .HasConstraintName("FK_match_second_caster_profile");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.HasIndex(e => e.TeamId, "FK_player_team");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CbRank)
                    .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                    .HasColumnName("cb_rank")
                    .HasDefaultValueSql("'NA'");

                entity.Property(e => e.InGameName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("in_game_name");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RmRank)
                    .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                    .HasColumnName("rm_rank")
                    .HasDefaultValueSql("'NA'");

                entity.Property(e => e.SzRank)
                    .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                    .HasColumnName("sz_rank")
                    .HasDefaultValueSql("'NA'");

                entity.Property(e => e.TcRank)
                    .HasColumnType("enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')")
                    .HasColumnName("tc_rank")
                    .HasDefaultValueSql("'NA'");

                entity.Property(e => e.TeamId)
                    .HasColumnType("int(11)")
                    .HasColumnName("team_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_player_team");
            });

            modelBuilder.Entity<SystemSwitch>(entity =>
            {
                entity.ToTable("system_switch");

                entity.HasComment("Used to store settings for the squid league applications");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("Id of the system switch");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name")
                    .HasComment("Name of the setting");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("value")
                    .HasComment("Value for the setting");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("team");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.TeamName)
                    .HasMaxLength(100)
                    .HasColumnName("team_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(100)
                    .HasColumnName("password_hash");

                entity.Property(e => e.Username)
                    .HasColumnType("int(11)")
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.ToTable("weapon");

                entity.HasIndex(e => e.SpecialId, "FK_weapon_special");

                entity.HasIndex(e => e.SubId, "FK_weapon_sub");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.PicturePath)
                    .HasMaxLength(2000)
                    .HasColumnName("picture_path");

                entity.Property(e => e.SpecialId)
                    .HasColumnType("int(11)")
                    .HasColumnName("special_id");

                entity.Property(e => e.SubId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sub_id");

                entity.Property(e => e.WeaponName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("weapon_name");

                entity.Property(e => e.WeaponRole)
                    .HasColumnType("enum('Anchor','Frontline','Support','Midline')")
                    .HasColumnName("weapon_role");

                entity.Property(e => e.WeaponType)
                    .HasColumnType("enum('Shooter','Blaster','Roller','Brush','Charger','Slosher','Splatling','Dualie','Brella')")
                    .HasColumnName("weapon_type");

                entity.HasOne(d => d.Special)
                    .WithMany(p => p.Weapons)
                    .HasForeignKey(d => d.SpecialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_weapon_special");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.Weapons)
                    .HasForeignKey(d => d.SubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_weapon_sub");
            });

            modelBuilder.Entity<WeaponPlayed>(entity =>
            {
                entity.ToTable("weapon_played");

                entity.HasIndex(e => e.PlayerId, "FK_weapon_played_player");

                entity.HasIndex(e => e.WeaponId, "FK_weapon_played_weapon");

                entity.HasIndex(e => e.GameId, "FK_wepaon_played_game");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.GameId)
                    .HasColumnType("int(11)")
                    .HasColumnName("game_id");

                entity.Property(e => e.IsHomeTeam).HasColumnName("is_home_team");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("player_id");

                entity.Property(e => e.WeaponId)
                    .HasColumnType("int(11)")
                    .HasColumnName("weapon_id");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.WeaponPlayeds)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_wepaon_played_game");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.WeaponPlayeds)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_weapon_played_player");

                entity.HasOne(d => d.Weapon)
                    .WithMany(p => p.WeaponPlayeds)
                    .HasForeignKey(d => d.WeaponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_weapon_played_weapon");
            });

            modelBuilder.Entity<WeaponSpecial>(entity =>
            {
                entity.ToTable("weapon_special");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.PicturePath)
                    .HasMaxLength(2000)
                    .HasColumnName("picture_path");

                entity.Property(e => e.SpecialName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("special_name");
            });

            modelBuilder.Entity<WeaponSub>(entity =>
            {
                entity.ToTable("weapon_sub");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.PicturePath)
                    .HasMaxLength(2000)
                    .HasColumnName("picture_path");

                entity.Property(e => e.SubName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sub_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
