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

namespace AnunturiDesktop.Pages
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            var publishWindow = new PublishAnuntWindow();
            publishWindow.Show();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new RegisterPage());
        }

        private void Sms_Click(object sender, RoutedEventArgs e)
        {
            var smsWindow = new SmsWindow();
            smsWindow.Show();
        }


        private void Users_Click(object sender, RoutedEventArgs e)
        {
            App.ParentWindowRef.ParentFrame.Navigate(new UsersPage());
        }
    }
}
