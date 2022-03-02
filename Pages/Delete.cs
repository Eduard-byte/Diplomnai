//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using GreatBrainApp.Models;
//using Microsoft.Win32;
//using UIKitTutorials.Helpers;

//namespace GreatBrainApp.Pages
//{
//    /// <summary>
//    /// Логика взаимодействия для AddGoodPage.xaml
//    /// </summary>
//    public partial class AddGoodPage : Page
//    {
//        private Товары _currentGood = new Товары();
//        // путь к файлу
//        private string _filePath = null;
//        // название текущей главной фотографии
//        private string _photoName = null;
//        // текущая папка приложения
//        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";
//        public AddGoodPage(Товары selectedGood)
//        {
//            InitializeComponent();
//            if (selectedGood != null)
//            {
//                _currentGood = selectedGood;
//                TextBoxGoodId.Visibility = Visibility.Hidden;
//                TextBlockGoodId.Visibility = Visibility.Hidden;
//                int x = selectedGood.ID;
//                // загрузка комплементарных товаров
//                List<Комплектация> additional = ЭнтитиЖмых.GetContext().Комплектация.Where(p =>
//               p.Основной_товар == selectedGood.ID).ToList();
//                List<Товары> goods = new List<Товары>();
//                foreach (Комплектация item in additional)
//                {
//                    goods.Add(item.Товары1);
//                }
//                ListViewAdditional.ItemsSource = goods;
//                _filePath = _currentDirectory + _currentGood.Основное_фото;
//            }
//            DataContext = _currentGood;
//            _photoName = _currentGood.Основное_фото;
//            //загрузка производителей
//            ComboDeveloper.ItemsSource = ЭнтитиЖмых.GetContext().Производители.ToList();
//        }
//        private StringBuilder CheckFields()
//        {
//            StringBuilder s = new StringBuilder();
//            if (string.IsNullOrWhiteSpace(_currentGood.Наименивание_товара))
//                s.AppendLine("Поле название пустое");
//            if (_currentGood.Производители == null)
//                s.AppendLine("Выберите производителя");
//            if (_currentGood.Цена < 0)
//                s.AppendLine("Цена не может быть отрицательной");
//            if (!string.IsNullOrWhiteSpace(TextBoxHeight.Text))
//            {
//                double x = 0;
//                if (!double.TryParse(TextBoxHeight.Text, out x))
//                    s.AppendLine("Высота только число");
//                else if (x < 0)
//                {
//                    s.AppendLine("Высота не может быть отрицательной");
//                }
//            }
//            if (!string.IsNullOrWhiteSpace(TextBoxLength.Text))
//            {
//                double x = 0;
//                if (!double.TryParse(TextBoxLength.Text, out x))
//                    s.AppendLine("Длина только число");
//                else if (x < 0)
//                {
//                    s.AppendLine("Длина не может быть отрицательной");
//                }
//            }
//            if (!string.IsNullOrWhiteSpace(TextBoxWidth.Text))

//            {
//                double x = 0;
//                if (!double.TryParse(TextBoxWidth.Text, out x))
//                    s.AppendLine("Ширина только число");
//                else if (x < 0)

//                {
//                    s.AppendLine("Ширина не может быть отрицательной");

//                }

//            }
//            if (!string.IsNullOrWhiteSpace(TextBoxWeight.Text))

//            {
//                double x = 0;
//                if (!double.TryParse(TextBoxWeight.Text, out x))
//                    s.AppendLine("Вес только число");
//                else if (x < 0)

//                {
//                    s.AppendLine("Вес не может быть отрицательным");

//                }

//            }
//            if
//           (string.IsNullOrWhiteSpace(_photoName))
//                s.AppendLine("фото не выбрано пустое");
//            return s;

//        }
//        // сохранение
//        private void BtnSaveClick(object sender, RoutedEventArgs e)

//        {
//            StringBuilder _error = CheckFields();
//            // если ошибки есть, то выводим ошибки в MessageBox
//            // и прерываем выполнение 
//            if (_error.Length > 0)

//            {
//                MessageBox.Show(_error.ToString());
//                return
//               ;

//            }
//            // проверка полей прошла успешно

//            if (_currentGood.ID == 0)
//            {
//                // добавление нового товара
//                // формируем новое название файла картинки,
//                // так как в папке может быть файл с тем же именем
//                string photo = ChangePhotoName();
//                // путь куда нужно скопировать файл
//                string dest = _currentDirectory + photo;
//                File.Copy(_filePath, dest);
//                _currentGood.Основное_фото = photo;
//                // добавляем товарв БД
//                ЭнтитиЖмых.GetContext().Товары.Add(_currentGood);

//            }
//            try
//            {
//                if (_filePath != null)
//                {

//                    string photo = ChangePhotoName();
//                    string dest = _currentDirectory + photo;
//                    File.Copy(_filePath, dest);
//                    _currentGood.Основное_фото = photo;
//                }
//                // Сохраняем изменения в БД
//                ЭнтитиЖмых.GetContext().SaveChanges();
//                MessageBox.Show("Запись Изменена");
//                // Возвращаемся на предыдущую форму
//                Manager.MainFrame.GoBack();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message.ToString());
//            }
//        }
//        // открытие этой же страницы
//        // в качестве параметра передаем выделенный товар в комплементарных товарах
//        private void ListViewAdditionalPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
//        {
//            if (ListViewAdditional.SelectedItems.Count > 0)
//            {
//                Товары good = (sender as ListView).SelectedItem as Товары;
//                Manager.MainFrame.Navigate(new AddGoodPage(good));
//            }
//        }
//        // открытие окна редактирования комплементарных товаров
//        private void BtnEditAdditionalClick(object sender, RoutedEventArgs e)
//        {
//            if (_currentGood.ID != 0)
//            {
//                Manager.MainFrame.Navigate(new AdditionalGoodsPage(_currentGood));
//            }
//        }
//        // Событие визуализации страницы
//        // после визуализации окна данные в listView подгружаются снова
//        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
//        {
//            // загрузка комплементарных товаров
//            List<Комплектация> additional = ЭнтитиЖмых.GetContext().Комплектация.Where(p => p.Основной_товар
//           == _currentGood.ID).ToList();
//            List<Товары> goods = new List<Товары>();
//            foreach (Комплектация item in additional)
//            {
//                goods.Add(item.Товары);
//            }
//            ListViewAdditional.ItemsSource = goods;
//        }
//        // загрузка фото
//        private void BtnLoadClick(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                //Диалог открытия файла
//                OpenFileDialog op = new OpenFileDialog();
//                op.Title = "Select a picture";
//                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif) | *.gif";
//                // диалог вернет true, если файл был открыт
//                if (op.ShowDialog() == true)
//                {
//                    // проверка размера файла
//                    // по условию файл дожен быть не более 2Мб.
//                    FileInfo fileInfo = new FileInfo(op.FileName);
//                    if (fileInfo.Length > (1024 * 1024 * 2))
//                    {
//                        // размер файла меньше 2Мб. Поэтому выбрасывается новое исключение
//                        throw new Exception("Размер файла должен быть меньше 2Мб");
//                    }
//                    ImagePhoto.Source = new BitmapImage(new Uri(op.FileName));
//                    _photoName = op.SafeFileName;
//                    _filePath = op.FileName;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//                _filePath = null;
//            }
//        }
//        //подбор имени файла
//        string ChangePhotoName()
//        {
//            string x = _currentDirectory + _photoName;
//            string photoname = _photoName;
//            int i = 0;
//            if (File.Exists(x))
//            {
//                while (File.Exists(x))
//                {
//                    i++;
//                    x = _currentDirectory + i.ToString() + photoname;
//                }
//                photoname = i.ToString() + photoname;
//            }
//            return photoname;
//        }
//    }
//}