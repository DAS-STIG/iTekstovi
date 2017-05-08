/**
 * Generate Script for itekstovi_db and Tables
 * -------------------------------------------------------
 * Author      : Addison B. 
 * SqlType     : PostgreSql 
 */

-- User: itekstovi_admin
-- DROP USER itekstovi_admin;

CREATE USER itekstovi_admin WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  CREATEDB
  NOCREATEROLE
  NOREPLICATION;
  
-- Database: itekstovi_db

-- DROP DATABASE "itekstovi_db";

CREATE DATABASE "itekstovi_db"
    WITH 
    OWNER = "itekstovi_admin"
    ENCODING = 'UTF8'
    LC_COLLATE = 'C'
    LC_CTYPE = 'C'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

-- SCHEMA: itvi

-- DROP SCHEMA itvi ;

CREATE SCHEMA itvi
    AUTHORIZATION postgres;

GRANT ALL ON SCHEMA itvi TO itekstovi_admin;

GRANT ALL ON SCHEMA itvi TO postgres;

ALTER DEFAULT PRIVILEGES IN SCHEMA itvi
GRANT ALL ON TABLES TO itekstovi_admin;

ALTER DEFAULT PRIVILEGES IN SCHEMA itvi
GRANT SELECT, USAGE ON SEQUENCES TO itekstovi_admin;

ALTER DEFAULT PRIVILEGES IN SCHEMA itvi
GRANT EXECUTE ON FUNCTIONS TO itekstovi_admin;

ALTER DEFAULT PRIVILEGES IN SCHEMA itvi
GRANT USAGE ON TYPES TO itekstovi_admin;

-- Table: itvi.artist

-- DROP TABLE itvi.artist;

CREATE TABLE itvi.artist
(
    id bigserial NOT NULL,
    first_name text COLLATE pg_catalog."default",
    last_name text COLLATE pg_catalog."default",
    about text COLLATE pg_catalog."default",
    created timestamp without time zone,
    updated timestamp without time zone,
    is_visible boolean NOT NULL,
    CONSTRAINT artist_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE itvi.artist
    OWNER to itekstovi_admin;
    
-- Table: itvi.song

-- DROP TABLE itvi.song;

CREATE TABLE itvi.song
(
    id uuid NOT NULL,
    name text COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    lyrics text COLLATE pg_catalog."default",
    created timestamp without time zone DEFAULT now(),
    updated timestamp without time zone,
    is_visible boolean,
    artist_id bigint NOT NULL,
    CONSTRAINT song_id_pk PRIMARY KEY (id),
    CONSTRAINT artist_id_fk FOREIGN KEY (artist_id)
        REFERENCES itvi.artist (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE itvi.song
    OWNER to itekstovi_admin;
