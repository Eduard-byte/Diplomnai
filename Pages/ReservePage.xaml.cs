using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            DateTime start = default;
            DateTime end = default;

            if (Auth.User is null)
            {
                MessageBox.Show("Вы не авторизованы.");
                return;
            }

            if (String.IsNullOrEmpty(StartDate.Text))
                errors.AppendLine("Введите дату начала проживания.");
            
            else
                start = Convert.ToDateTime(StartDate.Text).Date;
            

            if (String.IsNullOrEmpty(EndDate.Text))
                errors.AppendLine("Введите дату окончания проживания.");
            else
                end = Convert.ToDateTime(EndDate.Text).Date;

            if (start >= end)
            {
                errors.AppendLine("Дата начала проживания превышает дату конца проживания.");
            }

            if (start.Date < DateTime.Now.Date 
                || end.Date < DateTime.Now.Date)
                errors.AppendLine("Введенные даты некорректны.");

            if (!room.GetActualStatus)
                errors.AppendLine("Этот номер уже забронирован.");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                var reserve = new RegisterRoom
                {
                    Id_room = room.Id,
                    Id_user = Auth.User.Id,
                    StartDate = DateTime.Parse(StartDate.Text),
                    EndDate = DateTime.Parse(EndDate.Text),
                    Status = false
                };

                HotelContext.GetContext().RegisterRooms.Add(reserve);
                room.Status = true;

                HotelContext.GetContext().Entry(room).State = EntityState.Modified;
                HotelContext.GetContext().SaveChanges();

                RoomUser.Room = room;
                RoomUser.Reserve = reserve;

                MessageBox.Show("Номер забронирован.");
                Manager.MainFrame.Navigate(new HomePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // Helpers 
        private string GetError()
        {
            

            return "";
        }
    }
}
