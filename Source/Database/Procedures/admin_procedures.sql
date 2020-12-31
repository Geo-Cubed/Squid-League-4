use squid_league_4;

delimiter |

drop procedure if exists admin_update_weapon_information|
create procedure admin_update_weapon_information(in weaponId int, in weaponRole enum('Anchor', 'Frontline', 'Support', 'Midline'))
begin
	update `weapon`
	set `weapon_role` = weaponRole
	where `id` = weaponId;
end|

delimiter ;