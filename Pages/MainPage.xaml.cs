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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Frame _frame;
        public MainPage(Frame frame)
        {
            InitializeComponent();

            UpdateSource();

            _frame = frame;
        }
       // private void Page_IsVisibleChanged(object sender,
       //DependencyPropertyChangedEventArgs e)
       // {
       //     if (Visibility == Visibility.Visible)
       //     {
       //         using (var Entities = new noticeEntities())
       //         {
       //             Entities.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
       //             DataGridPayment.ItemsSource = Entities.Notice.ToList();
       //         }
       //     }
       // }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserService.UserID>0)
            {
                AddBtn.Visibility = Visibility.Visible;
                DelBtn.Visibility = Visibility.Visible;
                RedBtn.Visibility = Visibility.Visible;
                ReBtn.Visibility = Visibility.Visible;
                EndBtn.Visibility = Visibility.Visible;
                EndуdBtn.Visibility = Visibility.Visible;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new AuthPage(_frame));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            _frame.Navigate(new AddPage(_frame));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var noticeForRemoving = DataGridNotice.SelectedItems.Cast<Notice>().Select(note => note.ID).ToList();
            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {noticeForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var Entities = new noticeEntities())
                    {
                        var noticetoRemoveFromBase = Entities.Notice.Where(ent => noticeForRemoving.Contains(ent.ID)).ToList();
                        Entities.Notice.RemoveRange(noticetoRemoveFromBase);
                        Entities.SaveChanges();
                        MessageBox.Show("Данные успешно удалены!");
                        UpdateSource();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void UpdateSource(bool showEnded = false)
        {
            using (var Entities = new noticeEntities())
            {
                var noticeEntity = Entities.Notice
                   .Include("City")
                   .Include("User")
                   .Include("Type")
                   .Include("Category");

                var notices = new List<Notice>();
                if (UserService.UserID > 0)
                {
                    if (showEnded)
                    {
                        notices = noticeEntity
                       .Where(notice => notice.UserID == UserService.UserID && notice.Status == false)
                       .ToList();
                    }
                    else
                    {
                        notices = noticeEntity
                       .Where(notice => notice.UserID == UserService.UserID)
                       .ToList();
                    }
                }
                else
                {
                    notices = noticeEntity
                   .ToList();
                }

                DataGridNotice.ItemsSource = notices;
                //this.IsVisibleChanged += Page_IsVisibleChanged;

                if (showEnded)
                {
                    LabelSum.Content = "Сумма завершенных объявлений = " + notices.Sum(n => n.Coast);
                }
                else
                {
                    LabelSum.Content = string.Empty;
                }
            }
        }

        private void EndBtn_Click(object sender, RoutedEventArgs e)
        {
            var noticeForRemoving = DataGridNotice.SelectedItems.Cast<Notice>().Select(note => note.ID).ToList();
            if (MessageBox.Show($"Вы точно хотите завершить записи в количестве {noticeForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var Entities = new noticeEntities())
                    {
                        var noticetoRemoveFromBase = Entities.Notice.Where(ent => noticeForRemoving.Contains(ent.ID)).ToList();
                        foreach (var item in noticetoRemoveFromBase)
                        {
                            item.Status = false;
                        }
                        Entities.SaveChanges();
                        MessageBox.Show("Данные успешно завершены!");
                        UpdateSource();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EndуdBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateSource(true);
        }

        private void ReBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateSource();
        }
    }

}
