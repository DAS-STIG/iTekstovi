/**
 * Delete Song Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 */

CREATE OR REPLACE FUNCTION itvi.delete_song (
   p_id UUID
) 
 RETURNS bigint 
AS $$
   DECLARE affected_rows Integer DEFAULT 0;
BEGIN

   IF (p_id <> uuid_nil() AND 
 	  (SELECT COUNT(*) from itvi.song WHERE song.id = p_id) > 0) THEN 
     	DELETE FROM itvi."song" WHERE song.id = p_id;
   END IF; 
 
   GET DIAGNOSTICS affected_rows = ROW_COUNT;
   RETURN affected_rows;
END; $$ 
 
LANGUAGE 'plpgsql';