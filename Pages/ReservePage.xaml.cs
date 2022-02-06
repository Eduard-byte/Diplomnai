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
    /// Логика взаимодействия для ReservePage.xaml
    /// </summary>
    public partial class ReservePage : Page
    {
        private Room room { get; set; } = new Room();

        public ReservePage(Room roomSelected)
        {
            InitializeComponent();

            if (roomSelected is null)
            {
                MessageBox.Show("Комната не выбрана.");
                return;
            }

            room = roomSelected;

            DataContext = room;
        }

        private void ReserveUser(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (Authorization.User is null)
            {
                MessageBox.Show("Вы не авторизованы. ");
                return;
            }

            if (String.IsNullOrEmpty(StartDate.Text))
            {
                errors.AppendLine("Введите дату начала проживания.");
            }

            if (String.IsNullOrEmpty(EndDate.Text))
            {
                errors.AppendLine("Введите дату окончания проживания.");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                HotelContext.GetContext().RegisterRooms.Add(new RegisterRoom
                {
                    Id_room = room.Id,
                    Id_user = Authorization.User.Id,
                    StartDate = DateTime.Parse(StartDate.Text),
                    EndDate = DateTime.Parse(EndDate.Text),
                    Status = false
                });
                HotelContext.GetContext().SaveChanges();
                MessageBox.Show("Номер забронирован.");
                Manager.MainFrame.Navigate(new HomePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
