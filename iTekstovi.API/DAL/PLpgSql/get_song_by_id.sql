/**
 * Example Get Model Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 * Description : This stored procedure can be used as a template for PgSql 
 *               models in DAL 
 */

CREATE OR REPLACE FUNCTION itvi.get_song_by_id (
    p_id UUID
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
	song.id = p_id;
END; $$ 
 
LANGUAGE 'plpgsql';