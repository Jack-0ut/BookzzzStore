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
        public List<Book> booksToShowList = new List<Book>(100);
        EditDB editedRow = new EditDB();
        Book editedBook;
        DataAccess dataConnection = MainWindow.dataConnection;
        public EditorWindow()
        {
            InitializeComponent();
            MainWindow.dataConnection.OpenDbFile();
            this.BookList.ItemsSource = MainWindow.dataConnection.booksToShowList;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            
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
                MessageBox.Show("Error!");
            }

        }
        private void changeBookListData(int num)
        {
            editedBook.name = nameValue.Text;
            editedBook.quantity = Convert.ToInt32(quantityValue.Text);
            editedBook.ageRange = ageRangeValue.Text;
            editedBook.price = Convert.ToDouble(priceValue.Text);
            if (editedRow.addBook)
            {
                this.dataConnection.booksToShowList.Add(editedBook);
            }
            else
            {
                this.dataConnection.booksToShowList[num] = editedBook;
            }
            BookList.ItemsSource = null;
            BookList.ItemsSource = this.dataConnection.booksToShowList;
        }

        private void SaveRedDB_Click(object sender, RoutedEventArgs e)
        {
            //editedBook = new Book("Пізнавальна математика", 10, "12-18", 300.00);
            //changeBookListData(editedRow.bookNum);
            if (editedRow.addBook == true)
            {
                editedBook = new Book(nameValue.Text,int.Parse(quantityValue.Text),ageRangeValue.Text,double.Parse(priceValue.Text));
                dataConnection.OpenDbFile();
                this.BookList.ItemsSource = dataConnection.booksToShowList;
                editedRow.addDBRecord(editedBook);
            }
            else
            {
                dataConnection.OpenDbFile();
                this.BookList.ItemsSource = dataConnection.booksToShowList;
                editedRow.editDbRecord(editedBook);
            }
        }
        private void exitEditModeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }


    }
}
