use `squid_league_4`;

delimiter |

drop procedure if exists `admin_create_game_setting`|
create procedure `admin_create_game_setting`(in mapId int, in modeName varchar(32), in stage varchar(2), in sortOrder int)
begin

end|

drop procedure if exists `admin_get_game_setting`|
create procedure `admin_get_game_setting`()
begin

end|

drop procedure if exists `admin_update_game_setting`|
create procedure `admin_update_game_setting`(in settingId int, in mapId int, in modeName varchar(32), 
in stage varchar(2), in sortOrder int)
begin

end|

drop procedure if exists `admin_delete_game_setting`|
create procedure `admin_delete_game_setting`(in settingId int)
begin

end|

delimiter ;