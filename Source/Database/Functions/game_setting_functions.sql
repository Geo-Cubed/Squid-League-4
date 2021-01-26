use `squid_league_4`;

delimiter |

drop function if exists `get_mode_id_from_name`|
create function `get_mode_id_from_name`(modeName varchar(32))
returns int
reads sql data
deterministic
begin
	declare modeNameFormatted varchar(32);
    declare modeId int;
    set modeNameFormatted = lower(replace(modeName, ' ', ''));
    
    select
		`id` into modeId
	from
		`game_mode`
	where
		lower(replace(`mode_name`, ' ', '')) = modeNameFormatted;
        
	if (modeId is null) then
		return -1;
	else
		return modeId;
	end if;
end|

delimiter ;