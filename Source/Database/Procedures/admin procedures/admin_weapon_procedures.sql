use squid_league_4;

delimiter |

drop procedure if exists admin_update_weapon|
create procedure admin_update_weapon(in weaponId int, in weaponName varchar(50), in picturePath varchar(2000),
in subId int, in specialId int, 
in weaponType enum('Shooter','Blaster','Roller','Brush','Charger','Slosher','Splatling','Dualie','Brella'), 
in weaponRole enum('Anchor', 'Frontline', 'Support', 'Midline'))
begin
	update `weapon`
	set 
		`weapon_name` = weaponName,
        `picture_path` = picturePath,
        `sub_id` = subId,
        `special_id` = specialId,
        `weapon_type` = weaponType,
		`weapon_role` = weaponRole
	where `id` = weaponId;
end|

drop procedure if exists admin_get_weapon|
create procedure admin_get_weapon()
begin
	select
		`id`,
        `weapon_name` as 'name',
        `picture_path` as 'picturePath',
        `sub_id` as 'subId',
        `special_id` as 'specialId',
        `weapon_type` as 'weaponType',
        `weapon_role` as 'weaponRole'
	from
		`weapon`
	order by
		`weapon_name` asc;
end|

delimiter ;