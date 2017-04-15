/**
 * List Song Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 */

CREATE OR REPLACE FUNCTION itvi.list_song (
	p_only_visible boolean DEFAULT TRUE
) 
 RETURNS TABLE (
 id UUID,
 name text,
 lyrics text,
 description text,
 artist_id bigint,
 is_visible boolean,
 created timestamp with time zone,
 updated timestamp with time zone
) 
AS $$
BEGIN
	IF (p_only_visible) THEN
		RETURN QUERY SELECT 
			song.id, 
			song.name, 
			song.lyrics,
			song.description, 
			song.is_visible,
			song.created,
			song.updated
		FROM 
			itvi.song
		WHERE
			song.is_visible = p_only_visible;
	ELSE
		 RETURN QUERY SELECT 
			song.id, 
			song.name, 
			song.lyrics,
			song.description, 
			song.is_visible,
			song.created,
			song.updated		
		FROM 
			itvi.song;
	END IF;
END; $$ 
 
LANGUAGE 'plpgsql';