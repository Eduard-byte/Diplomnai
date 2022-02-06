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

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для RoomListPage.xaml
    /// </summary>
    public partial class RoomListPage : Page
    {
        public RoomListPage()
        {
            InitializeComponent();

            RoomList.ItemsSource = HotelContext.GetContext().Rooms.ToList();
        }

        private void ReserveButton(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ReservePage((sender as Button).DataContext as Room));
        }
    }
}
