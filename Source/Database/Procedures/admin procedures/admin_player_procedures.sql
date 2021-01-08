use `squid_league_4`;

delimiter |

drop procedure if exists admin_get_all_player_information|
create procedure admin_get_all_player_information()
begin
	select
		`id` as 'id',
        `in_game_name` as 'inGameName',
        `sz_rank` as 'szRank',
        `rm_rank` as 'rmRank',
        `tc_rank` as 'tcRank',
        `cb_rank` as 'cbRank',
        `team_id` as 'teamId',
        `is_active` as 'isActive'
	from
		`player`
	order by
		`in_game_name` asc;
end|

drop procedure if exists admin_create_player|
create procedure admin_create_player(in inGameName varchar(10), in szRank varchar(2), in rmRank varchar(2), in tcRank varchar(2),
in cbRank varchar(2), in teamId int, in isActive tinyint)
begin
	declare playerTeam int;
    set playerTeam = if(teamId > 0, teamId, NULL);
    
    insert into `player` (`in_game_name`, `sz_rank`, `rm_rank`, `tc_rank`, `cb_rank`, `team_id`, `is_active`)
    values (inGameName, szRank, rmRank, tcRank, cbRank, playerTeam, isActive);
end|

drop procedure if exists admin_update_player|
create procedure admin_update_player(in playerId int, in inGameName varchar(10), in szRank varchar(2), in rmRank varchar(2), 
in tcRank varchar(2), in cbRank varchar(2), in teamId int, in isActive tinyint)
begin
	declare playerTeam int;
    set playerTeam = if(teamId > 0, teamId, NULL);
    
    update `player`
    set
		`in_game_name` = inGameName,
        `sz_rank` = szRank,
        `rm_rank` = rmRank,
        `tc_rank` = tcRank,
        `cb_rank` = cbRank,
        `team_id` = playerTeam,
        `is_active` = isActive
	where
		`id` = playerId;
end|

drop procedure if exists admin_delete_player|
create procedure admin_delete_player(in playerId int)
begin
	if not exists (select 1 from `player_weapon` where `player_id` = playerId) then
        delete from `player`
        where `id` = playerId;

        -- Delete successful
        select 1;
    else
        -- Player has already played a match so cannot be deleted
        select 0;
    end if;
end|

delimiter ;