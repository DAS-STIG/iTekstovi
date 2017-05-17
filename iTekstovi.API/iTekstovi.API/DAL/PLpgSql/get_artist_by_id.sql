/**
 * Example Get Artist Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 */

CREATE OR REPLACE FUNCTION itvi.get_artist_by_id (
    p_id bigint
) 
 RETURNS TABLE (
	id bigint,
	first_name text,
	last_name text,
	about text,
	song_id UUID,
	is_visible boolean,
	created timestamp,
	updated timestamp
) 
AS $$
BEGIN
 RETURN QUERY SELECT 
 	artist.id, 
	artist.first_name, 
	artist.last_name, 
	artist.about, 
	song.id as "song_id",
	artist.is_visible,
	artist.created,
	artist.updated
FROM 
	itvi.artist left outer join itvi.song ON artist.id = song.artist_id
WHERE
	artist.id = p_id;
END; $$ 
 
LANGUAGE 'plpgsql';