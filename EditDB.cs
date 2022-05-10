using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace BookStore
{
    class EditDB
    {
        public int bookNum { get; set; }
        public bool addBook { get; set; }
        public void editDbRecord(Book bookToEdit)
        {
            MySqlConnection conn = DataAccess.getConnection();
            conn.Open();
            try
            {
                // Connect to DB and change record with desired index 
                string updateCommand = $"UPDATE `bookstore`.`books` SET `name` = '{bookToEdit.name}',`quantity` = {bookToEdit.quantity},`ageRange` = '{bookToEdit.ageRange}',`price` = {bookToEdit.price} WHERE(`id` = '{bookNum}');";
                MySqlCommand cmd = new MySqlCommand(updateCommand, conn);
                cmd.ExecuteNonQuery();
            }catch(Exception ex) { 
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) +
                "Помилка оновлення БД", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /*public void addDBRecord(string new_name,int new_quantity,string new_ageRange,double new_price)
        {
            // Connect to DB and change record with desired index 
            MySqlConnection conn = new MySqlConnection();
            conn.Open();
            conn.ConnectionString = MainWindow.dataConnection.connStr;
            string insertCommand = "Insert into books (name,quantity,ageRange,price)"
                                 + " values (name, @quantitu, @ageRange,@price) ";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = insertCommand;

            MySqlParameter name = cmd.Parameters.Add("@name", MySqlDbType.VarChar,45);
            name.Value = new_name;

            MySqlParameter quantity = cmd.Parameters.Add("@quantity", MySqlDbType.Int32);
            quantity.Value = new_quantity;

            MySqlParameter ageRange = cmd.Parameters.Add("@ageRange", MySqlDbType.VarChar,10);
            ageRange.Value = new_ageRange;

            MySqlParameter price = cmd.Parameters.Add("@price", MySqlDbType.Double);
            price.Value = new_price;

            cmd.ExecuteNonQuery();
        }*/
        public void addDBRecord(Book newBook)
        {
            MySqlConnection conn = DataAccess.getConnection();
            conn.Open();
            try{
                string insertCommand = $"INSERT INTO `bookstore`.`books` (`name`, `quantity`, `ageRange`, `price`) VALUES ('{newBook.name}', '{newBook.quantity}', '{newBook.ageRange}', '{newBook.price}');";
                MySqlCommand cmd = new MySqlCommand(insertCommand, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) +
                "Помилка запису в БД", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
