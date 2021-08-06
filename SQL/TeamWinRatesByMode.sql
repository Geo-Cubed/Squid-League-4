use `squid_league_4`;

select 
	round((sum(T1.`Win`) / count(T1.`Win`)) * 100, 2) as 'Win Rate',
    t.`team_name` as 'Team Name'
from
(
	select 
		case
			when g.`home_team_score` > g.`away_team_score` then 1
			else 0
		end as 'Win',
		m.`home_team_id` as 'Team',
		gs.`game_mode_id` as 'game_mode' 
	from `game` as g
	inner join `game_setting` as gs on g.`game_setting_id` = gs.`id`
	inner join `match` as m on g.`match_id` = m.`id`
	union all
	select 
		case
			when g.`home_team_score` < g.`away_team_score` then 1
			else 0
		end as 'Win',
		m.`away_team_id` as 'Team',
		gs.`game_mode_id` as 'game_mode' 
	from `game` as g
	inner join `game_setting` as gs on g.`game_setting_id` = gs.`id`
	inner join `match` as m on g.`match_id` = m.`id`
) as T1
inner join `team` as t on T1.`Team` = t.`id`
where t.`team_name` <> 'BYE'
and `game_mode` <> 0
group by T1.`Team`
order by round((sum(T1.`Win`) / count(T1.`Win`)) * 100, 2) desc;

