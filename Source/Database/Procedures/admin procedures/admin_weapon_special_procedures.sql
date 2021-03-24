use `squid_league_4`;

delimiter |

drop procedure if exists `admin_get_weapon_specail`|
create procedure `admin_get_weapon_special`()
begin
	select
		`id`,
        `special_name` as 'name',
        `picture_path` as 'picturePath'
	from
		`weapon_special`
	order by
		`special_name` asc;
end|

delimiter ;