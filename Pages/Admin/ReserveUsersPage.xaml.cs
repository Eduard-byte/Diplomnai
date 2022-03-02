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

namespace UIKitTutorials.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для ReserveUsersPage.xaml
    /// </summary>
    public partial class ReserveUsersPage : Page
    {
        public ReserveUsersPage()
        {
            InitializeComponent();

            UserReserveList.ItemsSource = HotelContext.GetContext().RegisterRooms.ToList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var result = (sender as Button).DataContext as RegisterRoom;

            if (MessageBox.Show($"Вы точно хотите удалить запись?",
                "Внимание", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    HotelContext.GetContext().RegisterRooms.Remove(result);
                    HotelContext.GetContext().SaveChanges();
                    Manager.MainFrame.Navigate(new ReserveUsersPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Что-то пошло не так. Подробнее: {ex}");
                }
            }
        }

        private void SendEmail(object sender, RoutedEventArgs e)
        {
            var result = (sender as Button).DataContext as RegisterRoom;

            Manager.MainFrame.Navigate(new SendMessage(result));
        }


        private void Search(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            var list = HotelContext.GetContext().RegisterRooms.ToList();

            list = list.Where(user => user.User.Surname.ToLower().Contains(SearchText.Text.ToLower())).ToList();

            UserReserveList.ItemsSource = list;
        }

        
    }
}
