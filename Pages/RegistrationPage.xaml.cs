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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private Frame _frame;
        public RegistrationPage(Frame frame)
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
                    MessageBox.Show("Пользователь с таким логином уже существует!"); return;
                }

                User userObject = new User
                {
                    Login = TextBoxLogin.Text,
                    Password = PasswordBox.Password,
                };
                db.User.Add(userObject);
                db.SaveChanges();
                MessageBox.Show("Пользователь успешно зарегистрирован!");
            }
            _frame.Navigate(new AuthPage(_frame));
        }
    }
}
