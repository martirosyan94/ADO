using System;
using System.Data.SqlClient;

namespace AdoLearning
{



    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=training;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //Connection connection = new Connection(connectionString);
            //connection.ConnectV2();
            //connection.Insert();
            //connection.InsertWithParameters();
            //Console.WriteLine(connection.GetRowsCountInEmployeeTable());
            //connection.SqlTransactionExample();

            // Read data
            DataReader dataReader = new DataReader(connectionString);
            //dataReader.GetEmpTblData();

            // example 2
            connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=shopDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; ;
            dataReader.GetInnerJoinExample();
            Console.ReadKey();
        }

    }
}

// Database structure
//USE training


//CREATE TABLE employee (
//	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
//    first_name NVARCHAR(30),
//	last_name NVARCHAR(30),
//	age INT
//)

//CREATE TABLE email (
//	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
//    addr NVARCHAR(30)
//)

//INSERT INTO employee
//values 
//('Ilena', 'Martin' ,27),
//('Bob', 'Smith', 27),
//('Bob', 'Johnson', 25),
//('Jack', 'Jane', 27)

//INSERT INTO email
//values 
//('@Ilena'),
//('@Bob'),
//('@Bob.Johnson'),
//('@Jack')

//SELECT* FROM employee
//SELECT * FROM email







// many to many
/*
CREATE DATABASE shopDb
USE shopDb

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
	FOREIGN KEY(store_id) REFERENCES store(id),
	PRIMARY KEY(book_id, store_id)
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

SELECT book.id, book.name, store.addr
FROM book
INNER JOIN book_store
ON book.id = book_store.book_id
INNER JOIN store
ON store.id = book_store.store_id
*/