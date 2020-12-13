use squid_league_4;

DELIMITER |

drop procedure if exists get_all_helpful_people_information|
create procedure get_all_helpful_people_information()
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