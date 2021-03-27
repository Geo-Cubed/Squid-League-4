use squid_league_4;

DELIMITER |

drop procedure if exists api_get_helpful_people|
create procedure api_get_helpful_people()
begin
	select
		`id`,
        `user_name`,
        `description`,
        `profile_picture_link`,
        `twitter_link`
	from
		helpful_people;
end |

DELIMITER ;