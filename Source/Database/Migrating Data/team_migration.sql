-- Check for the existing tables.
use squid_league_4;
drop procedure if exists migration;

delimiter |
create procedure migration()
begin
if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'team') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`team` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squid_league_4' and table_name = 'team') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`team` table not found in the `squid_league_4` database... unable to migrate';
end if;

-- Insert the teams.
select "Migrating teams from `squidLeague`.`team` to `squid_league_4`.`team`...";
insert into `squid_league_4`.`team` (`team_name`)
select 
    `team_name`
from 
    `squidLeague`.`team`
order by 
    `team_name` asc;
end |

delimiter ;
call squid_league_4.migration;
drop procedure migration;