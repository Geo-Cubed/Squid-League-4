use `squid_league_4`;

delimiter |

drop procedure if exists get_system_switch_type|
create procedure get_system_switch_type(in switchName varchar(128))
begin
	select *
    from `system_switch`
    where `name` = switchName;
end|

delimiter ;