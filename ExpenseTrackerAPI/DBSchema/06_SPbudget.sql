-- Create budget plan --
DELIMITER //
CREATE PROCEDURE spCreateBudget (
	IN uid BIGINT UNSIGNED,
    IN categoryID BIGINT UNSIGNED,
    IN threshold_amount DECIMAL(10, 2),
    OUT status_code INT
)
BEGIN 
	SET status_code = -1; -- set up for failure
	-- Check if user is valid --
    IF NOT EXISTS (SELECT 1 FROM user_accounts WHERE userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User not found';
	END IF;
    
    -- Check if category code is valid -- 
    IF NOT EXISTS (SELECT 1 FROM expense_categories WHERE cid = categoryID AND userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Category info not found';
	END IF;
    
    INSERT INTO user_budget(userID, categoryID, threshold) VALUES (uid, categoryID, threshold_amount);
    SET status_code = 1; 
END //
DELIMITER ;

-- Delete Budget plan --
DELIMITER //
CREATE PROCEDURE spDeleteBudget (
	IN uid BIGINT UNSIGNED,
    IN budget_id BIGINT UNSIGNED,
    OUT status_code INT
)
BEGIN 
	SET status_code = -1; -- set up for failure
	
    -- Check if user is valid --
    IF NOT EXISTS (SELECT 1 FROM user_accounts WHERE userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User not found';
	END IF;

	-- Check if transaction id is valid --
    IF NOT EXISTS (SELECT 1 FROM user_budget WHERE bid = budget_id AND userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Budget Information not found';
	END IF;
    
    DELETE FROM user_budget WHERE bid = budget_id AND userID = uid;
    SET status_code = 1;
END //
DELIMITER ;

-- Edit Budget Plan --
DELIMITER //
CREATE PROCEDURE spEditBudgetThreshold (
	IN uid BIGINT UNSIGNED,
    IN budget_id BIGINT UNSIGNED,
    IN new_threshold DECIMAL(10, 2),
    OUT status_code INT
)
BEGIN 
	SET status_code = -1; 
    -- Check if user is valid --
    IF NOT EXISTS (SELECT 1 FROM user_accounts WHERE userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User not found';
	END IF;

	-- Check if transaction id is valid --
    IF NOT EXISTS (SELECT 1 FROM user_budget WHERE bid = budget_id AND userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Budget Information not found';
	END IF;
    
    UPDATE user_budget SET threshold = new_threshold WHERE userID = uid AND bid = budget_id;
    SET status_code = 1;
END //
DELIMITER ;
