using ShopApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShopApp
{
    /// <summary>
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        ShopDbContext db = new ShopDbContext();

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserNo.Text.Trim() == "" || txtPassword.Password.Trim() == "")
            {
                MessageBox.Show("Please fill UserNo or Password");
            }
            else
            {
                Employee employee = db.Employees.FirstOrDefault(x => x.UserNo == Convert.ToInt32(txtUserNo.Text) &&
                                                                x.Password.Equals(txtPassword.Password));
                if (employee != null && employee.Id != 0)
                {
                    this.Visibility = Visibility.Collapsed;
                    MainWindow main = new MainWindow();
                    UserStatic.EmployeeId = employee.Id;
                    UserStatic.isAdmin = employee.IsAdmin;
                    UserStatic.Name = employee.Name;
                    UserStatic.Surename = employee.Surename;
                    UserStatic.UserNo = employee.UserNo;
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Wrong data :/");
                }

            }
        }

        private void txtUserNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
