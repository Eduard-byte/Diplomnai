using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private Accommodation _accommodation = new Accommodation();

        public SendMessage(RegisterRoom register)
        {
            InitializeComponent();

            if (register is null)
            {
                MessageBox.Show("Что-то пошло не так =(");
                Manager.MainFrame.Navigate(new ReserveUsersPage());
                return;
            }
            
            _register = register;

            //+" ₽"
            Price.Text = Convert.ToString(_register.GetPrice);

            DataContext = _register;
        }

        private void SendEmail(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(Price.Text))
            {
                errors.AppendLine("Введите итоговую цену проживания");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (!_register.Status)
                {
                    _accommodation.Days = _register.GetDays;
                    _accommodation.Price = Convert.ToDecimal(Price.Text);
                    _accommodation.Description = Desc.Text;
                    _accommodation.Id_resgister = _register.Id;

                    var message = "<h2>Здравствуйте!</h2><br>" +
                                  $"{_register.User.Name}, вы забронировали <b>{_register.Room.Name}</b><br><br>" +
                                  $"Дата начала проживания: <b>{_register.StartDate:D}</b><br>" +
                                  $"Дата окончания проживания <b>{_register.EndDate:D}</b><br><br>" +
                                  $"Сумма проживания: <b>{Price.Text} ₽</b><br><br>" +
                                  $"Заметки от отеля Ibis: {Desc.Text}";
                    Execute(_register.User.Email, "Отель Ibis", message);

                    _register.Status = true;

                    HotelContext.GetContext().Entry(_register).State = EntityState.Modified;
                    HotelContext.GetContext().Accommodations.Add(_accommodation);
                    HotelContext.GetContext().SaveChanges();

                    Manager.MainFrame.Navigate(new ReserveUsersPage());
                }
                else
                {
                    MessageBox.Show("Письмо уже отправлено");
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Что-то ошло не так. Подробнее - {ex.Message}");
                return;
            }
        }

        private void Execute(string email, string subject, string message)
        {
            // Клиент который отправляет письмо 
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("sjbhdvghchjskjcj@gmail.com", "Eduard0507");
            client.EnableSsl = true;

            // Получатель письма
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sjbhdvghchjskjcj@gmail.com", "Отель Ibis");
            mail.To.Add(new MailAddress(email));
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = message;

            // Отправка
            client.Send(mail);
        }
    }
}
