/**
 * List Artist Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 */

CREATE OR REPLACE FUNCTION itvi.list_artist (
	p_only_visible boolean DEFAULT TRUE
) 
RETURNS TABLE (
  id bigint,
  first_name text,
  last_name text,
  about text,
  is_visible boolean,
  created timestamp with time zone,
  updated timestamp with time zone
) 
AS $$
BEGIN
	IF (p_only_visible) THEN
		RETURN QUERY SELECT 
            artist.id, 
            artist.first_name, 
            artist.last_name, 
            artist.about, 
            artist.is_visible,
            artist.created,
            artist.updated
		FROM 
			itvi.artist
		WHERE
			artist.is_visible = p_only_visible;
	ELSE
		 RETURN QUERY SELECT 
            artist.id, 
            artist.first_name, 
            artist.last_name, 
            artist.about, 
            artist.is_visible,
            artist.created,
            artist.updated
		FROM 
			itvi.artist;
	END IF;
END; $$ 
 
LANGUAGE 'plpgsql';