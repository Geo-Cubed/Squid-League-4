use squid_league_4;

# Weapon win rate by mode.
select 
	round((sum(T1.`win`) / count(T1.`win`)) * 100, 2) as 'Win Rate',
    w.`weapon_name` as 'Weapon'
from
(
	select 
		case
			when g.`home_team_score` > g.`away_team_score` then 1
			else 0
		end as 'Win',
		wp.`weapon_id` as 'Weapon',
        gs.`game_mode_id` as 'game_mode'
	from `game` as g
	inner join `weapon_played` as wp on g.`id` = wp.`game_id`
    inner join `game_setting` as gs on g.`game_setting_id` = gs.`id`
	where wp.`is_home_team` = 1
	union all
	select
		case
			when g.`home_team_score` < g.`away_team_score` then 1
			else 0
		end as 'Win',
		wp.`weapon_id` as 'Weapon',
        gs.`game_mode_id` as 'game_mode'
	from `game` as g
	inner join `weapon_played` as wp on g.`id` = wp.`game_id`
    inner join `game_setting` as gs on g.`game_setting_id` = gs.`id`
	where wp.`is_home_team` = 0
) as T1
inner join `weapon` as w on T1.`Weapon` = w.`id` 
where `game_mode` <> 0
group by T1.`Weapon`
having count(T1.`Win`) > 3
order by round((sum(T1.`Win`) / count(T1.`Win`)) * 100, 2) desc;