use squid_league_4;
delimiter |

drop procedure if exists get_all_player_information|
create procedure get_all_player_information()
begin
	select
		p.`id`,
		p.`in_game_name` as playerName,
        p.`sz_rank` as splatZones,
        p.`rm_rank` as rainMaker,
        p.`tc_rank` as towerControl,
        p.`cb_rank` as clamBlitz,
        case
			when t.`is_active` = 0 then 'No Team'
			when t.`team_name` is null then 'No Team'
			else t.`team_name` 
		end as team,
        -- info on weapons roles and ranks.
        'Anchor' as `role`,
        '/Images/Splatoon/Weapons/Charger/Splatterscope.png' as weapon1,
        '/Images/Splatoon/Weapons/Shooter/SplattershotJr.png' as weapon2,
        '/Images/Splatoon/Weapons/Splatling/CustomHydraSplatling.png' as weapon3
	from
		player as p
			left join
		team as t on p.`team_id` = t.`id`
	where
		p.`is_active` = 1
	order by
		playerName asc, team asc;
end |

drop procedure if exists get_player_by_id|
create procedure get_player_by_id(in playerId int)
begin
	select
		p.`id`,
		p.`in_game_name` as playerName,
        p.`sz_rank` as splatZones,
        p.`rm_rank` as rainMaker,
        p.`tc_rank` as towerControl,
        p.`cb_rank` as clamBlitz,
        case
			when t.`is_active` = 0 then 0
            when t.`id` is null then 0
            else t.`id`
		end as teamId,
        case 
			when t.`is_active` = 0 then 'No Team'
			when t.`team_name` is null then 'No Team'
			else t.`team_name` 
		end as team,
        -- info on weapons roles and ranks.
        'Anchor' as `role`,
        '/Images/Splatoon/Weapons/Charger/Splatterscope.png' as weapon1,
        '/Images/Splatoon/Weapons/Shooter/SplattershotJr.png' as weapon2,
        '/Images/Splatoon/Weapons/Splatling/CustomHydraSplatling.png' as weapon3
	from
		player as p
			left join
		team as t on p.`team_id` = t.`id`
	where
		p.`is_active` = 1
        and p.`id` = playerId;
end |

drop procedure if exists get_players_by_team_id|
create procedure get_players_by_team_id(in teamId int)
begin
	select
		p.`id`,
		p.`in_game_name` as playerName,
        p.`sz_rank` as splatZones,
        p.`rm_rank` as rainMaker,
        p.`tc_rank` as towerControl,
        p.`cb_rank` as clamBlitz,
        -- info on weapons roles and ranks.
        'Anchor' as `role`,
        '/Images/Splatoon/Weapons/Charger/Splatterscope.png' as weapon1,
        '/Images/Splatoon/Weapons/Shooter/SplattershotJr.png' as weapon2,
        '/Images/Splatoon/Weapons/Splatling/CustomHydraSplatling.png' as weapon3
	from
		player as p
			left join
		team as t on p.`team_id` = t.`id`
	where
		p.`is_active` = 1
        and t.`is_active` = 1
        and p.`team_id` = teamId
	order by
		playerName asc, p.`id` asc;
end |

drop procedure if exists get_all_players_on_teams|
create procedure get_all_players_on_teams()
begin
	select
		p.`id`,
		p.`in_game_name` as playerName,
        p.`sz_rank` as splatZones,
        p.`rm_rank` as rainMaker,
        p.`tc_rank` as towerControl,
        p.`cb_rank` as clamBlitz,
        t.`id` as teamId,
        -- info on weapons roles and ranks.
        'Anchor' as `role`,
        '/Images/Splatoon/Weapons/Charger/Splatterscope.png' as weapon1,
        '/Images/Splatoon/Weapons/Shooter/SplattershotJr.png' as weapon2,
        '/Images/Splatoon/Weapons/Splatling/CustomHydraSplatling.png' as weapon3
	from
		player as p
			inner join
		team as t on p.`team_id` = t.`id`
	where
		p.`is_active` = 1
        and t.`is_active` = 1
	order by
		t.`team_name` asc, p.`in_game_name` asc;
end |

delimiter ;