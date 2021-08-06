use `squid_league_4`;

select 
	round((sum(T1.`Wins`) / sum(T1.`Total`)) * 100, 2) as 'Win Rate', 
    t.`team_name` as 'Team'
from
(
	select 
		(m.`home_team_score` + m.`away_team_score`) as 'Total', 
        m.`home_team_score` as 'Wins',
        m.`home_team_id` as 'Team'
	from 
		`match` as m
    where 
		m.`winner` <> 'none'
	union all
	select 
		(m.`home_team_score` + m.`away_team_score`) as 'Total', 
        m.`away_team_score` as 'Wins',
        m.`away_team_id` as 'Team'
	from 
		`match` as m
    where 
		m.`winner` <> 'none'
) as T1
inner join
	`team` as t on T1.`Team` = t.`id`
where
	t.`team_name` <> 'BYE'
group by T1.`Team`
order by round((sum(T1.`Wins`) / sum(T1.`Total`)) * 100, 2) desc;