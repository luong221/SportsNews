CREATE DATABASE SportsNews
GO
USE SportsNews
GO

create table role
(
	id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	rolename varchar(20) not null,
	description nvarchar(100)
)
go
create table info
(
	id bigint primary key not null IDENTITY(1,1),
	roleId bigint not null,
	email VARCHAR(100) NOT NULL,
	password VARCHAR(100) COLLATE Latin1_General_100_CI_AI_SC_UTF8 NOT NULL,
	name NVARCHAR(128) NOT NULL,
	lastname NVARCHAR(128) NOT NULL,
	gender bit not null,
	birthday datetime not null,
	address nvarchar(255) not null,
	img varchar(255),
	status varchar(30) check (status in ('ACTIVE','DEACTIVE')) default 'ACTIVE',
	createAt datetime,
	foreign key(roleId) references role(id) on delete cascade on update cascade
)
go
create table journalist
(
	id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	workExperience int not null,
	salary bigint not null,
	infoId bigint not null,
	foreign key(infoId) references info(id) on delete cascade on update cascade
)
GO
go
create table category
(
	id bigint primary key not null identity(1,1),
	name nvarchar(100) not null,
	description nvarchar(100),
	createAt datetime not null,
	updateAt datetime,
)

go


CREATE TABLE article
(
	id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	infoId bigint not null,
	categoryId bigint not null,
	title nvarchar(255) not null,
	totalView int default 0,
	thumbnail varchar(255) not null,
	description ntext not null,
	status varchar(30) check (status in ('PUBLISHED','INITIAL','REJECTED','NEED_CHANGE')) default 'INITIAL',
	createAt datetime not null,
	updateAt datetime,
	foreign key(infoId) references info(id) on delete cascade on update cascade,
	foreign key(categoryId) references category(id) on delete cascade on update cascade
)

go
create table keyword
(
	id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	name nvarchar(100) not null,
)
go
create table keyword_article
(
	articleId bigint not null,
	keywordId bigint not null,
	primary key(articleId,keywordId),
	foreign key (articleId) references article(id) on delete cascade on update cascade,
	foreign key (keywordId) references keyword(id) on delete cascade on update cascade,
)
go
create table comment
(
	id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	articleId bigint not null,
	infoId bigint not null,
	description ntext not null,
	createAt datetime not null,
	updateAt datetime,
	foreign key(infoId) references info(id),
	foreign key(articleId) references article(id) on delete cascade on update cascade
)
go

