/**
 * Example Delete Model Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 * Description : This stored procedure can be used as a template for PgSql 
 *               models in DAL 
 */

CREATE OR REPLACE FUNCTION delete_model_name (
   p_model_id UUID
) 
 RETURNS bigint 
AS $$
   DECLARE affected_rows Integer DEFAULT 0;
BEGIN

   IF (p_model_id <> uuid_nil() AND 
 	  (SELECT COUNT(*) from model_name WHERE model_name.model_id = p_model_id) > 0) THEN 
     	DELETE FROM public."model_name" WHERE model_name.model_id = p_model_id;
   END IF; 
 
   GET DIAGNOSTICS affected_rows = ROW_COUNT;
   RETURN affected_rows;
END; $$ 
 
LANGUAGE 'plpgsql';