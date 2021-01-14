use `squid_league_4`;

delimiter |

drop procedure if exists admin_get_caster|
create procedure admin_get_caster()
begin
    select
        `id`,
        `caster_name` as 'casterName',
        `twitter`,
        `youtube`,
        `twitch`,
        `discord`,
        `profile_picture_path` as 'picturePath',
        `is_active` as 'isActive'
    from 
        `caster_profile`
    order by
        `caster_name` asc;
end|

drop procedure if exists admin_create_caster|
create procedure admin_create_caster(in casterName varchar(255), in casterTwitter varchar(255),
in casterYoutube varchar(255), in casterTwitch varchar(255), in casterDiscord varchar(255),
in picturePath varchar(2000), in isActive tinyint)
begin
    insert into `caster_profile`(`caster_name`, `twitter`, `youtube`, `twitch`, `discord`, 
    `profile_picture_path`, `is_active`)
    values (casterName, casterTwitter, casterYoutube, casterTwitch, casterDiscord, picturePath, isActive);
end|

drop procedure if exists admin_update_caster|
create procedure admin_update_caster(in casterId int, in casterName varchar(255), 
in casterTwitter varchar(255), in casterYoutube varchar(255), in casterTwitch varchar(255), 
in casterDiscord varchar(255), in picturePath varchar(2000), in isActive tinyint)
begin
    update `caster_profile`
    set
        `caster_name` = casterName,
        `twitter` = casterTwitter,
        `youtube` = casterYoutube,
        `twitch` = casterTwitch,
        `discord` = casterDiscord,
        `profile_picture_path` = picturePath,
        `is_active` = isActive
    where `id` = casterId;      
end|

drop procedure if exists admin_delete_caster|
create procedure admin_delete_caster(in casterId int)
begin
    if exists (select 1 from `match` where `caster_profile_id` = casterId) then
        -- Remove the caster from all games which they were the primary caster.
        update `match`
        set 
            `caster_profile_id` = `secondary_caster_profile_id`,
            `secondary_caster_profile_id` = null
        where `secondary_caster_profile_id` = casterId;
    elseif exists (select 1 from `match` where `secondary_caster_profile_id` = casterId) then
        -- Remove the caster from all the ames which they were the secondary caster.
        update `match`
        set `secondary_caster_profile_id` = null
        where `secondary_caster_profile_id` = casterId;
    end if;

    -- Remove the caster.
    delete from `caster_profile`
    where `id` = casterId;
end|

delimiter ;