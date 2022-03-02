using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using Microsoft.Win32;
using UIKitTutorials.Entities;
using UIKitTutorials.Helpers;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        private readonly User _currentUser = new User();
        
        private string _filePath = null;
        private string _photoName = null;
        private readonly string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\User\";

        public UserProfilePage(User user)
        {
            if (user == null)
            {
                MessageBox.Show("Что-то пошло не так.");
                return;
            }

            InitializeComponent();

            _currentUser = user;

            DataContext = _currentUser;

            DateOfBirth.Text = Convert.ToString(_currentUser.DateOfBirth);
            _filePath = _currentDirectory + _currentUser.Image;
            _photoName = _currentUser.Image;
        }

        private void AddImage(object sender, MouseButtonEventArgs e)
        {
            AddImageProfile();
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = CheckErrors();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {

                if (_currentUser.Image == null)
                {
                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentUser.Image = photo;
                }

                // Сохраняем изменения в БД
                _currentUser.DateOfBirth = Convert.ToDateTime(DateOfBirth.Text);
                HotelContext.GetContext().Entry(_currentUser).State = EntityState.Modified;
                HotelContext.GetContext().SaveChanges();

                Auth.User = _currentUser;

                MessageBox.Show("Запись Изменена");
                Manager.MainFrame.Navigate(new HomePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Что-то пошло не так. Подробнее — {ex}");
            }
        }

        private void AddImageProfile()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif) | *.gif";
                
                if (op.ShowDialog() == true)
                {
                    FileInfo fileInfo = new FileInfo(op.FileName);
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
                    ImageUser.ImageSource = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _filePath = null;
            }
        }

        private string ChangePhotoName()
        {
            string x = _currentDirectory + _photoName;
            string imageName = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i + imageName;
                }
                imageName = i + imageName;
            }
            return imageName;
        }

        private StringBuilder CheckErrors()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(Name.Text))
                errors.AppendLine("Введите Имя");

            if (string.IsNullOrEmpty(Surname.Text))
                errors.AppendLine("Введите Фамилию");

            if (string.IsNullOrEmpty(Patronymic.Text))
                errors.AppendLine("Введите Отчество");

            if (string.IsNullOrEmpty(Email.Text))
                errors.AppendLine("Введите Email");

            return errors;
        }
    }
}
