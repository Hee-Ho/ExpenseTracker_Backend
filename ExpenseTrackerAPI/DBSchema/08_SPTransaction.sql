-- Inserting transaction --
/* To get the error message in backend
catch (MySqlException ex)
        {
            // Handle MySQL-specific errors
            if (ex.SqlState == "45000")  // Custom MySQL errors from SIGNAL SQLSTATE
            {
                return (false, ex.Message);
            }

            return (false, "Database error: " + ex.Message); 
*/
DELIMITER //
CREATE PROCEDURE spInsertTransaction(
	IN uid BIGINT UNSIGNED,
    IN category_id BIGINT UNSIGNED,
    IN saving_id BIGINT UNSIGNED,
    IN transaction_amount DECIMAL(10, 2),
    IN descrip TEXT,
    IN transaction_date DATE, 
    OUT status_code INT
)
BEGIN 
	DECLARE user_exist INT default 0;
    DECLARE category_exist INT DEFAULT 0;
	-- check if user exist --
	SELECT COUNT(*) into user_exist FROM user_accounts WHERE userID = uid;
    IF user_exist < 1 THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User not found';
	END IF;
    
    -- CHeck if category exist --
    SELECT COUNT(*) INTO category_exist FROM expense_categories WHERE cid = category_id AND userID = uid;
    IF category_exist < 1 THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Category not found';
	END IF;
    
    INSERT INTO transactions_info(userID, categoryID, amount, description, transaction_date) VALUES (uid, category_id, transaction_amount, descrip, transaction_date);
    SET status_code = 1;
END //
DELIMITER ;

-- Deleting Transaction --
DELIMITER //
CREATE PROCEDURE spDeleteTransaction (
	IN uid BIGINT UNSIGNED,
    IN transaction_id BIGINT UNSIGNED,
    OUT status_code INT
)
BEGIN
	DECLARE user_exist INT;
    DECLARE tid_exist INT;
	IF NOT EXISTS (SELECT 1 FROM transactions_info WHERE tid = transaction_id AND userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No transaction record exist';
	ELSE 
		DELETE FROM transactions_info WHERE tid = transaction_id and userID = uid;
        SET status_code = 1;
	END IF;
END //
DELIMITER ;

-- Changing Transaction Information --
-- Need to add option for editing description --
DELIMITER //
CREATE PROCEDURE spUpdateTransaction (
	IN uid BIGINT UNSIGNED,
    IN transaction_id BIGINT UNSIGNED,
    IN new_amount DECIMAL(10, 2),
    IN category_id BIGINT UNSIGNED, 
    OUT status_code INT
)
BEGIN
	-- Check if category exist for user --
    DECLARE check_category BIGINT UNSIGNED;
    SET status_code = -1;
    IF check_category IS NOT NULL AND NOT EXISTS (SELECT 1 FROM expense_categories WHERE cid = category_id AND userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No category type exist';
	END IF;
    
	IF NOT EXISTS (SELECT 1 FROM transactions_info WHERE tid = transaction_id AND userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No transaction record exist';
	END IF;
    
	UPDATE transactions_info SET 
		amount = IF(new_amount IS NOT NULL, new_amount, amount), 
		categoryID = IF(category_id IS NOT NULL, category_id, categoryID)
    WHERE tid = transaction_id AND userID = uid;
	SET status_code = 1;
END //
DELIMITER ;
