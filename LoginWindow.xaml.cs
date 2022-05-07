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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Checking whether Editor is fill the correct data
        private void AutorizationCheck()
        {
            if (MainWindow.autorization.isAdmin(loginBox.Text, passwordBox.Text))
            {
                Application.Current.MainWindow.Close();
                EditorWindow editorWindow = new EditorWindow();
                editorWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Введіть коректні дані для авторизації", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            AutorizationCheck();
        }
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AutorizationCheck();
            }
        }
        private void LoginForm_Closed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
        }

    }
}
