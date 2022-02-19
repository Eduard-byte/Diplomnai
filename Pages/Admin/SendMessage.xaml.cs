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
    /// Логика взаимодействия для SendMessage.xaml
    /// </summary>
    public partial class SendMessage : Page
    {
        private RegisterRoom _register = new RegisterRoom();

        public SendMessage(RegisterRoom register)
        {
            InitializeComponent();

            if (register is null)
            {
                MessageBox.Show("Что-то пошло не так =(");
                Manager.MainFrame.Navigate(new ReserveUsersPage());
                return;
            }
            else
            {
                _register = register;
            }

            Price.Text = Convert.ToString(_register.GetPrice) + " ₽";

            DataContext = _register;

            
        }
    }
}
