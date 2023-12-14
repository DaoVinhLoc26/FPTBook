use [master]
create database FPTBOOKDB
go 
use FPTBookDB
go

create table [User]
(
	id int identity(1,1) not null primary key,
	password varchar(30) not null,
	username varchar(30) not null,
	fullName nvarchar(30) not null,
	address nvarchar(50) not null,
	phone varchar(12) null,
	taxCode varchar(12) null,
	details nvarchar(300) null,
	role varchar(10) not null
)
go
alter table [user] alter column role varchar(100) not null
go
ALTER TABLE [user] add constraint def_role default 'User' for role
go

insert into [user](username,password,fullName, role) values ('admin','121212','Dao Vinh Loc','Admin')
go

create table Author
(
	id varchar(5) not null primary key,
	name nvarchar(30) not null,
	adress nvarchar(50) null,
	email varchar(30) null
)
go

insert into dbo.[Author] values('a1','David Thomas','222 Dalas', 'thisismail123@gmail.com')
insert into dbo.[Author] values('a2','Marry','21222 Dalas', 'thisismail122123@gmail.com')
insert into dbo.[Author] values('a3','Charles','223122 Dalas', 'thisismai21l123@gmail.com')
insert into dbo.[Author] values('a4','Tommy','222312 Dalas', 'thisismail12123@gmail.com')
insert into dbo.[Author] values('a5','Alaba','2211212 Dalas', 'thisismail121323@gmail.com')
go

create table Publisher
(
	id int identity(1, 1) not null primary key,
	name nvarchar(30) not null unique,
	address nvarchar(50) null,
	details nvarchar(300) null
)
go

insert into Publisher values('Amazon','US State', 'Provider from US');
insert into Publisher values('Udemy','Malaysia','Provider from Malaysia');
insert into Publisher values('Google','Autralia','Provider from Australia');
go

create table Book
(
	id int Identity(1,1) primary key,
	title nvarchar(30) not null,
	price int not null default(10),
	detailes nvarchar(500) null,
	imagel1 varchar(5000) null,
	imagel2 varchar(5000) null,
	imagel3 varchar(5000) null,
	imagel4 varchar(5000) null,
	imagel5 varchar(5000) null,
	catId int not null,
	authorId varchar(5) not null,
	publisherID int not null,
	constraint fk_catId foreign key(catId) references Category(id),
	constraint fk_authorId foreign key(authorId) references Author(id),
	constraint fk_publisherId foreign key(publisherId) references Publisher(id),
)
go
alter table Book add thumb varchar(5000)
go

create table Cart(
	id int identity(1,1) primary key,
	user_id int references [User](id),
	total_price INT
)
go

create table CartItem(
	id int identity(1,1) primary key,
	book_id int references Book(id),
	cart_id int references Cart(id),
	quantity int not null
)
go

insert into Category values('Comic', 'This category for children');
insert into Category values('Novel', 'This category for children');
insert into Category values('Fiction', 'This category for Fiction Lover');

insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Atomic Habits', 20, 'This is good Product', 1,'a1', 1,'https://m.media-amazon.com/images/I/91bYsX41DVL._AC_UF894,1000_QL80_.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Lessons in Chemistry', 20, 'This is good Product', 1,'a1', 1,'https://cdn2.penguin.com.au/covers/original/9781804993477.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Dream', 20, 'This is good Product', 2,'a2', 2,'https://youngadultinghome.files.wordpress.com/2022/03/child-of-the-dream.jpeg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Children', 30, 'This is good Product', 2,'a2', 2,'https://99designs-blog.imgix.net/blog/wp-content/uploads/2018/01/attachment_73599840-e1516026193959.png?auto=format&q=60&fit=max&w=930');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Superman', 35, 'This is good Product', 3,'a3', 3,'https://media.istockphoto.com/id/171327725/photo/usa-superman-comic-book-cover-postage-stamp.jpg?s=612x612&w=0&k=20&c=TZ2lSp1SFRa9KQuGDNF0ykEJcQaHbKT_ap3wfa_Nwn4=');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Batman', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');

insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 001', 20, 'This is good Product', 1,'a1', 1,'https://m.media-amazon.com/images/I/91bYsX41DVL._AC_UF894,1000_QL80_.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 002', 20, 'This is good Product', 1,'a1', 1,'https://cdn2.penguin.com.au/covers/original/9781804993477.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 003', 20, 'This is good Product', 2,'a2', 2,'https://youngadultinghome.files.wordpress.com/2022/03/child-of-the-dream.jpeg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 004', 30, 'This is good Product', 2,'a2', 2,'https://99designs-blog.imgix.net/blog/wp-content/uploads/2018/01/attachment_73599840-e1516026193959.png?auto=format&q=60&fit=max&w=930');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 005', 35, 'This is good Product', 3,'a3', 3,'https://media.istockphoto.com/id/171327725/photo/usa-superman-comic-book-cover-postage-stamp.jpg?s=612x612&w=0&k=20&c=TZ2lSp1SFRa9KQuGDNF0ykEJcQaHbKT_ap3wfa_Nwn4=');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 006', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 007', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 008', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 009', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 010', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 011', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 012', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 013', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 014', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 015', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 016', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 017', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 018', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 019', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');
insert into Book(title,price,detailes,catId,authorId,publisherID,thumb) Values('Book No 020', 40, 'This is good Product', 3,'a3', 3,'https://static01.nyt.com/images/2020/12/12/arts/10batman-item-art/10batman-item-art-superJumbo.jpg');


select* from book
select * from Category
select * from Book
select * from Publisher
select * from Author
select * from [User]