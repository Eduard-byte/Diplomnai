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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public static User User { get; set; } = new User();

        public static bool IsAdmin { get; set; } = false;

        public Authorization()
        {
            InitializeComponent();
        }

        private void Register(object sender, MouseButtonEventArgs e)
        {
            Manager.MainFrame.Navigate(new RegisterPage());
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (String.IsNullOrEmpty(Email.Text))
                errors.AppendLine("Укажите Email.");

            if (String.IsNullOrEmpty(Password.Password))
                errors.AppendLine("Введите пароль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                User = HotelContext.GetContext().Users
                    .FirstOrDefault(user =>
                        user.Email == Email.Text && user.Password == Password.Password);
                if (User == default)
                {
                    MessageBox.Show("Email или пароль введён неверно.");
                    return;
                }

                if (User.Email == "admin@ibis.com" && User.Password == "0000")
                {
                    IsAdmin = true;
                }

                Auth.User = User;
                MessageBox.Show($"Здравствуйте! {User.Name}.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            

            Auth.ImageUser.ImageSource = new BitmapImage(new Uri(User.GetPhoto));
            Auth.NameUser.Content = User.Surname + " " + User.Name;
            Auth.EmailUser.Content = User.Email;
            RoomCheck();

            Manager.MainFrame.Navigate(new HomePage());
        }

        private void RoomCheck()
        {
            if (Auth.User is null)
                return;

            var resultReserve = HotelContext.GetContext().RegisterRooms.FirstOrDefault(x => x.Id_user == Auth.User.Id);

            if (resultReserve is null)
                return;
            
            RoomUser.Reserve = resultReserve;
            RoomUser.Room = HotelContext.GetContext().Rooms.FirstOrDefault(x => x.Id == resultReserve.Id_room);
        
        }
    }
}
