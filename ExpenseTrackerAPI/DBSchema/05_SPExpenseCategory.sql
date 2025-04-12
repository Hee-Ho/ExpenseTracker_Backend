-- Procedure for inserting user's own category --
DELIMITER //
-- Check if user ID is valid, then check if the categories exist -- 
CREATE PROCEDURE spInsertExpenseCategory (
	IN uid BIGINT UNSIGNED, 
    IN category VARCHAR(255),
    OUT status_code INT
)
BEGIN 
	DECLARE user_exist INT;
    SET status_code = -1;
    
    SELECT COUNT(*) into user_exist FROM user_accounts WHERE userID = uid;
    IF user_exist < 1 THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Account does not exist';
	ELSE 
		INSERT INTO expense_categories(userID, category_name) VALUES (uid, category);
        SET status_code = 1;
	END IF;
END //

DELIMITER ;


-- Procedure for deleting a user's category --
DELIMITER //
-- Check if user_exist
CREATE PROCEDURE spDeleteExpenseCategory (
	IN uid BIGINT UNSIGNED,
    IN category_id BIGINT UNSIGNED, 
    OUT status_code INT
)
BEGIN
	DECLARE user_exist INT;
    DECLARE cid_exist INT;
    SET status_code = -1;
    
	SELECT COUNT(*) into user_exist FROM user_accounts WHERE userID = uid;
    IF user_exist < 1 THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Account does not exist';
	ELSE
		SELECT COUNT(*) INTO cid_exist FROM expense_categories WHERE userID = uid AND cid = category_id;
        IF cid_exist < 1 THEN
			SIGNAL SQLSTATE '45000'
			SET MESSAGE_TEXT = "User's category does not exist";
		ELSE 
			DELETE FROM expense_categories WHERE userID = uid AND cid = category_id;
            SET status_code = 1;
		END IF;
	END IF;
END //
DELIMITER ;


-- Edit category name -- 
DELIMITER //
CREATE PROCEDURE spEditCategory (
	IN uid BIGINT UNSIGNED, 
    IN category_id BIGINT UNSIGNED,
    IN new_name VARCHAR(255), 
    OUT status_code INT
)
BEGIN
	SET status_code = 0;
	IF NOT EXISTS (SELECT 1 FROM expense_categories WHERE cid = category_id AND userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = "User's category does not exist";
	ELSE 
		UPDATE expense_categories SET category_name = new_name WHERE cid = category_id AND userID = uid;
        SET status_code = 1;
	END IF;
END //
DELIMITER ;