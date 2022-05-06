using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BookStore
{
    public class DataAccess{
        public string connStr;
        private void OpenDbFile()
        {
            try
            {
                //connection to BD
                string connStr = "Server = localhost; Database = aviadispetcher; Uid = root; Pwd = Eugene_Solovey_2022;";
                MySqlConnection conn = new MySqlConnection(connStr);
                MySqlCommand command = new MySqlCommand();
                //request to BD
                string commandString = "SELECT * FROM rozklad;";
                command.CommandText = commandString;
                command.Connection = conn;
                //read answer from BD
                MySqlDataReader reader;
                command.Connection.Open();
                reader = command.ExecuteReader();
                //read BD to show book on the screen 
                int i = 0;
                while (reader.Read())
                {
                    //forming list of book from BD
                    booksToShowList.Add(new Book((string)reader["name"], (int)reader["quantity"], (string)reader["ageRange"], (double)reader["price"]));
                    i += 1;
                }
                reader.Close();
                //pass all books into list "BookList" on the form "InfoBookForm"
                BookList.ItemsSource = booksToShowList;
            }
            catch (Exception ex)
            {
                string errMsg = "";
                errMsg = "Підключіть веб-сервер MySQL та виконайте команду Файл-Завантажити";
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) +
                                errMsg, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}