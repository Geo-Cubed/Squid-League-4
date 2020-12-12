-- Check for the existing tables.
use squid_league_4;
delimiter |

drop procedure if exists migration;
create procedure migration()
begin
if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'game_mode') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`game_mode` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squid_league_4' and table_name = 'game_mode') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`game_mode` table not found in the `squid_league_4` database... unable to migrate';
end if;

-- Insert the data.
select "Migrating teams from `squidLeague`.`game_mode` to `squid_league_4`.`game_mode`...";
insert into `squid_league_4`.`game_mode`(`mode_name`)
select
    `mode_name`
from
    `squidLeague`.`game_mode`
order by
    `mode_name` asc;
end |

delimiter ;

call squid_league_4.migration();
drop procedure migration; 