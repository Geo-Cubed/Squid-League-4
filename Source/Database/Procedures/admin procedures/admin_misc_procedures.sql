use `squid_league_4`;

delimiter |

drop procedure if exists admin_get_sub|
create procedure admin_get_sub()
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

drop procedure if exists admin_get_special|
create procedure admin_get_special()
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