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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private Frame _frame;
        public AuthPage(Frame frame)
        {
            InitializeComponent();

            _frame = frame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var db = new noticeEntities();
            {
                var user = db.User
                .AsNoTracking()
                .FirstOrDefault(u => u.Login == TextBoxLogin.Text);
                if (user != null)
                {
                   UserService.UserID = user.ID;
                    _frame.Navigate(new MainPage(_frame));
                    return;
                }
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new RegistrationPage(_frame));
        }
    }
}

