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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Book> booksToShowList = new List<Book>(100);
        public static Autorization autorization = new Autorization();
        SelectData selectBooksByAgeRange;
        public static DataAccess dataConnection;
        public static string selectedAgeRange;
        public MainWindow()
        {
            InitializeComponent();
            dataConnection = new DataAccess();
            try
            {
                booksToShowList = dataConnection.OpenDbFile();
                for (int i = 0; i < booksToShowList.Count; i++)
                {
                    BookList.Items.Add(booksToShowList[i]);
                }
                Book cheapestBook = new Book();
                cheapestBook = dataConnection.minPrice();
                minPriceBookName.Text = cheapestBook.name;
                MinPriceBookPrice.Text = cheapestBook.price.ToString() + " UAH";
            }catch(Exception ex)
            {
                string errMsg = "";
                errMsg = "Підключіть веб-сервер MySQL та перезавантажте програму";
                MessageBox.Show(ex.Message + char.ConvertFromUtf32(13) + char.ConvertFromUtf32(13) +
                errMsg, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        // Work that done after click on the button "Autorization"
        private void autorizationButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Visibility = Visibility.Collapsed;
        }
        // Actions after click on the "Search" button
        private void searchAgeRangeBooks_Click(object sender, RoutedEventArgs e)
        {
            selectBooksByAgeRange = new SelectData();
            selectedAgeRange = lowAgeTextBox.Text + '-' + highAgeTextBox.Text;
            selectBooksByAgeRange.selectBooksByAge(Convert.ToInt32(lowAgeTextBox.Text), Convert.ToInt32(highAgeTextBox.Text));
            BookList.ItemsSource = selectBooksByAgeRange.selectedBooks;
        }

        private void saveInFileButton_Click(object sender, RoutedEventArgs e)
        {
            selectBooksByAgeRange.writeData(dataConnection.minPrice(), selectBooksByAgeRange.selectedBooks);

        }
    }
}
