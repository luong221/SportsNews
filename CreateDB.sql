CREATE DATABASE NewsPaper
GO
USE NewsPaper
GO



create table role
(
	id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	rolename varchar(20) not null,
	description nvarchar(100)
)
go
create table infor
(
	id bigint primary key not null identity(1,1),
	roleId bigint not null,
	email VARCHAR(100) NOT NULL,
	password VARCHAR(100) NOT NULL,
	name NVARCHAR(128) NOT NULL,
	lastname NVARCHAR(128) NOT NULL,
	gender bit not null,
	birthday datetime not null,
	address nvarchar(200) not null,
	img varchar(255),
	createAt datetime not null,
	foreign key(roleId) references role(id) on delete cascade on update cascade
)
go
CREATE TABLE users
(
	id varchar(50) PRIMARY KEY NOT NULL,	
	status varchar(30) check (status in ('ACTIVE','DEACTIVE')) default 'ACTIVE',

)
go
create table journalist
(
	id varchar(50) PRIMARY KEY NOT NULL,
	workExperience int not null,
	status varchar(30) check (status in ('WORKED','RETIRED')) default 'WORKED',
	salary bigint not null,
)
GO
create table administrative
(
	id varchar(50) PRIMARY KEY NOT NULL,
	
)
go
create table category
(
	id varchar(50) primary key not null,
	name nvarchar(100) not null,
	description nvarchar(100),
	createAt datetime not null,
	updateAt datetime,
)

go


CREATE TABLE article
(
	id BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	journalistId varchar(50) not null,
	categoryId varchar(50) not null,
	title nvarchar(100) not null,
	totalView int default 0,
	thumbnail varchar(255) not null,
	description ntext not null,
	status varchar(30) check (status in ('PUBLISHED','INITIAL','REJECTED','NEED_CHANGE')) default 'INITIAL',
	createAt datetime not null,
	updateAt datetime,
	foreign key(journalistId) references journalist(id) on delete cascade on update cascade,
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
	userId varchar(50) not null,
	description ntext not null,
	createAt datetime not null,
	updateAt datetime,
	foreign key(userId) references users(id),
	foreign key(articleId) references article(id) on delete cascade on update cascade
)
go
