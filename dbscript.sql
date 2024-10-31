CREATE DATABASE CocHupQuizDB;
GO

USE CocHupQuizDB;
GO

CREATE TABLE [user] (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    full_name VARCHAR(100),
    birth_year INT
);

CREATE TABLE category (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(100) NOT NULL,
    category_value INT NOT NULL
);

CREATE TABLE difficulty (
    difficulty_id INT PRIMARY KEY IDENTITY(1,1),
    difficulty_name VARCHAR(50) NOT NULL
);

CREATE TABLE question (
    question_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    category_id INT,
    difficulty_id INT,
    question_text VARCHAR(MAX) NOT NULL,
    correct_answer VARCHAR(255) NOT NULL,
    incorrect_answers VARCHAR(MAX) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES [user](user_id),
    FOREIGN KEY (category_id) REFERENCES category(category_id),
    FOREIGN KEY (difficulty_id) REFERENCES difficulty(difficulty_id)
);

CREATE TABLE record (
    record_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    category_id INT,
    difficulty_id INT,
    high_score INT,
    record_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES [user](user_id),
    FOREIGN KEY (category_id) REFERENCES category(category_id),
    FOREIGN KEY (difficulty_id) REFERENCES difficulty(difficulty_id)
);
GO

INSERT INTO [user] (username, [password], full_name, birth_year) VALUES
    ('user', '123', 'Default User', 2003);
GO

INSERT INTO category (category_name, category_value) VALUES
    ('Any Topic', 0),
    ('General Knowledge', 9),
    ('Entertainment: Books', 10),
    ('Entertainment: Film', 11),
    ('Entertainment: Music', 12),
    ('Entertainment: Musicals & Theatres', 13),
    ('Entertainment: Television', 14),
    ('Entertainment: Video Games', 15),
    ('Entertainment: Board Games', 16),
    ('Science & Nature', 17),
    ('Science: Computers', 18),
    ('Science: Mathematics', 19),
    ('Mythology', 20),
    ('Sports', 21),
    ('Geography', 22),
    ('History', 23),
    ('Politics', 24),
    ('Art', 25),
    ('Celebrities', 26),
    ('Animals', 27),
    ('Vehicles', 28),
    ('Entertainment: Comics', 29),
    ('Science: Gadgets', 30),
    ('Entertainment: Japanese Anime & Manga', 31),
    ('Entertainment: Cartoon & Animations', 32);
GO

INSERT INTO difficulty (difficulty_name) VALUES
    ('Easy'),
    ('Medium'),
    ('Hard');
GO