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

namespace _222_Notice_Sabancheev.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private Frame _frame;
        private Notice _currentNotice = new Notice();
        public AddPage(Frame frame)
        {
            InitializeComponent();

            using (var Entities = new noticeEntities())
            {
                CategoryCombo.ItemsSource = Entities.Category.ToList();
                CategoryCombo.DisplayMemberPath = "CategoryName";
                CityCombo.ItemsSource = Entities.City.ToList();
                CityCombo.DisplayMemberPath = "CityName";
                TypeCombo.ItemsSource = Entities.Type.ToList();
                TypeCombo.DisplayMemberPath = "TypeName";
                DataContext = _currentNotice;
            }

            _frame = frame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(_currentNotice.CityID.ToString());
            _currentNotice.UserID = UserService.UserID;
            _currentNotice.Date = DateTime.Now;
            _currentNotice.Status = true;
            using (var Entities = new noticeEntities())
            {
                if (_currentNotice.ID == 0)
                    Entities.Notice.Add(_currentNotice);
                try
                {
                    Entities.SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!");
                    _frame.Navigate(new MainPage(_frame));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }

        }

    }
}
