using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        EditDB editedRow = new EditDB();
        Book editedBook;
        DataAccess dataConnection = MainWindow.dataConnection;
        public EditorWindow()
        {
            InitializeComponent();
            this.dataConnection.OpenDbFile();
            this.BookList.ItemsSource = this.dataConnection.booksToShowList;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            editedRow.addBook = false;
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            nameValue.Text = "";
            quantityValue.Text = "";
            ageRangeValue.Text = "";
            priceValue.Text = "";
            editedRow.addBook = true;
            editedRow.bookNum = MainWindow.dataConnection.booksToShowList.Count;
            if (editedRow.bookNum >= 100)
            {
                editedRow.addBook = false;
                MessageBox.Show("К-ть записів перевищує ліміт!Оберіть запис,який хочете видалити", "Увага!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Book editedBook = BookList.SelectedItem as Book;
            editedRow.bookNum = BookList.SelectedIndex;
            editedRow.bookID = editedBook.id;
            editedRow.addBook = false;
            try
            { 
                nameValue.Text = editedBook.name;
                quantityValue.Text = Convert.ToString(editedBook.quantity);
                ageRangeValue.Text = editedBook.ageRange;
                priceValue.Text = Convert.ToString(editedBook.price);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13),"",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            editedRow = new EditDB();
            editedRow.addBook = false;
            editedRow.bookNum = BookList.SelectedIndex;
            editedRow.bookID = editedBook.id;
        }
        // Actions after User push button Save in GroupBox "Book"
        private void SaveRedDB_Click(object sender, RoutedEventArgs e)
        {
            if ((BookList.SelectedIndex < 0) && (!editedRow.addBook))
            {
                MessageBox.Show("Оберіть рядок для редагування подвійним кліком", "Увага!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            editedBook = new Book(nameValue.Text, int.Parse(quantityValue.Text), ageRangeValue.Text, double.Parse(priceValue.Text));
            if (editedRow.addBook == true)
            {
                editedRow.addDBRecord(editedBook);
            }
            else
            {
                editedRow.editDbRecord(editedBook);
                this.dataConnection.booksToShowList[editedRow.bookNum] = editedBook;

            }
            BookList.ItemsSource = null;
            BookList.ItemsSource = this.dataConnection.OpenDbFile();
        }

        private void exitEditModeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
