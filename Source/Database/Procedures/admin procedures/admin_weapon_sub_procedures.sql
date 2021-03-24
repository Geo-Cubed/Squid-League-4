use `squid_league_4`;

delimiter |

drop procedure if exists `admin_get_weapon_sub`|
create procedure `admin_get_weapon_sub`()
begin
	select
		`id`,
        `sub_name` as 'name' ,
        `picture_path` as 'picturePath'
	from
		`weapon_sub`
	order by
		`sub_name` asc;
end|

delimiter ;