use `squid_league_4`;

delimiter |

drop procedure if exists admin_get_maps|
create procedure admin_get_maps()
begin
	select
		`id`,
        `map_name` as 'mapName',
        `picture_path` as 'picturePath'
	from
		`game_map`
	order by
		`map_name` asc;
end|

drop procedure if exists admin_update_maps|
create procedure admin_update_maps(in mapId int, in mapName varchar(50), in picturePath varchar(2000))
begin
	update `game_map`
    set
		`map_name` = mapName,
        `picture_path` = picturePath
	where `id` = mapId;
end|

delimiter ;