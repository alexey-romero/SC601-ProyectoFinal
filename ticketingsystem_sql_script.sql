create database ticketing_system;

create schema system;

-- switch schema manually
-- postgresql does not possess an equivalent to mysql's or sql server's 'use'

drop schema public;

-- data type serial is equivalent to auto_increment property
-- https://www.postgresql.org/docs/16/datatype-numeric.html#DATATYPE-SERIAL

create table if not exists users (
	id serial primary key,
	first_name varchar(255) not null,
	last_name varchar(255) not null,
	email varchar(255) not null,
	password varchar(24) not null, -- revisit data type
	phone integer,
	-- profile_picture -- revisit when defined where to store media
	job_title varchar(255),
	id_manager int -- null until assigned a manager
);

-- https://www.postgresql.org/docs/16/datatype-enum.html
create type request_status as enum ('open', 'in progress', 'closed');

create table if not exists requests (
	id serial primary key,
	req_type text not null, -- revisit as possible enum
	status request_status not null,
	title varchar(255) not null,
	description text,
	estimatedDueDate date, -- revisit nullable option
	revokePermissionDate date,
	-- attachedFile -- revisit when defined where to store media
	adminNotes text,
	resolutionInfo text,
	id_user int not null references users (id),
	id_manager int not null references users (id),
	id_admin int references users (id) -- null until an admin takes it over
);

create type role_type as enum ('user', 'manager', 'admin');

-- a user can have multiple roles, such as user-manager or user-admin
create table if not exists users_roles (
	id_user_role serial,
	id_user int references users (id),
	user_role role_type
);