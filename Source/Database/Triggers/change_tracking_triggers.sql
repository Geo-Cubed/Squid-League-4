use squid_league_4;

DELIMITER |
-- Change tracking triggers for team.
drop trigger if exists team_insert_tracking|
create trigger team_insert_tracking after insert on `squid_league_4`.`team`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', team_name:'", new.`team_name`, "', is_active:'", new.is_active, "'");

    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('team', 'I', newRow, now(), curUser);
end|

drop trigger if exists team_update_tracking|
create trigger team_update_tracking after update on `squid_league_4`.`team`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', team_name:'", old.`team_name`, "', is_active:'", old.is_active, "'");
    set newRow = concat("id:'", new.`id`, "', team_name:'", new.`team_name`, "', is_active:'", new.is_active, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('team', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists team_delete_tracking|
create trigger team_delete_tracking after delete on `squid_league_4`.`team`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', team_name:'", old.`team_name`, "', is_active:'", old.is_active, "'");
 
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('team', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for player.
drop trigger if exists player_insert_tracking|
create trigger player_insert_tracking after insert on `squid_league_4`.`player`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', in_game_name:'", new.`in_game_name`, "', sz_rank:'", new.`sz_rank`, "', rm_rank:'", new.`rm_rank`, "', tc_rank:'", new.`tc_rank`, "', cb_rank:'", new.`cb_rank`, "', team_id:'", new.`team_id`, "', is_active:'", new.`is_active`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('player', 'I', newRow, now(), curUser);
end|

drop trigger if exists player_update_tracking|
create trigger player_update_tracking after update on `squid_league_4`.`player`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', in_game_name:'", old.`in_game_name`, "', sz_rank:'", old.`sz_rank`, "', rm_rank:'", old.`rm_rank`, "', tc_rank:'", old.`tc_rank`, "', cb_rank:'", old.`cb_rank`, "', team_id:'", old.`team_id`, "', is_active:'", old.`is_active`, "'");
    set newRow = concat("id:'", new.`id`, "', in_game_name:'", new.`in_game_name`, "', sz_rank:'", new.`sz_rank`, "', rm_rank:'", new.`rm_rank`, "', tc_rank:'", new.`tc_rank`, "', cb_rank:'", new.`cb_rank`, "', team_id:'", new.`team_id`, "', is_active:'", new.`is_active`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('player', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists player_delete_tracking|
create trigger player_delete_tracking after delete on `squid_league_4`.`player`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', in_game_name:'", old.`in_game_name`, "', sz_rank:'", old.`sz_rank`, "', rm_rank:'", old.`rm_rank`, "', tc_rank:'", old.`tc_rank`, "', cb_rank:'", old.`cb_rank`, "', team_id:'", old.`team_id`, "', is_active:'", old.`is_active`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('player', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for match.
drop trigger if exists match_insert_tracking|
create trigger match_insert_tracking after insert on `squid_league_4`.`match`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', home_team_id:'", new.`home_team_id`, "', away_team_id:'", new.`away_team_id`, "', home_team_score:'", new.`home_team_score`, "', away_team_score:'", new.`away_team_score`, "', caster_profile_id:'", new.`caster_profile_id`, "', match_vod_link:'", new.`match_vod_link`, "', match_date:'", new.`match_date`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('match', 'I', newRow, now(), curUser);
end|

drop trigger if exists match_update_tracking|
create trigger match_update_tracking after update on `squid_league_4`.`match`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', home_team_id:'", old.`home_team_id`, "', away_team_id:'", old.`away_team_id`, "', home_team_score:'", old.`home_team_score`, "', away_team_score:'", old.`away_team_score`, "', caster_profile_id:'", old.`caster_profile_id`, "', match_vod_link:'", old.`match_vod_link`, "', match_date:'", old.`match_date`, "'");
    set newRow = concat("id:'", new.`id`, "', home_team_id:'", new.`home_team_id`, "', away_team_id:'", new.`away_team_id`, "', home_team_score:'", new.`home_team_score`, "', away_team_score:'", new.`away_team_score`, "', caster_profile_id:'", new.`caster_profile_id`, "', match_vod_link:'", new.`match_vod_link`, "', match_date:'", new.`match_date`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('match', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists match_delete_tracking|
create trigger match_delete_tracking after delete on `squid_league_4`.`match`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', home_team_id:'", old.`home_team_id`, "', away_team_id:'", old.`away_team_id`, "', home_team_score:'", old.`home_team_score`, "', away_team_score:'", old.`away_team_score`, "', caster_profile_id:'", old.`caster_profile_id`, "', match_vod_link:'", old.`match_vod_link`, "', match_date:'", old.`match_date`, "'");
   
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('match', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for bracket_swiss.
drop trigger if exists bracket_swiss_insert_tracking|
create trigger bracket_swiss_insert_tracking after insert on `squid_league_4`.`bracket_swiss`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', match_id:'", new.`match_id`, "', match_week:'", new.`match_week`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('bracket_swiss', 'I', newRow, now(), curUser);
end|

drop trigger if exists bracket_swiss_update_tracking|
create trigger bracket_swiss_update_tracking after update on `squid_league_4`.`bracket_swiss`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', match_id:'", old.`match_id`, "', match_week:'", old.`match_week`, "'");
    set newRow = concat("id:'", new.`id`, "', match_id:'", new.`match_id`, "', match_week:'", new.`match_week`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('bracket_swiss', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists bracket_swiss_delete_tracking|
create trigger bracket_swiss_delete_tracking after delete on `squid_league_4`.`bracket_swiss`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', match_id:'", old.`match_id`, "', match_week:'", old.`match_week`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('bracket_swiss', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for bracket_knockout.
drop trigger if exists bracket_knockout_insert_tracking|
create trigger bracket_knockout_insert_tracking after insert on `squid_league_4`.`bracket_knockout`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', match_id:'", new.`match_id`, "', stage:'", new.`stage`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('bracket_knockout', 'I', newRow, now(), curUser);
end|

drop trigger if exists bracket_knockout_update_tracking|
create trigger bracket_knockout_update_tracking after update on `squid_league_4`.`bracket_knockout`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', match_id:'", old.`match_id`, "', stage:'", old.`stage`, "'");
    set newRow = concat("id:'", new.`id`, "', match_id:'", new.`match_id`, "', stage:'", new.`stage`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('bracket_knockout', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists bracket_knockout_delete_tracking|
create trigger bracket_knockout_delete_tracking after delete on `squid_league_4`.`bracket_knockout`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', match_id:'", old.`match_id`, "', stage:'", old.`stage`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('bracket_knockout', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for weapon_sub.
drop trigger if exists weapon_sub_insert_tracking|
create trigger weapon_sub_insert_tracking after insert on `squid_league_4`.`weapon_sub`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', sub_name:'", new.`sub_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('weapon_sub', 'I', newRow, now(), curUser);
end|

drop trigger if exists weapon_sub_update_tracking|
create trigger weapon_sub_update_tracking after update on `squid_league_4`.`weapon_sub`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', sub_name:'", old.`sub_name`, "', picture_path:'", old.`picture_path`, "'");
    set newRow = concat("id:'", new.`id`, "', sub_name:'", new.`sub_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('weapon_sub', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists weapon_sub_delete_tracking|
create trigger weapon_sub_delete_tracking after delete on `squid_league_4`.`weapon_sub`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', sub_name:'", old.`sub_name`, "', picture_path:'", old.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('weapon_sub', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for weapon_special.
drop trigger if exists weapon_special_insert_tracking|
create trigger weapon_special_insert_tracking after insert on `squid_league_4`.`weapon_special`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', special_name:'", new.`special_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('weapon_special', 'I', newRow, now(), curUser);
end|

drop trigger if exists weapon_special_update_tracking|
create trigger weapon_special_update_tracking after update on `squid_league_4`.`weapon_special`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', special_name:'", old.`special_name`, "', picture_path:'", old.`picture_path`, "'");
    set newRow = concat("id:'", new.`id`, "', special_name:'", new.`special_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('weapon_special', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists weapon_special_delete_tracking|
create trigger weapon_special_delete_tracking after delete on `squid_league_4`.`weapon_special`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', special_name:'", old.`special_name`, "', picture_path:'", old.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('weapon_special', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for weapon.
drop trigger if exists weapon_insert_tracking|
create trigger weapon_insert_tracking after insert on `squid_league_4`.`weapon`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', weapon_name:'", new.`weapon_name`, "', picture_path:'", new.`picture_path`, "', sub_id:'", new.`sub_id`, "', special_id:'", new.`special_id`, ", weapon_type:'", new.`weapon_type`, "', weapon_role:'", new.`weapon_role`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('weapon', 'I', newRow, now(), curUser);
end|

drop trigger if exists weapon_update_tracking|
create trigger weapon_update_tracking after update on `squid_league_4`.`weapon`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', weapon_name:'", old.`weapon_name`, "', picture_path:'", old.`picture_path`, "', sub_id:'", old.`sub_id`, "', special_id:'", old.`special_id`, ", weapon_type:'", old.`weapon_type`, "', weapon_role:'", old.`weapon_role`, "'");
    set newRow = concat("id:'", new.`id`, "', weapon_name:'", new.`weapon_name`, "', picture_path:'", new.`picture_path`, "', sub_id:'", new.`sub_id`, "', special_id:'", new.`special_id`, ", weapon_type:'", new.`weapon_type`, "', weapon_role:'", new.`weapon_role`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('weapon', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists weapon_delete_tracking|
create trigger weapon_delete_tracking after delete on `squid_league_4`.`weapon`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', weapon_name:'", old.`weapon_name`, "', picture_path:'", old.`picture_path`, "', sub_id:'", old.`sub_id`, "', special_id:'", old.`special_id`, ", weapon_type:'", old.`weapon_type`, "', weapon_role:'", old.`weapon_role`, "'");
  
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('weapon', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for game_map.
drop trigger if exists game_map_insert_tracking|
create trigger game_map_insert_tracking after insert on `squid_league_4`.`game_map`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', map_name:'", new.`map_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('game_map', 'I', newRow, now(), curUser);
end|

drop trigger if exists game_map_update_tracking|
create trigger game_map_update_tracking after update on `squid_league_4`.`game_map`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', map_name:'", old.`map_name`, "', picture_path:'", old.`picture_path`, "'");
    set newRow = concat("id:'", new.`id`, "', map_name:'", new.`map_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('game_map', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists game_map_delete_tracking|
create trigger game_map_delete_tracking after delete on `squid_league_4`.`game_map`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', map_name:'", old.`map_name`, "', picture_path:'", old.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('game_map', 'D', oldRow,now(), curUser);
end|

-- Change tracking triggers for game_mode.
drop trigger if exists game_mode_insert_tracking|
create trigger game_mode_insert_tracking after insert on `squid_league_4`.`game_mode`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', mode_name:'", new.`mode_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('game_mode', 'I', newRow, now(), curUser);
end|

drop trigger if exists game_mode_update_tracking|
create trigger game_mode_update_tracking after update on `squid_league_4`.`game_mode`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', mode_name:'", old.`mode_name`, "', picture_path:'", old.`picture_path`, "'");
    set newRow = concat("id:'", new.`id`, "', mode_name:'", new.`mode_name`, "', picture_path:'", new.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('game_mode', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists game_mode_delete_tracking|
create trigger game_mode_delete_tracking after delete on `squid_league_4`.`game_mode`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', mode_name:'", old.`mode_name`, "', picture_path:'", old.`picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('game_mode', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for game_settings.
drop trigger if exists game_setting_insert_tracking|
create trigger game_setting_insert_tracking after insert on `squid_league_4`.`game_setting`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', game_map_id:'", new.`game_map_id`, "', game_mode_id:'", new.`game_mode_id`, "', bracket_stage:'", new.`bracket_stage`, "', sort_order:'", new.`sort_order`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('game_setting', 'I', newRow, now(), curUser);
end|

drop trigger if exists game_setting_update_tracking|
create trigger game_setting_update_tracking after update on `squid_league_4`.`game_setting`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', game_map_id:'", old.`game_map_id`, "', game_mode_id:'", old.`game_mode_id`, "', bracket_stage:'", old.`bracket_stage`, "', sort_order:'", old.`sort_order`, "'");
    set newRow = concat("id:'", new.`id`, "', game_map_id:'", new.`game_map_id`, "', game_mode_id:'", new.`game_mode_id`, "', bracket_stage:'", new.`bracket_stage`, "', sort_order:'", new.`sort_order`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('game_setting', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists game_setting_delete_tracking|
create trigger game_setting_delete_tracking after delete on `squid_league_4`.`game_setting`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', game_map_id:'", old.`game_map_id`, "', game_mode_id:'", old.`game_mode_id`, "', bracket_stage:'", old.`bracket_stage`, "', sort_order:'", old.`sort_order`, "'");
   
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('game_setting', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for game.
drop trigger if exists game_insert_tracking|
create trigger game_insert_tracking after insert on `squid_league_4`.`game`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', home_team_score:'", new.`home_team_score`, "', away_team_score:'", new.`away_team_score`, "', game_setting_id:'", new.`game_setting_id`, "', match_id:'", new.`match_id`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('game', 'I', newRow, now(), curUser);
end|

drop trigger if exists game_update_tracking|
create trigger game_update_tracking after update on `squid_league_4`.`game`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', home_team_score:'", old.`home_team_score`, "', away_team_score:'", old.`away_team_score`, "', game_setting_id:'", old.`game_setting_id`, "', match_id:'", old.`match_id`, "'");
    set newRow = concat("id:'", new.`id`, "', home_team_score:'", new.`home_team_score`, "', away_team_score:'", new.`away_team_score`, "', game_setting_id:'", new.`game_setting_id`, "', match_id:'", new.`match_id`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('game', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists game_delete_tracking|
create trigger game_delete_tracking after delete on `squid_league_4`.`game`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', home_team_score:'", old.`home_team_score`, "', away_team_score:'", old.`away_team_score`, "', game_setting_id:'", old.`game_setting_id`, "', match_id:'", old.`match_id`, "'");
  
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('game', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for weapon_played.
drop trigger if exists weapon_played_insert_tracking|
create trigger weapon_played_insert_tracking after insert on `squid_league_4`.`weapon_played`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', player_id:'", new.`player_id`, "', weapon_id:'", new.`weapon_id`, "', game_id:'", new.`game_id`, "', is_home_team:'", new.`is_home_team`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('weapon_played', 'I', newRow, now(), curUser);
end|

drop trigger if exists weapon_played_update_tracking|
create trigger weapon_played_update_tracking after update on `squid_league_4`.`weapon_played`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', player_id:'", old.`player_id`, "', weapon_id:'", old.`weapon_id`, "', game_id:'", old.`game_id`, "', is_home_team:'", old.`is_home_team`, "'");
    set newRow = concat("id:'", new.`id`, "', player_id:'", new.`player_id`, "', weapon_id:'", new.`weapon_id`, "', game_id:'", new.`game_id`, "', is_home_team:'", new.`is_home_team`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('weapon_played', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists weapon_played_delete_tracking|
create trigger weapon_played_delete_tracking after delete on `squid_league_4`.`weapon_played`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', player_id:'", old.`player_id`, "', weapon_id:'", old.`weapon_id`, "', game_id:'", old.`game_id`, "', is_home_team:'", old.`is_home_team`, "'");
   
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('weapon_played', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for caster_profile
drop trigger if exists caster_profile_insert_tracking|
create trigger caster_profile_insert_tracking after insert on `squid_league_4`.`caster_profile`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', caster_name:'", new.`caster_name`, "', twitter:'", new.`twitter`, "', youtube:'", new.`youtube`, "', twitch:'", new.`twitch`, "', discord:'", new.`discord`, "', facebook:'", new.`facebook`, "', profile_picture_path:'", new.`profile_picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('caster_profile', 'I', newRow, now(), curUser);
end|

drop trigger if exists caster_profile_update_tracking|
create trigger caster_profile_update_tracking after update on `squid_league_4`.`caster_profile`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', caster_name:'", old.`caster_name`, "', twitter:'", old.`twitter`, "', youtube:'", old.`youtube`, "', twitch:'", old.`twitch`, "', discord:'", old.`discord`, "', facebook:'", old.`facebook`, "', profile_picture_path:'", old.`profile_picture_path`, "'");
    set newRow = concat("id:'", new.`id`, "', caster_name:'", new.`caster_name`, "', twitter:'", new.`twitter`, "', youtube:'", new.`youtube`, "', twitch:'", new.`twitch`, "', discord:'", new.`discord`, "', facebook:'", new.`facebook`, "', profile_picture_path:'", new.`profile_picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('caster_profile', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists caster_profile_delete_tracking|
create trigger caster_profile_delete_tracking after delete on `squid_league_4`.`caster_profile`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', caster_name:'", old.`caster_name`, "', twitter:'", old.`twitter`, "', youtube:'", old.`youtube`, "', twitch:'", old.`twitch`, "', discord:'", old.`discord`, "', facebook:'", old.`facebook`, "', profile_picture_path:'", old.`profile_picture_path`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('caster_profile', 'D', oldRow, now(), curUser);
end|

-- Change tracking triggers for user.
drop trigger if exists user_insert_tracking|
create trigger user_insert_tracking after insert on `squid_league_4`.`user`
for each row
begin
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set newRow = concat("id:'", new.`id`, "', username:'", new.`username`, "', password_hash:'", new.`password_hash`, "', is_admin:'", new.`is_admin`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, new_row, update_date, username)
    value
    ('user', 'I', newRow, now(), curUser);
end|

drop trigger if exists user_update_tracking|
create trigger user_update_tracking after update on `squid_league_4`.`user`
for each row
begin
    declare oldRow varchar(2000);
    declare newRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', username:'", old.`username`, "', password_hash:'", old.`password_hash`, "', is_admin:'", old.`is_admin`, "'");
    set newRow = concat("id:'", new.`id`, "', username:'", new.`username`, "', password_hash:'", new.`password_hash`, "', is_admin:'", new.`is_admin`, "'");
    
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, new_row, update_date, username)
    value
    ('user', 'U', oldRow, newRow, now(), curUser);
end|

drop trigger if exists user_delete_tracking|
create trigger user_delete_tracking after delete on `squid_league_4`.`user`
for each row
begin
    declare oldRow varchar(2000);
    declare curUser varchar(100) default current_user();

    set oldRow = concat("id:'", old.`id`, "', username:'", old.`username`, "', password_hash:'", old.`password_hash`, "', is_admin:'", old.`is_admin`, "'");
   
    insert into `squid_league_4`.`audit` (table_name, change_type, old_row, update_date, username)
    value
    ('user', 'U', oldRow, now(), curUser);
end|

DELIMITER ;