using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoLearning
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Connect()
        {
            // Create connection object
            SqlConnection connection = new SqlConnection(_connectionString);
            
            // Open connection
            connection.Open();

            // get connection info
            GetConnectionInfo(connection);

            // close and dispose connection
            connection.Close();
            connection.Dispose();
        }
        public void ConnectV2()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                GetConnectionInfo(connection);
            }
        }
        public void GetConnectionInfo(SqlConnection connection)
        {
            Console.WriteLine(connection.ConnectionString);
            Console.WriteLine(connection.ConnectionTimeout);
            Console.WriteLine(connection.State.ToString());
            Console.WriteLine(connection.Database);
            Console.WriteLine(connection.DataSource);
        }
        public int GetRowsCountInEmployeeTable()
        {
            string sql = "SELECT COUNT(*) FROM employee";
            int count;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    count = (int)command.ExecuteScalar(); 
                }
            }
            return count;
        }
        public void Insert()
        {
            string sql = "INSERT INTO employee(first_name, last_name, age)";
            sql += "VALUES('Ilena', 'Martin' ,27)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    //command.CommandType = CommandType.Text;
                    Console.WriteLine(command.ExecuteNonQuery());
                    //reset command text
                    //command.CommandText = newcommand
                }
            }
        }

        // with parameters
        public void InsertWithParameters() 
        {
            Employee employee = new Employee() {First_Name = "Kari", Last_Name = "Smith", Age = 24 };
            string sql_query = "INSERT INTO employee(first_name, last_name, age)";
            sql_query += "VALUES(@first_name, @last_name,@age)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql_query, connection))
                {

                    command.Parameters.Add(new SqlParameter("@first_name", employee.First_Name));
                    command.Parameters.Add(new SqlParameter("@last_name", employee.Last_Name));
                    command.Parameters.Add(new SqlParameter("@age", employee.Age));

                    //command.CommandType = CommandType.Text;

                    connection.Open();
                    
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("successfully Inserted with parameters");
        }
        // transaction processing'
        public void SqlTransactionExample()
        {
            // create data
            Employee employee = new Employee() { First_Name = "Ani", Last_Name = "Mamyan", Age = 27 };
            Email email = new Email() { Addr = "Mart@gmail.com" };

            // sql connection
            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                string sql_query = "INSERT INTO employee(first_name, last_name, age)";
                sql_query += "VALUES(@first_name, @last_name,@age)";

                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand command = new SqlCommand(sql_query, connection))
                    {

                        //set transaction to command
                        command.Transaction = transaction;

                        command.Parameters.Add(new SqlParameter("@first_name", employee.First_Name));
                        command.Parameters.Add(new SqlParameter("@last_name", employee.Last_Name));
                        command.Parameters.Add(new SqlParameter("@age", employee.Age));

                        command.ExecuteNonQuery();
                        
                        // second query
                        sql_query = "INSERT INTO email(addr)";
                        sql_query += "VALUES(@addr)";

                        command.CommandText = sql_query;

                        command.Parameters.Clear();

                        command.Parameters.Add(new SqlParameter("@addr", email.Addr));

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        Console.WriteLine("Transaction successfully complited");

                    }
                }
            }
            // transaction
            // command



            // inside command
            // add parameters
            // transaction
            // run


            // do same for other command

        }
    }

    public class Employee
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
    }

    public class Email
    {
        public string Addr { get; set; }
    }
}
