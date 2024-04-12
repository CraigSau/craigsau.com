CREATE DATABASE IF NOT EXISTS craigsaudev;

CREATE TABLE IF NOT EXISTS Users (
    id VARCHAR(36) NOT NULL PRIMARY KEY,
    user_name VARCHAR(30) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NULL,
    email VARCHAR(100) NOT NULL,
    email_confirmed TINYINT(1) NOT NULL DEFAULT 0,
    passwordhash VARCHAR(100) NOT NULL,
    created_on DATE NOT NULL,
    CONSTRAINT UC_Users UNIQUE (id, user_name, email)
)
