/**
 * Example List Model Stored Procedure 
 * ----------------------------------------
 * Author      : Addison B. 
 * Description : This stored procedure can be used as a template for PgSql 
 *               models in DAL 
 */

CREATE OR REPLACE FUNCTION list_model_name (
	p_only_visible BOOLEAN DEFAULT true
) 
 RETURNS TABLE (
 model_id UUID,
 name Text,
 description Text,
 created timestamp with time zone,
 updated timestamp with time zone
) 
AS $$
BEGIN
	IF (p_only_visible) THEN
		RETURN QUERY SELECT 
			model_name.model_id, 
			model_name.name, 
			model_name.description, 
			model_name.created,
			model_name.updated
		FROM 
			public.model_name
		WHERE
			model_name.is_visible = p_only_visible;
	ELSE
		 RETURN QUERY SELECT 
			model_name.model_id, 
			model_name.name, 
			model_name.description, 
			model_name.created,
			model_name.updated		
		FROM 
			public.model_name;
	END IF;
END; $$ 
 
LANGUAGE 'plpgsql';