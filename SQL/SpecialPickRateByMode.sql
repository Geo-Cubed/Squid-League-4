use `squid_league_4`;

select
	round((count(T1.`Special`) / T3.`GameCount`) * 100, 2) as 'Pick Rate',
    ws.`special_name` as 'Sub'
from
(
	select
 		ws.`id` as 'Special',
        gs.`game_mode_id` as 'game_mode'
	from `game` as g
	inner join `weapon_played` as wp on g.`id` = wp.`game_id`
    inner join `weapon` as w on wp.`weapon_id` = w.`id`
    inner join `weapon_special` as ws on w.`special_id` = ws.`id`
    inner join `game_setting` as gs on g.`game_setting_id` = gs.`id`
    group by g.`id`, ws.`id`
) as T1
inner join `weapon_special` as ws on ws.`id` = T1.`Special`
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
group by ws.`id`
order by round((count(T1.`Special`) / T3.`GameCount`) * 100, 2) desc;