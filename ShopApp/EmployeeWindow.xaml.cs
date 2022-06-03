using Microsoft.Win32;
using ShopApp.DB;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
        }
        ShopDbContext db = new ShopDbContext();
        List<Position> positions = new List<Position>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbShop.ItemsSource = db.Shops.ToList();
            cmbShop.DisplayMemberPath = "ShopName";
            cmbShop.SelectedValuePath = "Id";
            cmbShop.SelectedIndex = -1;
            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserNo.Text.Trim() == "" || txtPassword.Text.Trim() == "" || txtName.Text.Trim() == ""
               || txtSurname.Text.Trim() == "" || txtSalary.Text.Trim() == "" || cmbShop.SelectedIndex == -1
               || cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill the necessary areas");
            }
            else
            {
                Employee employee = new Employee();
                employee.UserNo = Convert.ToInt32(txtUserNo.Text);
                employee.Password = txtPassword.Text;
                employee.Name = txtName.Text;
                employee.Surename = txtSurname.Text;
                employee.Salary = Convert.ToInt32(txtSalary.Text);
                employee.ShopId = Convert.ToInt32(cmbShop.SelectedValue);
                employee.PositionId = Convert.ToInt32(cmbPosition.SelectedValue);
                TextRange text = new TextRange(txtAdress.Document.ContentStart, txtAdress.Document.ContentEnd);
                employee.Address = text.Text;
                employee.BirthDay = picker1.SelectedDate;
                employee.IsAdmin = (bool)chisAdmin.IsChecked;
                string filename = "";
                string Unique = Guid.NewGuid().ToString();
                filename += Unique + dialog.SafeFileName;
                employee.ImagePath = filename;
                db.Employees.Add(employee);
                db.SaveChanges();
                File.Copy(txtImage.Text, @"Images//" + filename);
                MessageBox.Show("Employee was Added");
                txtUserNo.Clear();
                txtPassword.Clear();
                txtName.Clear();
                txtSurname.Clear();
                txtSalary.Clear();
                picker1.SelectedDate = DateTime.Today;
                cmbShop.SelectedIndex = -1;
                cmbPosition.ItemsSource = positions;
                cmbPosition.SelectedIndex = -1; txtAdress.Document.Blocks.Clear();
                chisAdmin.IsChecked = false;
                EmployeeImage.Source = new BitmapImage();
                txtImage.Clear();
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        OpenFileDialog dialog = new OpenFileDialog();
        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            if (dialog.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(dialog.FileName);
                image.EndInit();
                EmployeeImage.Source = image;
                txtImage.Text = dialog.FileName;
            }
        }

        private void txtUserNo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            bool isUnique = false;
            var Uniquelist = db.Employees.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (Uniquelist.Count > 0)
            {
                MessageBox.Show("This User No is Already used by another employee");
            }
            else
            {
                MessageBox.Show("This user no is avaliable");
            }
        }

        private void txtUserNo_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void txtUserNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void cmbShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ShopId = Convert.ToInt32(cmbShop.SelectedValue);
            if (cmbShop.SelectedIndex != -1)
            {
                cmbPosition.ItemsSource = positions.Where(x => x.ShopId == ShopId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }
    }
}
