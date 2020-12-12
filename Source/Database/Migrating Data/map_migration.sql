-- Check for the existing tables.
use squid_league_4;
drop procedure if exists migration;

delimiter |
create procedure migration()
begin
if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'map') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`map` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squid_league_4' and table_name = 'game_map') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`game_map` table not found in the `squid_league_4` database... unable to migrate';
end if;

-- Insert the data.
select "Migrating teams from `squidLeague`.`map` to `squid_league_4`.`game_map`...";
insert into `squid_league_4`.`game_map`(`map_name`)
select
    `map_name`
from
    `squidLeague`.`map`
order by 
    `map_name` asc;
end |
    
delimiter ;
call squid_league_4.migration();
drop procedure migration;    