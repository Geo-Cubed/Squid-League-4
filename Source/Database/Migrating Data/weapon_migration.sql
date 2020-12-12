-- Check for the existing tables.
use squid_league_4;
drop procedure if exists migration;

delimiter |
create procedure migration()
begin
if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'sub') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`sub` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squid_league_4' and table_name = 'weapon_sub') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`weapon_sub` table not found in the `squid_league_4` database... unable to migrate';
end if;

-- Insert subs.
select "Migrating teams from `squidLeague`.`sub` to `squid_league_4`.`weapon_sub`...";
insert into `squid_league_4`.`weapon_sub`(`sub_name`)
select
    `name`
from
    `squidLeague`.`sub`
order by 
    `id` asc;


-- Check for the existing tables.
if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'special') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`special` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squid_league_4' and table_name = 'weapon_special') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`weapon_special` table not found in the `squid_league_4` database... unable to migrate';
end if;

-- Insert specials.
select "Migrating teams from `squidLeague`.`special` to `squid_league_4`.`weapon_special`...";
insert into `squid_league_4`.`weapon_special`(`special_name`)
select 
    `name`
from    
    `squidLeague`.`special`
order by
    `id` asc;


-- Check for the existing tables.
if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'weapon') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`weapon` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'type_of_weapon') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`type_of_weapon` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squid_league_4' and table_name = 'weapon') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`weapon` table not found in the `squid_league_4` database... unable to migrate';
end if;

-- Insert weapons.
select "Migrating teams from `squidLeague`.`weapon` to `squid_league_4`.`weapon`...";
insert into `squid_league_4`.`weapon`(`weapon_name`, `sub_id`, `special_id`, `weapon_type`)
select 
    w.`weapon_name`,
    w.`sub_id`,
    w.`special_id`,
    t.`name_of_type`
from
    `squidLeague`.`weapon` as w
        inner join
    `squidLeague`.`type_of_weapon` as t on w.`weapon_type_id` = t.`id`
order by
    w.`id` asc;
end |

delimiter ;
call squid_league_4.migration();
drop procedure if exists migration;