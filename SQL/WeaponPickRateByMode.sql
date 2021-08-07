use `squid_league_4`;

select
	round((count(T1.`Weapon`) / T3.`GameCount`) * 100, 2) as 'Pick Rate',
    w.`weapon_name` as 'Weapon'
from
(
	select 
		g.`id` as 'GameId',
 		wp.`weapon_id` as 'Weapon',
        gs.`game_mode_id` as 'game_mode'
	from `game` as g
	inner join `weapon_played` as wp on g.`id` = wp.`game_id`
    inner join `game_setting` as gs on g.`game_setting_id` = gs.`id`
    group by g.`id`, wp.`weapon_id`
) as T1
inner join `weapon` as w on T1.`Weapon` = w.`id`
join 
(
	select 
		count(*) as 'GameCount'
	from
    (
		select `game_mode_id` as 'game_mode'
		from `game` as g 
		inner join `game_setting` as gs on g.`game_setting_id` = gs.`id` 
    ) as T2
    where `game_mode` <> 0
) as T3
where `game_mode` <> 0
group by T1.`Weapon`
order by round((count(T1.`Weapon`) / T3.`GameCount`) * 100, 2) desc;