using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoLearning
{
    public class DataReader
    {
        private readonly string _connection_string;
        public DataReader(string connection_string)
        {
            _connection_string = connection_string;
        }

        public void GetEmpTblData()
        {
            using (SqlConnection connection = new SqlConnection(_connection_string))
            {
                string sql = "SELECT first_name, last_name, age FROM employee";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            Console.WriteLine("\t{0}\t{1}", reader.GetName(0),
                                reader.GetName(1));

                            while (reader.Read())
                            {
                                Console.WriteLine("\t{0}\t{1}", reader.GetString(0),
                                    reader.GetString(1));
                            }
                            reader.NextResult();
                        }
                    }
                }
            }
        }

        public void GetInnerJoinExample()
        {
            using (SqlConnection connection = new SqlConnection(_connection_string))
            {
                string sql = "SELECT book.id, book.name, store.addr" +
                " FROM book" +
                " INNER JOIN book_store" +
                " ON book.id = book_store.book_id" +
                " INNER JOIN store" +
                " ON store.id = book_store.store_id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.HasRows)
                        {
                            Console.WriteLine("\t{0}\t{1}\t{2}", reader.GetName(0),
                                reader.GetName(1), reader.GetName(2));

                            while (reader.Read())
                            {
                                Console.WriteLine("\t{0}\t{1}\t{2}", reader.GetInt32(0),
                                    reader.GetString(1), reader.GetString(2));
                            }
                            reader.NextResult();
                        }
                    }
                }
            }
        }
    }
}
