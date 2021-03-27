use `squid_league_4`;

delimiter |

drop procedure if exists api_get_caster|
create procedure api_get_caster()
begin
	select
		`id`,
        `caster_name` as 'name',
        `twitter`,
        `youtube`,
        `twitch`,
        `discord`,
        `profile_picture_path` as 'picturePath'
	from
		`caster_profile`
	where
		`is_active` = 1
	order by
		`caster_name` asc;
end|

drop procedure if exists api_get_caster_by_caster_id|
create procedure api_get_caster_by_caster_id(in casterId int)
begin
	select
		`id`,
        `caster_name` as 'name',
		`twitter`,
        `youtube`,
        `twitch`,
        `discord`,
        `profile_picture_path` as 'picturePath'
	from
		`caster_profile`
	where
		`is_active` = 1
        and `id` = casterId;
end|

drop procedure if exists api_get_caster_by_match_id|
create procedure api_get_caster_by_match_id(in matchId int)
begin
	select
		c.`id` as 'id',
        c.`caster_name` as 'name',
		c.`twitter` as 'twitter',
        c.`youtube` as 'youtube',
        c.`twitch` as 'twitch',
        c.`discord` as 'discord',
        c.`profile_picture_path` as 'picturePath'
	from
		`caster_profile` as c
			inner join
		`match` as m on c.`id` = m.`caster_profile_id`
	where
		c.`is_active` = 1
        and m.`id` = matchId;
end|

delimiter ;