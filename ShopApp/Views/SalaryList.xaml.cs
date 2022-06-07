using Microsoft.EntityFrameworkCore;
using ShopApp.DB;
using ShopApp.ViewModels;
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

namespace ShopApp.Views
{
    /// <summary>
    /// Interaction logic for SalaryList.xaml
    /// </summary>
    public partial class SalaryList : UserControl
    {
        public SalaryList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SalaryWindow window = new SalaryWindow();
            window.ShowDialog();
            FillDataGrid();
        }

        ShopDbContext db = new ShopDbContext();
        List<SalaryDetailModel> salaries = new List<SalaryDetailModel>();
        List<Position> positions = new List<Position>();
        void FillDataGrid()
        {
            salaries = db.Salaries.Include(x => x.Employee).Include(x => x.Month).Select(x => new SalaryDetailModel()
            {
                UserNo = x.Employee.UserNo,
                Name = x.Employee.Name,
                Amount = x.Amount,
                EmployeeId = x.EmployeeId,
                Id = x.Id,
                MonthId = x.Month,
                MonthName = x.Month.MonthName,
                Surname = x.Employee.Surename,
                Year = x.Year,
                ShopId = x.Employee.ShopId,
                PositionId = x.Employee.PositionId
            }).OrderByDescending(x => x.Year).OrderByDescending(x => x.MonthId).ToList();
            gridSalary.ItemsSource = salaries;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
            cmbShop.ItemsSource = db.Shops.ToList();
            cmbShop.DisplayMemberPath = "ShopName";
            cmbShop.SelectedValuePath = "Id";
            cmbShop.SelectedIndex = -1;
            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;
            List<Salary> months = db.Salaries.ToList();
            cmbMonth.ItemsSource = months;
            cmbMonth.DisplayMemberPath = "MonthName";
            cmbMonth.SelectedValuePath = "Id";
            cmbMonth.SelectedIndex = -1;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<SalaryDetailModel> search = salaries;
            if (txtUserNo.Text.Trim() != "")
                search = search.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtName.Text.Trim() != "")
                search = search.Where(x => x.Name.Contains(txtName.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                search = search.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            if (cmbShop.SelectedIndex != -1)
                search = search.Where(x => x.ShopId == Convert.ToInt32(cmbShop.SelectedValue)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                search = search.Where(x => x.PositionId == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            if (txtYear.Text.Trim() != "")
                search = search.Where(x => x.Year == Convert.ToInt32(txtYear.Text)).ToList();
            if (cmbMonth.SelectedIndex != -1)
                search = search.Where(x => x.MonthId == Convert.ToInt32(cmbMonth.SelectedValue)).ToList();
            if (txtSalary.Text.Trim() != "")
            {
                if (rbMore.IsChecked == true)
                    search = search.Where(x => x.Amount > Convert.ToInt32(txtSalary.Text)).ToList();
                else if (rbLess.IsChecked == true)
                    search = search.Where(x => x.Amount < Convert.ToInt32(txtSalary.Text)).ToList();
                else
                    search = search.Where(x => x.Amount == Convert.ToInt32(txtSalary.Text)).ToList();

            }
            gridSalary.ItemsSource = search;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtYear.Clear();
            txtSalary.Clear();
            cmbShop.SelectedIndex = -1;
            cmbPosition.ItemsSource = positions;
            cmbPosition.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            rbEquals.IsChecked = false;
            rbMore.IsChecked = false;
            rbLess.IsChecked = false;
            gridSalary.ItemsSource = salaries;
        }
        SalaryDetailModel model = new SalaryDetailModel();
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SalaryWindow window = new SalaryWindow();
            window.model = model;
            window.ShowDialog();
            FillDataGrid();
        }

        private void gridSalary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model = (SalaryDetailModel)gridSalary.SelectedItem;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete", "Question", MessageBoxButton.YesNo
               , MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (model.Id != 0)
                {
                    SalaryDetailModel salarymodel = (SalaryDetailModel)gridSalary.SelectedItem;
                    Salary salary = db.Salaries.Find(salarymodel.Id);
                    db.Salaries.Remove(salary);
                    db.SaveChanges();
                    MessageBox.Show("Salary was deleted");
                    FillDataGrid();
                }
            }
        }
    }
}
