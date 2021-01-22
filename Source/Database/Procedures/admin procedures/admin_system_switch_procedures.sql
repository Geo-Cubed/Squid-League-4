use `squid_league_4`;

delimiter |

drop procedure if exists admin_get_system_switch|
create procedure admin_get_system_switch()
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

drop procedure if exists admin_update_system_switch|
create procedure admin_update_system_switch(in switchId int, in settingName varchar(128), in settingValue varchar(128))
begin
	update `system_switch`
    set
		`name` = settingName,
        `value` = settingValue
	where `id` = switchId;
end|

drop procedure if exists admin_delete_system_switch|
create procedure admin_delete_system_switch(in switchId int)
begin
	delete from `system_switch`
    where `id` = switchId;
end|

delimiter ;