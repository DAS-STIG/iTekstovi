/**
 * Example Get Model Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 * Description : This stored procedure can be used as a template for PgSql 
 *               models in DAL 
 */

CREATE OR REPLACE FUNCTION get_model_name_by_id (
    p_model_id UUID
) 
 RETURNS TABLE (
 model_id UUID,
 name Text,
 description Text,
 created timestamp with time zone,
 updated timestamp with time zone,
 attributes JSON
) 
AS $$
BEGIN
 RETURN QUERY SELECT 
 model_name.quiz_id, 
 model_name.name, 
 model_name.description, 
 quiz_type.type,
 quiz_type.type_id,
 model_name.created,
 model_name.updated,
 model_name.attributes
FROM 
	public.model_name 
WHERE
	model_name.quiz_id = p_model_id;
END; $$ 
 
LANGUAGE 'plpgsql';