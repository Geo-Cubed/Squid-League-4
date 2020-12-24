use squid_league_4;

delimiter |

drop procedure if exists get_set_information_by_match_id|
create procedure get_set_information_by_match_id(in matchId int)
begin
	select
		*
    from
		`game` as g
			inner join
		`game_setting` as gs on g.`game_setting_id` = gs.`id`
			inner join
		`game_mode` as gmo on gs.`game_mode_id` = gmo.`id`
			inner join
		`game_map` as gma on gs.`game_map_id` = gma.`id`
			inner join
            /* might need to construct another table of each players weapons for each game instead of this */
		`weapon_played` as wp on g.`id` = wp.`game_id`		
	where
		g.`match_id` = matchId
	order by
		gs.`sort_order` asc;
end |

delimiter ;