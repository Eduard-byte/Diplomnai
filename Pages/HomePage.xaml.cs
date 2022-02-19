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
    public partial class HomePage : Page
    {
        private User person = new User();
        private Room room = new Room();
        private RegisterRoom request = new RegisterRoom();

        public HomePage()
        {
            InitializeComponent();

            person = Auth.User;
            if (person is null)
            {
                ImageUser.ImageSource = new BitmapImage(new Uri("C:\\Users\\eduar\\Desktop\\Hotel PC\\UIKitTutorials-main\\bin\\Debug\\Images\\User\\UserNull.png"));
                Name.Content = "—";
                Surname.Content = "—";
                Patronymic.Content = "—";
                Email.Content = "—";
            }
            else
            {
                ImageUser.ImageSource = new BitmapImage(new Uri(person.GetPhoto));
                Name.Content = person.Name;
                Surname.Content = person.Surname;
                Patronymic.Content = person.Patronymic;
                Email.Content = person.Email;
            }

            room = RoomUser.Room;
            request = RoomUser.Reserve;
            if (room is null || request is null)
            {
                RoomImage.ImageSource =
                    new BitmapImage(new Uri(
                        "C:\\Users\\eduar\\Desktop\\Hotel PC\\UIKitTutorials-main\\bin\\Debug\\Images\\Room\\NullRoom.png"));
                RoomName.Content = "Номер не выбран";
                DateStart.Content = "—";
                DateEnd.Content = "—";
                Price.Content = "—";
                DaysToAccommodation.Content = "—";
            }
            else
            {
                RoomImage.ImageSource =
                    new BitmapImage(new Uri(room.GetPhoto));
                RoomName.Content = room.Name + " номер";
                DateStart.Content = request.StartDate.ToString("dd MMMM yyyy года");
                DateEnd.Content = request.EndDate.ToString("dd MMMM yyyy года");
                Price.Content = RoomUser.GetPrice + " ₽";
                DaysToAccommodation.Content = RoomUser.GetDaysToAccommodation;
            }
        }

        private void SettingUser(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
