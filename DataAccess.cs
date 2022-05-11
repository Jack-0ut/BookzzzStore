using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BookStore
{
    public class DataAccess
    {
        //List<Book> booksToShowList;
        public List<Book> booksToShowList;
        public static MySqlConnection getConnection()
        {
            string connStr = "Database=bookstore;Data Source=localhost;User Id=root;Password=Eugene_Solovey_2022;";
            MySqlConnection conn = new MySqlConnection(connStr);
            return conn;
        }
        public List<Book> OpenDbFile()
        {
            try
            {
                //connection to BD
                MySqlConnection conn = getConnection();
                MySqlCommand command = new MySqlCommand();
                //request to BD
                string commandString = "SELECT * FROM books;";
                command.CommandText = commandString;
                command.Connection = conn;

                //read answer from BD
                MySqlDataReader reader;
                command.Connection.Open();
                reader = command.ExecuteReader();

                //read BD to show book on the screen 
                booksToShowList = new List<Book>(100);
                int i = 0;
                while (reader.Read())
                {
                    Book book = new Book((string)reader["name"], (int)reader["quantity"], (string)reader["ageRange"], (double)reader["price"]);
                    book.id = (int)reader["id"];
                    booksToShowList.Add(book);
                    //forming list of book from BD
                    //booksToShowList.Add(new Book((int)reader["id"], (string)reader["name"], (int)reader["quantity"], (string)reader["ageRange"], (double)reader["price"]));
                    i += 1;
                }
                reader.Close();
                return booksToShowList;
            }
            catch (Exception ex)
            {
                return new List<Book>();
            }
        }
    }
}
