use squid_league_4;

delimiter |

drop procedure if exists admin_get_all_team_information|
create procedure admin_get_all_team_information()
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

drop procedure if exists admin_update_team_information|
create procedure admin_update_team_information(in teamId int, in teamName varchar(100), in isActive tinyint)
begin
	update `team`
	set `team_name` = teamName, `is_active` = isActive
	where `id` = teamId;
end|

drop procedure if exists admin_delete_team|
create procedure admin_delete_team(in teamId int)
begin
	
end|

drop procedure if exists admin_create_team|
create procedure admin_create_team(in teamName varchar(100), in isActive tinyint)
begin
	insert into `team` (team_name, is_active)
    values (teamName, isActive);
end|

delimiter ;