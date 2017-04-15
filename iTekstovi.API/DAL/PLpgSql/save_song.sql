/**
 * List Song Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 */

CREATE OR REPLACE FUNCTION itvi.save_song(
	p_name text, 
	p_lyrics text,
	p_description text, 
	p_artist_id int,
	p_is_visible boolean DEFAULT TRUE,
	p_id UUID DEFAULT uuid_nil()
) 
 RETURNS bigint 
AS $$
	DECLARE affected_rows Integer DEFAULT 0;
BEGIN

	IF (p_id <> uuid_nil() AND 
	(SELECT COUNT(*) from itvi.song WHERE song.id = p_id) > 0) THEN -- update row 
		UPDATE itvi."song" 
		SET "name" = p_name, "lyrics" = p_lyrics, "description" = p_description, "artist_id" = p_artist_id,
			 "is_visible" = p_is_visible, "updated" = current_timestamp
		WHERE song.id = p_id;
	ELSE -- Insert new row 
		INSERT 
		INTO itvi."song"("id", "name", "lyrics", "description", "artist_id", "is_visible", "created", "updated")
		VALUES (uuid_generate_v4(), p_name, p_description, p_artist_id, p_is_visible, current_timestamp, current_timestamp);
	END IF; 

	GET DIAGNOSTICS affected_rows = ROW_COUNT;
	RETURN affected_rows;
END; $$ 
 
LANGUAGE 'plpgsql';