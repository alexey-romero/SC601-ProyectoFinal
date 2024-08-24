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
-- In Progress: Cuando user normal crea el Request, ticket pasa a In progress (IT Admin puede verlo)
-- Approved: Cuando IT Admin cambia status a aprobado. Posteriormente lo cierra (closed)
-- Denied: Cuando IT Admin cambia status a rechazado. Posteriormente lo cierra (closed)
-- Closed: Request se enlista en My tickets de IT Admin como request anteriores.
create type system.request_status as enum ('in progress', 'approved', 'denied', 'closed');

CREATE TYPE SYSTEM.request_type AS ENUM ('Paid Time Off', 'Maternity Leave', 'Software Installation/Fix', 'Hardware');

CREATE TABLE IF NOT EXISTS SYSTEM.request_types (
    id_request_type SERIAL PRIMARY KEY,
    type_name TEXT NOT NULL UNIQUE
);

create table if not exists SYSTEM.requests (
	id serial primary key,
	id_request_type INT NOT NULL REFERENCES SYSTEM.request_types(id_request_type),
	status request_status not null,
	title varchar(255) not null,
	description text,
	estimatedDueDate date, -- revisit nullable option
	-- attachedFile -- revisit when defined where to store media
	adminNotes text,
	resolutionInfo text,
	id_user int not null references users (id),
	id_manager int not null references users (id),
	id_admin int references users (id), -- null until an admin takes it over
	creation_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL -- Fecha de creaci�n del ticket
);


create type SYSTEM.role_type as enum ('user', 'admin');

-- a user can have multiple role or user-admin
create table if not exists SYSTEM.users_roles (
	id_user_role serial,
	id_user int references users (id),
	user_role role_type
);

/*			TESTING			*/

select * from users;
select * from user_roles;
select * from requests;
select * from request_types;


/*	Agrego rol tipo manager para que users tengan un id de manager	*/
ALTER TYPE SYSTEM.role_type ADD VALUE 'manager';

-- Insertar tres usuarios con rol 'user' normal

INSERT INTO SYSTEM.users (first_name, last_name, email, password, phone, job_title, id_manager) 
VALUES 
('Juan', 'Perez', 'juanperez@email.com', 'password1', '1234567890', 'ingeniero junior', 6),
('Pedro', 'Ramirez', 'pedroramirez@email.com', 'password2', '0987654321', 'ingeniero mid', 7),
('Maria', 'Gonzalez', 'mariagonzalez@email.com', 'password3', '0987654323', 'ingeniero senior', 7);


-- Asignarles el rol 'user' en la tabla users_roles
INSERT INTO SYSTEM.users_roles (id_user, user_role) 
VALUES 
(1, 'user'), 
(2, 'user'), 
(3, 'user');

INSERT INTO SYSTEM.users (first_name, last_name, email, password, phone, job_title, id_manager) 
VALUES 
('jason', 'kid', 'admin1@email.com', 'admin1rules', '546561515', 'AdminPosition1', NULL),
('martin', 'sommers', 'admin2@email.com', 'admin2rules', '879845516', 'AdminPosition2', NULL);


-- Asignarles el rol 'admin' en la tabla users_roles
INSERT INTO SYSTEM.users_roles (id_user, user_role) 
VALUES 
(4, 'admin'), 
(5, 'admin');


INSERT INTO SYSTEM.users (first_name, last_name, email, password, phone, job_title, id_manager) 
VALUES 
('MicroManager', 'Intenso', 'manager1@email.com', 'manager1', NULL, 'ManagerPosition1', NULL),
('ChillManager', 'BestTeam', 'manager2@email.com', 'manager2', NULL, 'ManagerPosition2', NULL);

-- Asignarles el rol 'manager' en la tabla users_roles
INSERT INTO SYSTEM.users_roles (id_user, user_role) 
VALUES 
(6, 'manager'), 
(7, 'manager');


