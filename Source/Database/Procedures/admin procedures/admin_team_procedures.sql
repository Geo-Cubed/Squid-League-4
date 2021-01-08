use squid_league_4;

delimiter |

drop procedure if exists admin_get_team|
create procedure admin_get_team()
begin
	select
		`id` as 'id',
        `team_name` as 'teamName',
        `is_active` as 'isActive'
	from
		`team`
	order by
		`team_name` asc;
end|

drop procedure if exists admin_update_team|
create procedure admin_update_team(in teamId int, in teamName varchar(100), in isActive tinyint)
begin
	update `team`
	set `team_name` = teamName, `is_active` = isActive
	where `id` = teamId;
end|

drop procedure if exists admin_delete_team|
create procedure admin_delete_team(in teamId int)
begin
	if not exists (select 1 from `match` where `home_team_id` = teamId or `away_team_id` = teamId) then
		update `player`
        set `team_id` = null
        where `team_id` = teamId;
        
        delete from `team`
        where `id` = teamId;
        
        -- Delete successful.
        select 1 as 'outputCode';
	else
		-- Delete aborted.
		select 0 as 'outputCode';
    end if;
end|

drop procedure if exists admin_create_team|
create procedure admin_create_team(in teamName varchar(100), in isActive tinyint)
begin
	insert into `team` (team_name, is_active)
    values (teamName, isActive);
end|

delimiter ;