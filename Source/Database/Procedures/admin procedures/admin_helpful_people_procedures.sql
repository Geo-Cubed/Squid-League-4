use `squid_league_4`;

delimiter |

drop procedure if exists admin_get_helpful_people|
create procedure admin_get_helpful_people()
begin
    select
        `id`,
        `user_name` as 'userName',
        `description`,
        `profile_picture_link` as 'picturePath',
        `twitter_link` as 'twitter'
    from 
        `helpful_people`
    order by
        `user_name` asc;
end|

drop procedure if exists admin_create_helpful_people|
create procedure admin_create_helpful_people(in userName varchar(32), in descrip varchar(128), 
in picturePath varchar(2000), in twitter varchar(2000))
begin
    insert into `helpful_people` (`user_name`, `description`, `profile_picture_link`, `twitter_link`)
    values (userName, descrip, picturePath, twitter);
end|

drop procedure if exists admin_update_helpful_people|
create procedure admin_update_helpful_people(in helpfulId int, in userName varchar(32), 
in descrip varchar(128), in picturePath varchar(2000), in twitter varchar(2000))
begin
    update `helpful_people`
    set
        `user_name` = userName,
        `description` = descrip,
        `profile_picture_link` = picturePath,
        `twitter_link` = twitter
    where `id` = helpfulId;
end|


drop procedure if exists admin_delete_helpful_people|
create procedure admin_delete_helpful_people(in helpfulId int)
begin
    delete from `helpful_people`
    where `id` = helpfulId;
end|

delimiter ;