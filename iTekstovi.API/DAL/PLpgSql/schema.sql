/**
 * Generate Script for itekstovi_db and Tables
 * -------------------------------------------------------
 * Author      : Addison B. 
 * SqlType     : PostgreSql 
 */

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
    CONSTRAINT "song_id_pk" PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE itvi.song
    OWNER to "itekstovi_admin";

-- Table: itvi.artist

-- DROP TABLE itvi.artist;

CREATE TABLE itvi.artist
(
    id integer NOT NULL DEFAULT nextval('itvi.artist_id_seq'::regclass),
    first_name text COLLATE pg_catalog."default",
    last_name text COLLATE pg_catalog."default",
    about text COLLATE pg_catalog."default",
    created timestamp without time zone,
    updated timestamp without time zone,
    CONSTRAINT artist_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE itvi.artist
    OWNER to itekstovi_admin;
