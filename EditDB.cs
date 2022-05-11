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
        public int bookID { get; set; }
        public bool addBook { get; set; }
        public void editDbRecord(Book bookToEdit)
        {
            MySqlConnection conn = DataAccess.getConnection();
            conn.Open();
            try
            {
                // Connect to DB and change record with desired index 
                string updateCommand = $"UPDATE `bookstore`.`books` SET `name` = '{bookToEdit.name}',`quantity` = '{bookToEdit.quantity.ToString()}',`ageRange` = '{bookToEdit.ageRange}',`price` = '{bookToEdit.price.ToString()}' WHERE(`id` = '{bookID.ToString()}');";
                MySqlCommand cmd = new MySqlCommand(updateCommand, conn);
                cmd.ExecuteNonQuery();
            }catch(Exception ex) { 
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) +
                "Помилка оновлення запису в БД", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
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
