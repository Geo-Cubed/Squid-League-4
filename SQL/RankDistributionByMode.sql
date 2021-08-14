use `squid_league_4`;

select 
	cast(RankTbl.`Rank` as char(2)) as 'Rank',
    count(*) as 'Occurances'
from
	(
		select
			`sz_rank` as 'Rank',
            '3' as 'game_mode'
		from `player`
		union all
		select
			`rm_rank` as 'Rank',
            '2' as 'game_mode'
		from `player`
        union all
		select
			`cb_rank` as 'Rank',
            '1' as 'game_mode'
		from `player`
        union all
		select
			`tc_rank` as 'Rank',
            '4' as 'game_mode'
		from `player`
    ) as RankTbl
where RankTbl.`Rank` <> 'NA'
and `game_mode` <> 0
group by RankTbl.`Rank`
order by count(*) desc;

    