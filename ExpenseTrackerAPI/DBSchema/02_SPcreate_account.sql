DELIMITER //
-- Create Account --
CREATE PROCEDURE spCreateAccount(
    IN user_email VARCHAR(255), 
    IN user_name VARCHAR(255),
    IN user_password VARCHAR(128), -- password will be hashed
    OUT status INT,
    OUT uid BIGINT UNSIGNED
)
BEGIN
    DECLARE email_exists INT;
    DECLARE username_exists INT;
    
    account_creation:BEGIN -- label for block --
    
    -- Check if email already exists (case-insensitive)
    SELECT COUNT(*) INTO email_exists FROM user_accounts WHERE LOWER(email) = LOWER(user_email);
    IF email_exists > 0 THEN
        SET status = 0;
        LEAVE account_creation; -- early exit --
    END IF;

    -- Check if username already exists (case-insensitive)
    SELECT COUNT(*) INTO username_exists FROM user_accounts WHERE LOWER(username) = LOWER(user_name);
    IF username_exists > 0 THEN
        SET status = 0;
        LEAVE account_creation;
    END IF;

    -- Start transaction for atomicity
    START TRANSACTION;
    INSERT INTO user_accounts(username, pw, email) VALUES (user_name, user_password, user_email);

    -- Verify insertion before selecting
    IF ROW_COUNT() > 0 THEN
        SET status = 1;
        SELECT userID INTO uid FROM user_accounts WHERE email = user_email;
        COMMIT;
    ELSE
        SET status = -1;
        ROLLBACK;
    END IF;
    
    END account_creation;
END //

DELIMITER ;