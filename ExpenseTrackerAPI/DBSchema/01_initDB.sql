-- CREATE DATABASE expense_tracker;
USE expense_tracker;

-- User Table----------
CREATE TABLE User_accounts (
userID SERIAL PRIMARY KEY, -- SERIAL is BIGINT UNSIGNED NOT NULL AUTO ICNREMENT --
username VARCHAR(255) NOT NULL UNIQUE,
pw VARCHAR(255) NOT NULL, 
email VARCHAR(255) NOT NULL UNIQUE
);

-- User Expense Categories ------ 
CREATE TABLE Expense_categories (
	cid SERIAL PRIMARY KEY,
    userID BIGINT UNSIGNED NOT NULL, 
    category_name VARCHAR(255) NOT NULL,
    FOREIGN KEY (userID) REFERENCES User_accounts(userID) on DELETE CASCADE
);

-- May have to change to allow tracking for saving
-- Expense and Income Logging ------
CREATE TABLE Transactions_info (
	tid SERIAL PRIMARY KEY,
	userID BIGINT UNSIGNED NOT NULL,
    amount DECIMAL(10, 2) NOT NULL, 
    categoryID BIGINT UNSIGNED,
    description TEXT NULL,
    transaction_date DATE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY(userID) REFERENCES User_accounts(userID) on DELETE CASCADE,
    FOREIGN KEY(categoryID) REFERENCES Expense_categories(cid) on DELETE SET NULL
);
-- Total income will be in stored procedure -----

-- Expense Budget ------
CREATE TABLE User_Budget (
	bid SERIAL PRIMARY KEY,
    userID BIGINT UNSIGNED NOT NULL,
    categoryID BIGINT UNSIGNED,
    threshold DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY(userID) REFERENCES User_accounts(userID) on DELETE CASCADE,
    FOREIGN KEY(categoryID) REFERENCES Expense_categories(cid) on DELETE SET NULL
);

-- Expense Saving Goal -----
CREATE TABLE User_saving (
	sid SERIAL PRIMARY KEY, 
    userID BIGINT UNSIGNED NOT NULL,
    goal_name VARCHAR(255),
    goal_amount DECIMAL(10, 2) NOT NULL, 
    FOREIGN KEY(userID) REFERENCES User_accounts(userID) on DELETE CASCADE
);

CREATE TABLE Saving_transaction (
	stid SERIAL PRIMARY KEY,
    savingID BIGINT UNSIGNED NOT NULL,
    amount DECIMAL(10, 2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY(savingID) REFERENCES User_saving(sid) on DELETE CASCADE
);
