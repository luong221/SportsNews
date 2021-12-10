use newspaper
go
select * from role
select * from category
select * from journalist
select * from users
select * from administrative
select * from article

insert into role values
('ADMIN',N'Quản trị viên'),
('JOURNALIST',N'Nhà Báo'),
('READER',N'Bạn Đọc')
go

insert into category(id,name,description,createAt) values
('CT001',N'Bóng Đá',N'Bóng Đá','2021/12/03'),
('CT002',N'Chuyển Nhượng',N'Chuyển Nhượng','2021/12/03'),
('CT003',N'Bóng Rổ',N'Bóng Rổ','2021/12/03'),
('CT004',N'ESPORT',N'ESPORT','2021/12/03'),
('CT005',N'Cầu Lông',N'Cầu Lông','2021/12/03'),
('CT006',N'Bóng Chuyền',N'Bóng Chuyền','2021/12/03'),
('CT007',N'Các Môn Khác',N'Các Môn Thể Thao Khác','2021/12/03')

go

insert into journalist(id,workexperience,roleid,email,password,name,lastname,gender,birthday,address,salary,createat) values
('J2021001',2,2,'lucinda1971@gmail.com','123456',N'Donna',N'Moore',0,'1979/03/12','Missouri',3900,'2021/12/03'),
('J2021002',3,2,'franz1988@gmail.com','123456',N'Jimmie',N'Robinson',1,'1984/04/16','Mississippi',6700,'2021/12/03'),
('J2021003',2,2,'oscar_heane0@hotmail.com','123456',N'Jonathan',N'McCain',1,'1966/12/20','Georgia',6100,'2021/12/03'),
('J2021004',3,2,'aaliyah1980@hotmail.com','123456',N'Willie ',N'Lin',0,'1967/11/15','Illinois',10000,'2021/12/03'),
('J2021005',1,2,'lilla2006@yahoo.com','123456',N'Barbara ',N'Neri',0,'1967/07/10','New Jersey',5000,'2021/12/03'),
('J2021006',4,2,'christa.luettg@yahoo.com','123456',N'Amanda ',N'Mayfield',0,'1956/06/07','Georgia',5200,'2021/12/03'),
('J2021007',3,2,'ariel2009@gmail.com','123456',N'Anthony ',N'Caddell',1,'1971/09/09','Ohio',5300,'2021/12/03'),
('J2021008',2,2,'sherman.turcot@hotmail.com','123456',N'Valerie ',N'Figueroa',0,'1970/05/10','Texas',5700,'2021/12/03'),
('J2021009',1,2,'sebastian_cummin@gmail.com','123456',N'Lyle ',N'Wright',1,'1971/08/28','Carolina',3900,'2021/12/03'),
('J2021010',2,2,'amalia_powlows@yahoo.com','123456',N'Marjorie ',N'Romero',1,'1981/05/11','Ohio',3900,'2021/12/03')
go
insert into administrative(id,roleid,email,password,name,lastname,gender,birthday,address) values
('A2021001',1,'luonghaui221@gmail.com','luong2001',N'Lương',N'Nguyễn',1,'2001/11/27',N'Hà Nội')
go
insert into users(id,roleid,email,password,name,lastname,gender,birthday,address,createat) values
('U20210001',3,'tranquangvinh@gmail.com','123456',N'Vinh',N'Trần Quang',1,'2001-9-26',N'Hà Nội','2021-4-12 9:30'),
('U20210002',3,'lequangtuyen@gmail.com','123456',N'Tuyển',N'Lê Quang',1,'2003-11-20',N'Hải Phòng','2021-2-23 15:30'),
('U20210003',3,'nguyenthihien@gmail.com','123456',N'Hiền',N'Nguyễn Thị',0,'2000-2-12',N'Tp Hồ Chí Minh','2018-1-17 9:30'),
('U20210004',3,'nguyenlinhchi@gmail.com','123456',N'Chi',N'Nguyễn Linh',0,'1998-12-12',N'Hà Nam','2019-6-25 11:30'),
('U20210005',3,'nguyenvanluong@gmail.com','123456',N'Lương',N'Nguyễn Văn',1,'2001-5-28',N'Hà Nội','2020-5-2 19:30'),
('U20210006',3,'buithuylinh@gmail.com','123456',N'Linh',N'Bùi Thùy',0,'1999-4-22',N'Đà Nẵng','2021-1-19 20:30'),
('U20210008',3,'nguyenduclong@gmail.com','Dl123456@.',N'Long',N'Nguyễn Đức',1,'2000-11-9',N'Cần Thơ','2020-9-10 7:30'),
('U20210009',3,'nguyenthuha@gmail.com','Th123456@.',N'Hà',N'Nguyễn Thu',0,'2002-10-11',N'Hà Nội','2021-3-21 10:30'),
('U20210010',3,'thaianhvu@gmail.com','Av123456@.',N'Vũ',N'Thái Anh',1,'2000-1-12',N'Hà Nội','2018-6-28 10:30'),
('U20210011',3,'buiquangtung@gmail.com','Qt123456@.',N'Tùng',N'Bùi Quang',1,'2000-10-1',N'Hà Nội','2020-12-23 6:30')
select * from article


insert into dbo.article(journalistId, categoryId, title, thumbnail, description, createAt) values


