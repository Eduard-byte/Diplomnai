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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public static User User { get; set; } = new User();

        public RegisterPage()
        {
            InitializeComponent();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (String.IsNullOrEmpty(Name.Text))
                errors.AppendLine("Укажите Имя.");
            else
                User.Name = Name.Text;

            if (String.IsNullOrEmpty(Surname.Text))
                errors.AppendLine("Укажите Фамилию.");
            else
                User.Surname = Surname.Text;

            if (String.IsNullOrEmpty(Patronymic.Text))
                errors.AppendLine("Укажите Отчество.");
            else
                User.Patronymic = Patronymic.Text;

            if (String.IsNullOrEmpty(Email.Text))
                errors.AppendLine("Укажите Email.");
            else
                User.Email = Email.Text;

            if (String.IsNullOrEmpty(Password.Text))
                errors.AppendLine("Укажите Пароль.");
            else
                User.Password = Password.Text;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            try
            {
                HotelContext.GetContext().Users.Add(User);
                HotelContext.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Manager.ImageUser.ImageSource = new BitmapImage(new Uri(User.GetPhoto));
            Manager.NameUser.Content = User.Surname + " " + User.Name;
            Manager.EmailUser.Content = User.Email;

            Manager.MainFrame.Navigate(new HomePage());
        }
    }
}
