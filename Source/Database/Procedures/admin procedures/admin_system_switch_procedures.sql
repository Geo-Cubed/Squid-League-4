use `squid_league_4`;

delimiter |

drop procedure if exists admin_get_system_switches|
create procedure admin_get_system_switches()
begin
	select *
    from `system_switch`
    order by `name` asc;
end|

drop procedure if exists admin_create_system_switch|
create procedure admin_create_system_switch(in settingName varchar(128), in settingValue varchar(128))
begin
	if not exists (select 1 from `system_switch` where `name` = settingName and `value` = settingValue) then
		insert into `system_switch`(`name`, `value`)
        value (settingName, settingValue);
    end if;
end|

delimiter ;