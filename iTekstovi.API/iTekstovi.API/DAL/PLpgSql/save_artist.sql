/**
 * Save Artist Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 */

CREATE OR REPLACE FUNCTION itvi.save_artist(
	p_name text, 
	p_first_name text,
	p_last_name text, 
	p_about text,
	p_is_visible boolean DEFAULT TRUE,
	p_id bigint DEFAULT -1
) 
 RETURNS integer 
AS $$
	DECLARE affected_rows Integer DEFAULT 0;
BEGIN

	IF (p_id >0 AND 
	(SELECT COUNT(*) from itvi.artist WHERE artist.id = p_id) > 0) THEN -- update row 
		UPDATE itvi."artist" 
		SET "name" = p_name, "first_name" = p_first_name, "last_name" = p_last_name, "about" = p_about,
			 "is_visible" = p_is_visible, "updated" = current_timestamp
		WHERE song.id = p_id;
	ELSE -- Insert new row 
		INSERT 
		INTO itvi."song"("name", "first_name", "last_name", "about", "is_visible", "created", "updated")
		VALUES (p_name, p_first_name, p_last_name, p_about, p_is_visible, current_timestamp, current_timestamp);
	END IF; 

	GET DIAGNOSTICS affected_rows = ROW_COUNT;
	RETURN affected_rows;
END; $$ 
 
LANGUAGE 'plpgsql';