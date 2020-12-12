-- Check for the existing tables.
use squid_league_4;
drop procedure if exists migration;

delimiter |
create procedure migration()
begin
if not exists(select 1 from information_schema.tables where table_schema = 'squidLeague' and table_name = 'player') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`player` table not found in the `squidLeague` database... unable to migrate';
end if;

if not exists(select 1 from information_schema.tables where table_schema = 'squid_league_4' and table_name = 'player') then
	SIGNAL SQLSTATE '45000'
	SET MESSAGE_TEXT = '`player` table not found in the `squid_league_4` database... unable to migrate';
end if;

-- Insert the players.
select "Migrating teams from `squidLeague`.`player` to `squid_league_4`.`player`...";
insert into `squid_league_4`.`player`(`in_game_name`, `sz_rank`, `rm_rank`, `tc_rank`, `cb_rank`)
select
    `user_name`,
    `splat_zones_rank`,
    `rain_maker_rank`,
    `tower_control_rank`,
    `clam_blitz_rank`
from
    `squidLeague`.`player`
order by 
    `user_name` asc;
end |

delimiter ;
call squid_league_4.migration();
drop procedure migration;