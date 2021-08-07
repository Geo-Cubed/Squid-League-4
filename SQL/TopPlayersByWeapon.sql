use `squid_league_4`;

select 
	round((sum(T1.`win`) / count(T1.`win`)) * 100, 2) as 'Win Rate',
    p.`in_game_name` as 'Player'
from
(
	select 
		case
			when g.`home_team_score` > g.`away_team_score` then 1
			else 0
		end as 'Win',
		wp.`player_id` as 'Player',
        wp.`weapon_id` as 'weapon'
	from `game` as g
	inner join `weapon_played` as wp on g.`id` = wp.`game_id`
	where wp.`is_home_team` = 1
	union all
	select
		case
			when g.`home_team_score` < g.`away_team_score` then 1
			else 0
		end as 'Win',
		wp.`player_id` as 'Player',
        wp.`weapon_id` as 'weapon'
	from `game` as g
	inner join `weapon_played` as wp on g.`id` = wp.`game_id`
	where wp.`is_home_team` = 0
) as T1
inner join `player` as p on T1.`Player` = p.`id` 
where `weapon` <> 0
group by p.`id`
having count(T1.`Win`) > 2
order by round((sum(T1.`Win`) / count(T1.`Win`)) * 100, 2) desc;