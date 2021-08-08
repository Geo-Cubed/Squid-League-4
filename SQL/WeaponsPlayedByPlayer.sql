use `squid_league_4`;

select 
	count(*) as 'Times Played',
	w.`weapon_name` as 'Weapon'
from `weapon_played` as wp
inner join `weapon` as w on wp.`weapon_id` = w.`id`
where player_id <> 0
group by wp.`weapon_id`
order by count(*) desc;


