use `squid_league_4`;
delimiter |

drop procedure if exists `admin_get_match`|
create procedure `admin_get_match`()
begin
	select
		`id`,
        `home_team_id` as 'homeTeamId',
        `away_team_id` as 'awayTeamId',
        `caster_profile_id` as 'casterProfileId',
        `match_vod_link` as 'matchVodLink',
        `match_date` as 'matchDate',
        `secondary_caster_profile_id` as 'secondaryCasterProfile'
	from
		`match`
	order by
		`match_date` desc;
end|

drop procedure if exists `admin_create_match`|
create procedure `admin_create_match`(in homeTeamId int, in awayTeamId int, in casterId int, in matchVod varchar(2000),
in matchDate datetime, in secondaryCasterId int)
begin
	insert into `match`(`home_team_id`, `away_team_id`, `caster_profile_id`, `match_vod_link`, `match_date`,
    `secondary_caster_profile_id`)
    value (homeTeamId, awayTeamId, casterId, matchVod, matchDate, secondaryCasterId);
end|

drop procedure if exists `admin_update_match`|
create procedure `admin_update_match`(in matchId int, in homeTeamId int, in awayTeamId int, in casterId int, 
in matchVod varchar(2000), in matchDate datetime, in secondaryCasterId int)
begin
	update `match`
    set
		`home_team_id` = homeTeamId,
        `away_team_id` = awayTeamId,
        `caster_profile_id` = casterId,
        `match_vod_link` = matchVod,
        `match_date` = matchDate,
        `secondary_caster_profile_id` = secondaryCasterId
	where `id` = matchId;
end|

drop procedure if exists `admin_delete_match`|
create procedure `admin_delete_match`(in matchId int)
begin
	declare output int default 0;
	if not exists (select 1 from `game` where `match_id` = matchId) then
		delete from `bracket_knockout`
        where `match_id` = matchId;
        
        delete from `bracket_swiss`
        where `match_id` = matchId;
        
		delete from `match`
        where `id` = matchId;
        
        set output = 1;
    end if;
    
    select output;
end|

delimiter ;