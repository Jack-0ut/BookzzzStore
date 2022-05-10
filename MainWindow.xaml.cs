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
        public static DataAccess dataConnection;
        public MainWindow()
        {
            InitializeComponent();
            dataConnection = new DataAccess();
            try
            {
                booksToShowList = dataConnection.OpenDbFile();
                BookList.ItemsSource = booksToShowList;
            }catch(Exception ex)
            {
                string errMsg = "";
                errMsg = "Підключіть веб-сервер MySQL та виконайте команду Файл-Завантажити";
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

        private void InfoBookForm_Activated(object sender, EventArgs e)
        {

        }
    }
}
