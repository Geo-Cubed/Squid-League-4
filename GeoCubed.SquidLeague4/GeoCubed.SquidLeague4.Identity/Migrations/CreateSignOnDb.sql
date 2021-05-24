use `squid_league_4_sign_on`;

create table `AspNetRoles`(
	`Id` varchar(255) not null,
    `Name` varchar(255),
    `NormalizedName` varchar(255) unique,
    `ConcurrencyStamp` varchar(255),
    constraint `PK_AspNetRoles` primary key(`Id`)
) engine=InnoDB default charset=utf8;

create table `AspNetUsers`(
	`Id` varchar(255) not null,
	`UserName` varchar(255),
	`NormalizedUserName` varchar(255) unique,
	`Email` varchar(255),
	`NormalizedEmail` varchar(255),
	`EmailConfirmed` tinyint,
	`PasswordHash` varchar(255),
	`SecurityStamp` varchar(255),
	`ConcurrencyStamp` varchar(255),
	`PhoneNumber` varchar(255),
	`PhoneNumberConfirmed` tinyint not null,
	`TwoFactorEnabled` tinyint not null,
	`LockoutEnd` timestamp,
	`LockoutEnabled` tinyint not null,
	`AccessFailedCount` int not null,
	`FirstName` varchar(255),
	`LastName` varchar(255),
    constraint `PK_AspNetUsers` primary key(`Id`)
) engine=InnoDB default charset=utf8;

create table `AspNetRoleClaims`(
	`Id` int not null auto_increment,
    `RoleId` varchar(255) not null,
    `ClaimType` varchar(255),
    `ClaimValue` varchar(255),
    constraint `PK_AspNetRoleClaims` primary key(`Id`),
    constraint `FK_AspNetRoleClaims_AspNetRoles_RoleId` 
		foreign key(`RoleId`) 
		references `AspNetRoles`(`Id`)
        on delete cascade
) engine=InnoDB default charset=utf8;

create table `AspNetUserClaims`(
	`Id` int not null auto_increment,
    `UserId` varchar(255) not null,
    `ClaimType` varchar(255),
    `ClaimValue` varchar(255),
	constraint `PK_AspNetUserClaims` primary key(`Id`),
    constraint `FK_AspNetUserClaims_AspNetUsers_UserId` 
		foreign key(`UserId`) 
		references `AspNetUsers`(`Id`)
        on delete cascade
) engine=InnoDB default charset=utf8;

create table `AspNetUserLogins`(
	`LoginProvider` varchar(255) not null,
	`ProviderKey` varchar(255) not null,
	`ProviderDisplayName` varchar(255),
	`UserId` varchar(255),
	constraint `PK_AspNetUserLogins` primary key(`LoginProvider`, `ProviderKey`),
    constraint `FK_AspNetUserLogins_AspNetUsers_UserId` 
		foreign key(`UserId`) 
		references `AspNetUsers`(`Id`)
        on delete cascade
) engine=InnoDB default charset=utf8;

create table `AspNetUserRoles`(
	`UserId` varchar(255) not null,
    `RoleId` varchar(255) not null,
    constraint `PK_AspNetUserRoles` primary key(`UserId`, `RoleId`),
    constraint `FK_AspNetUserRoles_AspNetRoles_RoleId` 
		foreign key(`RoleId`) 
		references `AspNetRoles`(`Id`)
        on delete cascade,    
	constraint `FK_AspNetUserRoles_AspNetUsers_UserId` 
		foreign key(`UserId`) 
		references `AspNetUsers`(`Id`)
        on delete cascade
) engine=InnoDB default charset=utf8;

create table `AspNetUserTokens`(
	`UserId` varchar(255) not null,
    `LoginProvider` varchar(255) not null,
    `Name` varchar(255) not null,
    `Value` varchar(255),
    constraint `PK_AspNetUserTokens` primary key(`UserId`, `LoginProvider`, `Name`),
	constraint `FK_AspNetUserTokens_AspNetUsers_UserId` 
		foreign key(`UserId`) 
		references `AspNetUsers`(`Id`)
        on delete cascade    
) engine=InnoDB default charset=utf8;

create index `IX_AspNetRoleClaims_RoleId` on `AspNetRoleClaims` (`RoleId`);
create index `IX_AspNetUserClaims_UserId` on `AspNetUserClaims` (`UserId`);
create index `IX_AspNetUserLogins_UserId` on `AspNetUserLogins` (`UserId`);
create index `IX_AspNetUserRoles_RoleId` on `AspNetUserRoles` (`RoleId`);