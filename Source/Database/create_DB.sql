/*
 * This is the create script for the Squid league 4 database
 * This is a schemea for a mysql database
 */ 
 
/*
 * -- Tables --
 * team
 * player
 * caster_profile
 * match
 * bracket_swiss
 * bracket_knockout
 * weapon_sub
 * weapon_special
 * weapon
 * game_map
 * game_mode
 * game_setting
 * game
 * weapon_played
 * helpful_people
 * ...
 * user <- might not need depending on log in method.
 * audit
 */

-- this will cause an error if there is already a sl4 db (this is fine as we don't want to be just deleting any db we come across)	
create database `squid_league_4`;

create table `squid_league_4`.`team`(
   `id` int not null auto_increment comment 'Id of the team table',
   `team_name` varchar(100) comment 'Name of the team',
   `is_active` tinyint(1) default 0 comment 'If the team is part of the tournament',
   constraint `PK_team` primary key(`id`)
) comment 'Contains information about the teams';

create table `squid_league_4`.`player`(
   `id` int not null auto_increment comment 'Id of the player table',
   `in_game_name` varchar(10) not null comment 'The 10 character switch name of the player',
   `sz_rank` enum('NA', 'UN', 'C-', 'C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+', 'S', 'S+', 'X') default 'NA' comment 'The player\'s splat zones rank',
   `rm_rank` enum('NA', 'UN', 'C-', 'C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+', 'S', 'S+', 'X') default 'NA' comment 'The player\'s rain maker rank',
   `tc_rank` enum('NA', 'UN', 'C-', 'C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+', 'S', 'S+', 'X') default 'NA' comment 'The player\'s tower control rank',
   `cb_rank` enum('NA', 'UN', 'C-', 'C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+', 'S', 'S+', 'X') default 'NA' comment 'The player\'s clam blitz rank',
   `team_id` int comment 'Id of the team the player is part of',
   `is_active` tinyint(1) default 0 comment 'If the player is part of the tournament',
   constraint `PK_player` primary key(`id`),
   constraint `FK_player_team` foreign key(`team_id`) references `squid_league_4`.`team`(`id`)
) comment 'Contains information on the players including their ranks and team';

create table `squid_league_4`.`caster_profile`(
   `id` int not null auto_increment comment 'Id of the caster profile table',
   `caster_name` varchar(255) not null comment 'Prefered name of the caster',
   `twitter` varchar(255) comment 'Link to the caster\'s twitter page',
   `youtube` varchar(255) comment 'Link to the caster\'s youtube page',
   `twitch` varchar(255) comment 'Link to the caster\'s twitch page',
   `discord` varchar(255) comment 'Caster\'s discord name',
   `profile_picture_path` varchar(2000) comment 'Path to the caster\'s profile picture',
   `is_active` tinyint(1) default 0 comment 'If the caster is active or not',  
   constraint `PK_caster_profile` primary key(`id`)
) comment 'Contains information on the casters and their socials';

create table `squid_league_4`.`match`(
   `id` int not null auto_increment comment 'Id of the match table',
   `home_team_id` int not null comment 'Id of the home team',
   `away_team_id` int not null comment 'Id of the away team',
   `home_team_score` int default 0 comment 'Overall score of the home team',
   `away_team_score` int default 0 comment 'Overall score of the away team',
   `caster_profile_id` int comment 'Id of the casters profile',
   `match_vod_link` varchar(2000) comment 'Link to the VOD of the match',
   `match_date` datetime comment 'Date and time when the match is/was played',
   constraint `PK_match` primary key(`id`),
   constraint `FK_match_home_team` foreign key(`home_team_id`) references `squid_league_4`.`team`(`id`),
   constraint `FK_match_away_team` foreign key(`away_team_id`) references `squid_league_4`.`team`(`id`),
   constraint `FK_match_caster_profile` foreign key(`caster_profile_id`) references `squid_league_4`.`caster_profile`(`id`)
) comment 'Used to record the overall score of a set';

create table `squid_league_4`.`bracket_swiss`
(
   `id` int not null auto_increment comment 'Id of the bracket swiss table',
   `match_id` int not null comment 'Id of the match in the swiss bracket',
   `match_week` int not null comment 'The week in which the match happens',
   constraint `PK_bracket_swiss` primary key(`id`),
   constraint `FK_bracket_swiss_match` foreign key(`match_id`) references `squid_league_4`.`match`(`id`)
) comment 'Used to store the matches that occur during the swiss stage';

create table `squid_league_4`.`bracket_knockout`(
   `id` int not null auto_increment comment 'Id of the bracket knockout table',
   `match_id` int not null comment 'Id of the match in the knockout stage',
   `stage` enum('Q1', 'Q2', 'Q3', 'Q4', 'S1', 'S2', 'F', 'T') not null comment 'The stage in which the match takes place in the bracket',
   constraint `PK_bracket_knockout` primary key(`id`),
   constraint `FK_bracket_knockout_match` foreign key(`match_id`) references `squid_league_4`.`match`(`id`)
) comment 'Used to store the matches that happen during the knockout stage';

create table `squid_league_4`.`weapon_sub`(
   `id` int not null auto_increment comment 'Id of the weapon sub table',
   `sub_name` varchar(50) not null comment 'Name of the sub',
   `picture_path` varchar(2000) comment 'Path to the picture of the sub',
   constraint `PK_weapon_sub` primary key(`id`)
) comment 'Used to store information on weapon subs';

create table `squid_league_4`.`weapon_special`(
   `id` int not null auto_increment comment 'Id of the weapon special table',
   `special_name` varchar(50) not null comment 'Name of the special',
   `picture_path` varchar(2000) comment 'Path to the picture of the special',
   constraint `PK_weapon_special` primary key(`id`)
) comment 'Used to store inforation on weapon specials';

create table `squid_league_4`.`weapon`(
   `id` int not null auto_increment comment 'Id of the weapon table',
   `weapon_name` varchar(50) not null comment 'Name of the weapon',
   `picture_path` varchar(2000) comment 'Path to the picture of the weapon',
   `sub_id` int not null comment 'Id of the weapon\'s sub',
   `special_id` int not null comment 'Id of the weapon\'s special',
   `weapon_type` enum('Shooter', 'Blaster', 'Roller', 'Brush', 'Charger', 'Slosher', 'Splatling', 'Dualie', 'Brella') comment 'Type of weapon',
   `weapon_role` enum('Anchor', 'Frontline', 'Support', 'Midline') comment 'Role the weapon plays in a team',
   constraint `PK_weapon` primary key(`id`),
   constraint `FK_weapon_sub` foreign key(`sub_id`) references `squid_league_4`.`weapon_sub`(`id`),
   constraint `FK_weapon_special` foreign key(`special_id`) references `squid_league_4`.`weapon_special`(`id`)
) comment 'Used to store information on weapons';

create table `squid_league_4`.`game_map`(
   `id` int not null auto_increment comment 'Id of the game map table',
   `map_name` varchar(50) not null comment 'Name of the map',
   `picture_path` varchar(2000) comment 'Path to the picture of the map',
   constraint `PK_map` primary key(`id`)
) comment 'Used to store information about the game maps';

create table `squid_league_4`.`game_mode`(
   `id` int not null auto_increment comment 'Id of the game mode table',
   `mode_name` varchar(50) not null comment 'Name of the mode',
   `picture_path` varchar(2000) comment 'Path to the picture of the mode',
   constraint `PK_mode` primary key(`id`)
) comment 'Used to store information about the game modes';

create table `squid_league_4`.`game_setting`(
   `id` int not null auto_increment comment 'Id of the game setting table',
   `game_map_id` int not null comment 'Id of the map',
   `game_mode_id` int not null comment 'Id of the mode',
   `bracket_stage` varchar(2) not null comment 'Stage in the swiss / bracket', -- Q1 S1 F etc for top cut 1, 2, 3 for swiss weeks.
   `sort_order` int not null comment 'Order that the games are played in', -- order in which the games are played 1 for first game in set, 9 for last (in bo9)
   constraint `PK_game_setting` primary key(`id`),
   constraint `FK_game_setting_game_map` foreign key(`game_map_id`) references `squid_league_4`.`game_map`(`id`),
   constraint `FK_game_setting_game_mode` foreign key(`game_mode_id`) references `squid_league_4`.`game_mode`(`id`)
) comment 'Used to record the map and mode used for each game of a bracket stage';

create table `squid_league_4`.`game`(
   `id` int not null auto_increment comment 'Id of the game table',
   `home_team_score` double not null default 0 comment 'Game score for the home team',
   `away_team_score` double not null default 0 comment 'Game score for the away team', 
   `game_setting_id` int not null comment 'Id of the game setting for the game',
   `match_id` int not null comment 'Id of the match the game is happening in',
   constraint `PK_game_set` primary key(`id`),
   constraint `FK_game_setting` foreign key(`game_setting_id`) references `squid_league_4`.`game_setting`(`id`),
   constraint `FK_game_set_match` foreign key(`match_id`) references `squid_league_4`.`match`(`id`)
) comment 'Used to record the scores of a single game in a set';

create table `squid_league_4`.`weapon_played`(
   `id` int not null auto_increment comment 'Id of the weapon played table',
   `player_id` int not null comment 'Id of the player who played the weapon',
   `weapon_id` int not null comment 'Id of the weapon that was played',
   `game_id` int not null comment 'Id of the game the weapon was played in',
   `is_home_team` tinyint(1) not null comment 'If the player was on the home team or away team',
   constraint `PK_weapon_played` primary key(`id`),
   constraint `FK_weapon_played_player` foreign key(`player_id`) references `squid_league_4`.`player`(`id`),
   constraint `FK_weapon_played_weapon` foreign key(`weapon_id`) references `squid_league_4`.`weapon`(`id`),
   constraint `FK_wepaon_played_game` foreign key(`game_id`) references `squid_league_4`.`game`(`id`)
) comment 'Used to link a player to a weapon for a particular match';

create table `squid_league_4`.`helpful_people`(
	`id` int not null auto_increment comment 'Id of the helpful_people table',
    `user_name` varchar(32) not null comment 'Name of the person',
    `description` varchar(128) comment 'Short description of what the person did to help',
    `profile_picture_link` varchar(2000) comment 'Link to the users selected profile picture',
    `twitter_link` varchar(2000) comment 'Link to the users twitter account',
    constraint `PK_helpful_people` primary key(`id`)
) comment 'Used to store people who helped with the project so that I can add more people easily';

/* -- insert more tables here -- */

create table `squid_league_4`.`user`(
   `id` int not null auto_increment comment 'Id of the user table',
   `username` int not null comment 'Username of the user',
   `password_hash` varchar(100) comment 'Hash of the users password because storing plaintext is big no',
   `is_admin` tinyint(1) comment 'If the user is an admin or not',
   constraint `PK_admin` primary key(`id`)
) comment 'Used to store the user profiles of the system, this will only be used on an admin side';

create table `squid_league_4`.`audit`(
   `id` int not null auto_increment comment 'Id of the audit',
   `table_name` varchar(100) comment 'Name of the table affected',
   `change_type` enum('I', 'U', 'D') comment 'Type of change that occured I=insert, U=update, D=delete',
   `old_row` varchar(2000) comment 'Values in the old row',
   `new_row` varchar(2000) comment 'Values in the new row',
   `update_date` datetime comment 'Date and time the update occured',
   `username` varchar(100) comment 'User that caused the update',
   constraint `FK_audit` primary key(`id`)
) comment 'Used to store and row changes that occur';