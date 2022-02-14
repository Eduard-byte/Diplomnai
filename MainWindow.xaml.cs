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
using UIKitTutorials.Entities;
using UIKitTutorials.Helpers;
using UIKitTutorials.Pages;

namespace UIKitTutorials
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Manager.MainFrame = PagesNavigation;

            Manager.MainFrame.Navigate(new HomePage());

            Auth.ImageUser = ImageUser;
            Auth.EmailUser = EmailUser;
            Auth.NameUser = NameUser;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            

            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
           // PagesNavigation.Navigate(new HomePage());
            
            PagesNavigation.Navigate(new System.Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdNotes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new RoomListPage());
        }

        private void rdPayment_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new ReservePage(new Room()));
        }

        private void RdAuth_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Authorization());
        }
    }
}
