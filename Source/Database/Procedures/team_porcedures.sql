use squid_league_4;
delimiter |

drop procedure if exists get_all_team_information|
create procedure get_all_team_information()
begin
	select
		`id`,
        `team_name` as teamName,
        0 as teamWins,
        0 as teamLosses
	from
		`team`
	where
		`is_active` = 1
	order by
		`team_name` asc, `id` asc;
end|

drop procedure if exists get_team_by_id|
create procedure get_team_by_id(in teamId int)
begin 
	select 
		`id`,
        `team_name` as teamName
	from
		`team`
	where
		`is_active` = 1
        and `id` = teamId;
end|  

drop procedure if exists get_team_by_player_id|
create procedure get_team_by_player_id(in playerId int)
begin
	select
		case
			when t.`id` is null then -1
            else t.`id`
		end as id,
        case 
			when t.`team_name` is null then 'No Team'
            else t.`team_name`
		end as teamName
	from
		`team` as t
			inner join
		`player` as p on t.`id` = p.`team_id`
	where
        t.`is_active` = 1
        and p.`is_active`
        and p.`id` = playerId;
end |

delimiter ;