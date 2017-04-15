CREATE OR REPLACE FUNCTION save_model_name(
	p_name Text, 
	p_description Text, 
	p_model_id UUID DEFAULT uuid_nil()
) 
 RETURNS bigint 
AS $$
	DECLARE affected_rows Integer DEFAULT 0;
BEGIN

	IF (p_model_id <> uuid_nil() AND 
	(SELECT COUNT(*) from model_name WHERE model_name.quiz_id = p_model_id) > 0) THEN -- update row 
		UPDATE public."model_name" 
		SET "name" = p_name, "description" = p_description, "updated" = current_timestamp
		WHERE model_name.quiz_id = p_model_id;
	ELSE -- Insert new row 
		INSERT 
		INTO public."model_name"("model_id", "name", "description")
		VALUES (uuid_generate_v4(), p_name, p_description);
	END IF; 

	GET DIAGNOSTICS affected_rows = ROW_COUNT;
	RETURN affected_rows;
END; $$ 
 
LANGUAGE 'plpgsql';