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

        public string connStr;
        public List<Book> OpenDbFile()
        {
            try
            {
                //connection to BD
                //string connStr = "Server=localhost; Database=bookstore; User Id=root; Password=";
                string connStr = "Server=localhost;Database=bookstore;port=3306;User Id=root;password=Eugene_Solovey_2022;";
                MySqlConnection conn = new MySqlConnection(connStr);
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
                List<Book> booksToShowList = new List<Book>(100);
                int i = 0;
                while (reader.Read())
                {
                    //forming list of book from BD
                    booksToShowList.Add(new Book((string)reader["name"], (int)reader["quantity"], (string)reader["ageRange"], (double)reader["price"]));
                    i += 1;
                }
                reader.Close();
                return booksToShowList;
            }
            catch (Exception ex)
            {
                /*string errMsg = "";
                errMsg = "Підключіть веб-сервер MySQL та виконайте команду Файл-Завантажити";
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) +
                                errMsg, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);*/
                return new List<Book>();

            }
        }
    }
}
