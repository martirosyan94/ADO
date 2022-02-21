drop database training
CREATE DATABASE training
USE training
-- DROP TABLE employee
-- DROP TABLE email
CREATE TABLE employee (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	first_name NVARCHAR(30),
	last_name NVARCHAR(30),
	age INT
)

INSERT INTO employee
values 
('Ilena', 'Martin' ,27),
('Bob', 'Smith', 27),
('Bob', 'Johnson', 25),
('Jack', 'Jane', 27)


SELECT DISTINCT first_name FROM employee

UPDATE employee SET first_name = 'Liana' where ID = 2
SELECT first_name FROM employee

SELECT  
COUNT(first_name), first_name, last_name
FROM employee
GROUP BY first_name, last_name
HAVING COUNT(first_name) > 0

SELECT  first_name
FROM employee
ORDER BY age

-- DROP TABLE email
CREATE TABLE email (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	addr NVARCHAR(30),
	fk_employee_id INT
	FOREIGN KEY (fk_employee_id) 
	REFERENCES employee(id)
	-- UNIQUE
)

-- with constraints -------------------------------------
CREATE TABLE email (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	addr NVARCHAR(30),
	employee_id INT,
	CONSTRAINT FK_employee
	FOREIGN KEY (employee_id) -- unique vonc anel??
	REFERENCES employee(id)
)
---------------------------------------------------------

INSERT INTO email
values 
('@Ilena', NULL),
('@Bob', NULL), -- NULL-ն էլ ա դուփլիքյեթ համարում
('@Bob.Johnson', 3),
('@Jack', 4)

SELECT * FROM email

INSERT INTO email
values 
('@Bob.Johnsonv2', 3)

SELECT first_name, last_name, addr
FROM employee
FULL OUTER JOIN email
ON employee.id = email.fk_employee_id



-- Many to Many BookStore --

CREATE TABLE book (
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	name NVARCHAR(30),
	author NVARCHAR(30)
)

CREATE TABLE store (
	id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	addr NVARCHAR(30)
)

CREATE TABLE book_store(
	book_id INT NOT NULL,
	store_id INT NOT NULL,
	book_price INT,
	FOREIGN KEY (book_id) REFERENCES book(id),
	FOREIGN KEY (store_id) REFERENCES store(id),
	PRIMARY KEY (book_id, store_id)
)

INSERT INTO book
VALUES
('Erkate krunky', 'Jack London'),
('Karamazov Exbayrnery', 'Dostoevsky'),
('Spitak Zhaniqy', 'Dostoevsky')

INSERT INTO store
VALUES
('Moskovyan Bookinist'),
('Metronom Bookinist'),
('Abovyan Bookinist')

INSERT INTO book_store
VALUES
(1, 1, 1200),
(1, 2, 1200),
(2, 2, 1500),
(3, 3, 1400)

SELECT book.id, book.name, store.addr, book_store.book_price
FROM book
INNER JOIN book_store
ON book.id = book_store.book_id
INNER JOIN store
ON store.id = book_store.store_id