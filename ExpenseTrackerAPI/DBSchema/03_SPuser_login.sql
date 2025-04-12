DELIMITER //
-- Login Procedure --
CREATE PROCEDURE spLogin (
	IN user_email VARCHAR(255),
    OUT status INT,
    OUT uid BIGINT UNSIGNED,
    OUT user_name VARCHAR(255),
    OUT hashedpw VARCHAR(255) 
    )
BEGIN 
	DECLARE user_exist INT;
	-- check if user exist --
    SELECT COUNT(*) INTO user_exist FROM user_accounts WHERE email = user_email;
    
    -- if user does not exist --
    IF user_exist = 0 THEN 
		SET status = 0;
        SET uid = NULL;
        SET user_name = NULL;
        SET hashedpw = NULL;
	ELSE
		SELECT userID, username, pw INTO uid, user_name, hashedpw FROM user_accounts WHERE email = user_email;
        SET status = 1;
	END IF;
END //

DELIMITER ;