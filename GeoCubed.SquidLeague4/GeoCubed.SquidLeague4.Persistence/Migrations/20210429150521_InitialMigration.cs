using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace GeoCubed.SquidLeague4.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    table_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    change_type = table.Column<string>(type: "enum('I','U','D')", nullable: true),
                    old_row = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    new_row = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "caster_profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    caster_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    twitter = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    youtube = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    twitch = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    discord = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    profile_picture_path = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'", comment: "If the caster is active or not"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caster_profile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_map",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    map_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    picture_path = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_map", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_mode",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    mode_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    picture_path = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_mode", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "helpful_people",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false, comment: "Id of the helpful_people table")
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, comment: "Name of the person"),
                    description = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true, comment: "Short description of what the person did to help"),
                    profile_picture_link = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true, comment: "Link to the users selected profile picture"),
                    twitter_link = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true, comment: "Link to the users twitter account"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_helpful_people", x => x.id);
                },
                comment: "Used to store people who helped with the project so that I can add more people easily");

            migrationBuilder.CreateTable(
                name: "system_switch",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false, comment: "Id of the system switch")
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "Name of the setting"),
                    value = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false, comment: "Value for the setting"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_switch", x => x.id);
                },
                comment: "Used to store settings for the squid league applications");

            migrationBuilder.CreateTable(
                name: "team",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    team_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<int>(type: "int(11)", nullable: false),
                    password_hash = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    is_admin = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "weapon_special",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    special_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    picture_path = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapon_special", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "weapon_sub",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    sub_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    picture_path = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapon_sub", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "game_setting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    game_map_id = table.Column<int>(type: "int(11)", nullable: false),
                    game_mode_id = table.Column<int>(type: "int(11)", nullable: false),
                    bracket_stage = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    sort_order = table.Column<int>(type: "int(11)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_setting", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_setting_game_map",
                        column: x => x.game_map_id,
                        principalTable: "game_map",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_game_setting_game_mode",
                        column: x => x.game_mode_id,
                        principalTable: "game_mode",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "match",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    home_team_id = table.Column<int>(type: "int(11)", nullable: false),
                    away_team_id = table.Column<int>(type: "int(11)", nullable: false),
                    home_team_score = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'0'"),
                    away_team_score = table.Column<int>(type: "int(11)", nullable: true, defaultValueSql: "'0'"),
                    caster_profile_id = table.Column<int>(type: "int(11)", nullable: true),
                    match_vod_link = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    match_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    secondary_caster_profile_id = table.Column<int>(type: "int(11)", nullable: true, comment: "Id of the secondary caster"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match", x => x.id);
                    table.ForeignKey(
                        name: "FK_match_away_team",
                        column: x => x.away_team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_match_caster_profile",
                        column: x => x.caster_profile_id,
                        principalTable: "caster_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_match_home_team",
                        column: x => x.home_team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_match_second_caster_profile",
                        column: x => x.secondary_caster_profile_id,
                        principalTable: "caster_profile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "player",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    in_game_name = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    sz_rank = table.Column<string>(type: "enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')", nullable: true, defaultValueSql: "'NA'"),
                    rm_rank = table.Column<string>(type: "enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')", nullable: true, defaultValueSql: "'NA'"),
                    tc_rank = table.Column<string>(type: "enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')", nullable: true, defaultValueSql: "'NA'"),
                    cb_rank = table.Column<string>(type: "enum('NA','UN','C-','C','C+','B-','B','B+','A-','A','A+','S','S+','X')", nullable: true, defaultValueSql: "'NA'"),
                    team_id = table.Column<int>(type: "int(11)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValueSql: "'0'"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player", x => x.id);
                    table.ForeignKey(
                        name: "FK_player_team",
                        column: x => x.team_id,
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "weapon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    weapon_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    picture_path = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    sub_id = table.Column<int>(type: "int(11)", nullable: false),
                    special_id = table.Column<int>(type: "int(11)", nullable: false),
                    weapon_type = table.Column<string>(type: "enum('Shooter','Blaster','Roller','Brush','Charger','Slosher','Splatling','Dualie','Brella')", nullable: true),
                    weapon_role = table.Column<string>(type: "enum('Anchor','Frontline','Support','Midline')", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapon", x => x.id);
                    table.ForeignKey(
                        name: "FK_weapon_special",
                        column: x => x.special_id,
                        principalTable: "weapon_special",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_weapon_sub",
                        column: x => x.sub_id,
                        principalTable: "weapon_sub",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bracket_knockout",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    match_id = table.Column<int>(type: "int(11)", nullable: false),
                    stage = table.Column<string>(type: "enum('Q1','Q2','Q3','Q4','S1','S2','F','T')", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bracket_knockout", x => x.id);
                    table.ForeignKey(
                        name: "FK_bracket_knockout_match",
                        column: x => x.match_id,
                        principalTable: "match",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bracket_swiss",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    match_id = table.Column<int>(type: "int(11)", nullable: false),
                    match_week = table.Column<int>(type: "int(11)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bracket_swiss", x => x.id);
                    table.ForeignKey(
                        name: "FK_bracket_swiss_match",
                        column: x => x.match_id,
                        principalTable: "match",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "game",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    home_team_score = table.Column<double>(type: "double", nullable: false),
                    away_team_score = table.Column<double>(type: "double", nullable: false),
                    game_setting_id = table.Column<int>(type: "int(11)", nullable: false),
                    match_id = table.Column<int>(type: "int(11)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game", x => x.id);
                    table.ForeignKey(
                        name: "FK_game_set_match",
                        column: x => x.match_id,
                        principalTable: "match",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_game_setting",
                        column: x => x.game_setting_id,
                        principalTable: "game_setting",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "weapon_played",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    player_id = table.Column<int>(type: "int(11)", nullable: false),
                    weapon_id = table.Column<int>(type: "int(11)", nullable: false),
                    game_id = table.Column<int>(type: "int(11)", nullable: false),
                    is_home_team = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapon_played", x => x.id);
                    table.ForeignKey(
                        name: "FK_weapon_played_player",
                        column: x => x.player_id,
                        principalTable: "player",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_weapon_played_weapon",
                        column: x => x.weapon_id,
                        principalTable: "weapon",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_wepaon_played_game",
                        column: x => x.game_id,
                        principalTable: "game",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "FK_bracket_knockout_match",
                table: "bracket_knockout",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "FK_bracket_swiss_match",
                table: "bracket_swiss",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "FK_game_set_match",
                table: "game",
                column: "match_id");

            migrationBuilder.CreateIndex(
                name: "FK_game_setting",
                table: "game",
                column: "game_setting_id");

            migrationBuilder.CreateIndex(
                name: "FK_game_setting_game_map",
                table: "game_setting",
                column: "game_map_id");

            migrationBuilder.CreateIndex(
                name: "FK_game_setting_game_mode",
                table: "game_setting",
                column: "game_mode_id");

            migrationBuilder.CreateIndex(
                name: "FK_match_away_team",
                table: "match",
                column: "away_team_id");

            migrationBuilder.CreateIndex(
                name: "FK_match_caster_profile",
                table: "match",
                column: "caster_profile_id");

            migrationBuilder.CreateIndex(
                name: "FK_match_home_team",
                table: "match",
                column: "home_team_id");

            migrationBuilder.CreateIndex(
                name: "FK_match_second_caster_profile",
                table: "match",
                column: "secondary_caster_profile_id");

            migrationBuilder.CreateIndex(
                name: "FK_player_team",
                table: "player",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "FK_weapon_special",
                table: "weapon",
                column: "special_id");

            migrationBuilder.CreateIndex(
                name: "FK_weapon_sub",
                table: "weapon",
                column: "sub_id");

            migrationBuilder.CreateIndex(
                name: "FK_weapon_played_player",
                table: "weapon_played",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "FK_weapon_played_weapon",
                table: "weapon_played",
                column: "weapon_id");

            migrationBuilder.CreateIndex(
                name: "FK_wepaon_played_game",
                table: "weapon_played",
                column: "game_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit");

            migrationBuilder.DropTable(
                name: "bracket_knockout");

            migrationBuilder.DropTable(
                name: "bracket_swiss");

            migrationBuilder.DropTable(
                name: "helpful_people");

            migrationBuilder.DropTable(
                name: "system_switch");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "weapon_played");

            migrationBuilder.DropTable(
                name: "player");

            migrationBuilder.DropTable(
                name: "weapon");

            migrationBuilder.DropTable(
                name: "game");

            migrationBuilder.DropTable(
                name: "weapon_special");

            migrationBuilder.DropTable(
                name: "weapon_sub");

            migrationBuilder.DropTable(
                name: "match");

            migrationBuilder.DropTable(
                name: "game_setting");

            migrationBuilder.DropTable(
                name: "team");

            migrationBuilder.DropTable(
                name: "caster_profile");

            migrationBuilder.DropTable(
                name: "game_map");

            migrationBuilder.DropTable(
                name: "game_mode");
        }
    }
}
