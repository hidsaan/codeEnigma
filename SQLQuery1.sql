create table users (
id int not null primary key identity,
firstname varchar (100)  not null,
lastname varchar (100)  not null,
dob varchar (50) not null,
phone varchar (20) null,
email varchar (150) not null unique,
address varchar (200) null,
password varchar (25)  not null,
created_at datetime not null default current_timestamp
);

insert into users (firstname, lastname, dob, phone, email, address, password) values
('Aiman', 'Mukhtar', '11-09-2000', '+91 9858280468', 'aiman@gmail.com', 'srinagar, j&k', 'abc123')
