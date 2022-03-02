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

            SortByStatus.SelectedIndex = 0;
            SortByPrice.SelectedIndex = 0;
        }

        private void ReserveButton(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ReservePage((sender as Button).DataContext as Room));
        }

        private void SortByPrice_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           Update(); 
        }

        private void SortByStatus_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            var result = HotelContext.GetContext().Rooms.ToList();

            if (SortByPrice.SelectedIndex >= 0)
            {
                if(SortByPrice.SelectedIndex == 1)
                    result = result.OrderBy(p => p.Price).ToList();
                if (SortByPrice.SelectedIndex == 2)
                    result = result.OrderByDescending(p => p.Price).ToList();
            }

            if (SortByStatus.SelectedIndex >= 0)
            {
                if (SortByStatus.SelectedIndex == 1)
                    result = result.Where(s => s.GetActualStatus).ToList();
                if (SortByStatus.SelectedIndex == 2)
                    result = result.Where(s => !s.GetActualStatus).ToList();
            }

            //switch (SortByPrice.SelectedIndex)
            //{
            //    case 0: break;
            //    case 1: 
            //        break;
            //    case 2: result = result.OrderByDescending(p => p.Price).ToList();
            //        break;
            //}

            //switch (SortByStatus.SelectedIndex)
            //{
            //    case 0: break;
            //    case 1:
            //        result = result.Where(s => s.GetActualStatus).ToList();
            //        break;
            //    case 2:
            //        result = result.Where(s => !s.GetActualStatus).ToList();
            //        break;
            //}

            RoomList.ItemsSource = result;
        }
    }
}
