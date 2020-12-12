use squid_league_4;

DELIMITER |
-- Change tracking triggers for team.
drop trigger if exists team_insert_tracking|
drop trigger if exists team_update_tracking|
drop trigger if exists team_delete_tracking|

-- Change tracking triggers for player.
drop trigger if exists player_insert_tracking|
drop trigger if exists player_update_tracking|
drop trigger if exists player_delete_tracking|

-- Change tracking triggers for match.
drop trigger if exists match_insert_tracking|
drop trigger if exists match_update_tracking|
drop trigger if exists match_delete_tracking|

-- Change tracking triggers for bracket_swiss.
drop trigger if exists bracket_swiss_insert_tracking|
drop trigger if exists bracket_swiss_update_tracking|
drop trigger if exists bracket_swiss_delete_tracking|

-- Change tracking triggers for bracket_knockout.
drop trigger if exists bracket_knockout_insert_tracking|
drop trigger if exists bracket_knockout_update_tracking|
drop trigger if exists bracket_knockout_delete_tracking|

-- Change tracking triggers for weapon_sub.
drop trigger if exists weapon_sub_insert_tracking|
drop trigger if exists weapon_sub_update_tracking|
drop trigger if exists weapon_sub_delete_tracking|

-- Change tracking triggers for weapon_special.
drop trigger if exists weapon_special_insert_tracking|
drop trigger if exists weapon_special_update_tracking|
drop trigger if exists weapon_special_delete_tracking|

-- Change tracking triggers for weapon.
drop trigger if exists weapon_insert_tracking|
drop trigger if exists weapon_update_tracking|
drop trigger if exists weapon_delete_tracking|

-- Change tracking triggers for game_map.
drop trigger if exists game_map_insert_tracking|
drop trigger if exists game_map_update_tracking|
drop trigger if exists game_map_delete_tracking|

-- Change tracking triggers for game_mode.
drop trigger if exists game_mode_insert_tracking|
drop trigger if exists game_mode_update_tracking|
drop trigger if exists game_mode_delete_tracking|

-- Change tracking triggers for game_settings.
drop trigger if exists game_setting_insert_tracking|
drop trigger if exists game_setting_update_tracking|
drop trigger if exists game_setting_delete_tracking|

-- Change tracking triggers for game.
drop trigger if exists game_insert_tracking|
drop trigger if exists game_update_tracking|
drop trigger if exists game_delete_tracking|

-- Change tracking triggers for weapon_played.
drop trigger if exists weapon_played_insert_tracking|
drop trigger if exists weapon_played_update_tracking|
drop trigger if exists weapon_played_delete_tracking|

-- Change tracking triggers for caster_profile
drop trigger if exists caster_profile_insert_tracking|
drop trigger if exists caster_profile_update_tracking|
drop trigger if exists caster_profile_delete_tracking|

-- Change tracking triggers for user.
drop trigger if exists user_insert_tracking|
drop trigger if exists user_update_tracking|
drop trigger if exists user_delete_tracking|

DELIMITER ;