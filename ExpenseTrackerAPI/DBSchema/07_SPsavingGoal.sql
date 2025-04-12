DELIMITER // 
-- Create a saving goal --
CREATE PROCEDURE spCreateSavingGoal (
	IN uid BIGINT UNSIGNED,
    IN goalName VARCHAR(255),
    IN amount DECIMAL(10, 2),
    OUT status_code INT	
)
BEGIN
	SET status_code = -1;
    
    IF NOT EXISTS (SELECT 1 FROM user_accounts WHERE userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User not found';
	END IF; 
	
    INSERT INTO user_saving(userID, goal_name, goal_amount) VALUES(uid, goalName, amount);
    SET status_code = 1;
END //
DELIMITER ;

-- Delete a saving goal -- 
DELIMITER // 
CREATE PROCEDURE spDeleteSavingGoal (
	IN uid BIGINT UNSIGNED,
    IN saving_ID BIGINT UNSIGNED,
    OUT status_code INT
)
BEGIN 
	SET status_code = -1;
    
    -- Check if user exist and if saving plan exist --
    IF NOT EXISTS (SELECT 1 FROM user_accounts WHERE userID = uid) THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User not found';
	END IF; 
    
    IF NOT EXISTS (SELECT 1 FROM user_saving WHERE sid = saving_ID and userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Saving plan not found';
	END IF; 
	
    DELETE FROM user_saving WHERE sid = saving_ID and userID = uid;
    SET status_code = 1; 
END //
DELIMITER ;

-- Edit Saving Goal --
-- Change the the goal amount and/or description/name of the plan --
DELIMITER //
CREATE PROCEDURE spEditSavingGoal (
	IN uid BIGINT UNSIGNED,
    IN saving_ID BIGINT UNSIGNED,
    IN new_amount DECIMAL(10, 2),
    IN new_goalName VARCHAR(255), 
    OUT status_code INT
)
BEGIN 
    SET status_code = -1;
     -- Check if user exist and if saving plan exist --
    IF NOT EXISTS (SELECT 1 FROM user_accounts WHERE userID = uid) THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'User not found';
	END IF; 
    
    IF NOT EXISTS (SELECT 1 FROM user_saving WHERE sid = saving_ID and userID = uid) THEN 
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Saving plan not found';
	END IF; 
    
    -- Dynamic update based on the field that is provided --
    UPDATE user_saving SET 
		goal_amount = IF( new_amount IS NOT NULL, new_amount, goal_amount),
        goal_name = IF(new_goalName IS NOT NULL, new_goalName, goal_name)
	WHERE sid = saving_ID and userID = uid;
    SET status_code = 1;
END //
DELIMITER ;

-- Saving Goal Transaction Store Procedure

-- Create a transaction toward a saving goal --
DELIMITER //

DELIMITER ;

-- Delete a saving transaction --


-- Edit the saving transaction --




