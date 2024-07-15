create database ticketing_system;

create schema system;

-- switch schema manually
-- postgresql does not possess an equivalent to mysql's or sql server's 'use'

drop schema public;

-- Sets path to system in generally, even tho there's indication of the path in every create type/table.
SET search_path TO system;

-- data type serial is equivalent to auto_increment property
-- https://www.postgresql.org/docs/16/datatype-numeric.html#DATATYPE-SERIAL

create table if not exists SYSTEM.users (
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

-- Se ocupaba hacer id_manager como unique porque la referencia FK de id_manager en tabla requests impedia crear la tabla sino se hacia unique.
ALTER TABLE system.users ADD CONSTRAINT unique_id_manager UNIQUE (id_manager);

-- https://www.postgresql.org/docs/16/datatype-enum.html
create type system.request_status as enum ('open', 'in progress', 'closed');

create table if not exists SYSTEM.requests (
	id serial primary key,
	req_type text not null, -- revisit as possible enum
	status request_status not null,
	title varchar(255) not null,
	description text,
	estimatedDueDate date, -- revisit nullable option
	-- attachedFile -- revisit when defined where to store media
	adminNotes text,
	resolutionInfo text,
	id_user int not null references users (id),
	id_manager int not null references users (id),
	id_admin int references users (id) -- null until an admin takes it over
);

create type SYSTEM.role_type as enum ('user', 'manager', 'admin');

-- a user can have multiple roles, such as user-manager or user-admin
create table if not exists SYSTEM.users_roles (
	id_user_role serial,
	id_user int references users (id),
	user_role role_type
);