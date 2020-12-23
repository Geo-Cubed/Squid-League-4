use squid_league_4;

delimiter |

drop procedure if exists get_all_swiss_match_information|
create procedure get_all_swiss_match_information()
begin
	select
		m.`id` as 'id',
        m.`home_team_id` as 'homeTeamId',
        m.`away_team_id` as 'awayTeamId',
        ht.`team_name` as 'homeTeamName',
        awt.`team_name` as 'awayTeamName',
        m.`home_team_score` as 'homeTeamScore',
        m.`away_team_score` as 'awayTeamScore',
        m.`caster_profile_id` as 'casterId',
        c.`caster_name` as 'casterName',
        m.`match_vod_link` as 'vodLink',
        m.`match_date` as 'matchDate',
        s.`match_week` as 'matchWeek',
        case
			when m.`match_date` < now() then 1
            else 0
		end as 'hasHappened'
	from
		`bracket_swiss` as s
			inner join
		`match` as m on s.`match_id` = m.`id`
			inner join
		`team` as ht on m.`home_team_id` = ht.`id`
			inner join
		`team` as awt on m.`away_team_id` = awt.`id`
			left join
		`caster_profile` as c on m.`caster_profile_id` = c.`id`
	order by
		s.`match_week` asc, 'hasHappened' desc, m.`id` asc;  
end|

delimiter ;