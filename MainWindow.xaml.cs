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

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Book> booksToShowList = new List<Book>(100);
        public MainWindow()
        {
            InitializeComponent();
            DataAccess access = new DataAccess();
            booksToShowList = access.OpenDbFile();
            BookList.ItemsSource = booksToShowList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void autorizationButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Visibility = Visibility.Collapsed;

        }
    }
}
