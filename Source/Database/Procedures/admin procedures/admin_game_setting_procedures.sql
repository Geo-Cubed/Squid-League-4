use `squid_league_4`;

delimiter |

drop procedure if exists `admin_create_game_setting`|
create procedure `admin_create_game_setting`(in mapId int, in modeName varchar(32), in stage varchar(2), in sortOrder int)
begin
	declare modeId int;
    set modeId = get_mode_id_from_name(modeName);
    if (modeId > 0) then
    	insert into `game_setting`(game_map_id, game_mode_id, bracket_stage, sort_order)
        value (mapId, modeId, stage, sortOrder);
        select 1 as 'output';
    else
		select 0 as 'output';
    end if;
end|

drop procedure if exists `admin_get_game_setting`|
create procedure `admin_get_game_setting`()
begin
	select *
    from `game_setting`
    order by `bracket_stage` asc, `sort_order` asc;
end|

drop procedure if exists `admin_update_game_setting`|
create procedure `admin_update_game_setting`(in settingId int, in mapId int, in modeName varchar(32), 
in stage varchar(2), in sortOrder int)
begin
	declare modeId int;
    set modeId = get_mode_id_from_name(modeName);
    if (modeId > 0) then
		update `game_setting`
        set
			`game_map_id` = mapId,
            `game_mode_id` = modeId,
            `bracket_stage` = stage,
            `sort_order` = sortOrder
		where `id` = settingId;
        select 1 as 'output';
    else
		select 0 as 'output';
    end if;
end|

drop procedure if exists `admin_delete_game_setting`|
create procedure `admin_delete_game_setting`(in settingId int)
begin
	if not exists (select 1 from `game` where `game_setting_id` = settingId) then
		delete from `game_setting`
        where `id` = settingId;
		select 1 as 'output';
	else
		select 0 as 'output';
    end if;
end|

delimiter ;