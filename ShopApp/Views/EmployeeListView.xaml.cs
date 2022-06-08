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
    /// Logika interakcji dla klasy EmployeeListView.xaml
    /// </summary>
    public partial class EmployeeListView : UserControl
    {
        public EmployeeListView()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow window = new EmployeeWindow();
            window.ShowDialog();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        void FillDatagrid()
        {
            cmbShop.ItemsSource = db.Shops.ToList();
            cmbShop.DisplayMemberPath = "DepartmentName";
            cmbShop.SelectedValuePath = "Id";
            cmbShop.SelectedIndex = -1;
            positions = db.Positions.ToList();
            cmbPosition.ItemsSource = positions;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;
            list = db.Employees.Include(x => x.Position).Include(x => x.Shop).Select(x => new EmployeeDetailModel()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                BirthDay = (DateTime)x.BirthDay,
                ShopId = x.ShopId,
                ShopName = x.Shop.ShopName,
                isAdmin = x.IsAdmin,
                Password = x.Password,
                PositionId = x.PositionId,
                PositionName = x.Position.PositionName,
                Salary = x.Salary,
                Surename = x.Surename,
                UserNo = x.UserNo,
                ImagePath = x.ImagePath
            }).ToList();
            gridEmployee.ItemsSource = list;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDetailModel model = (EmployeeDetailModel)gridEmployee.SelectedItem;
            EmployeeWindow page = new EmployeeWindow();
            page.model = model;
            page.ShowDialog();
            FillDatagrid();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtUserNo.Clear();
            txtSurname.Clear();
            cmbShop.SelectedIndex = -1;
            cmbPosition.ItemsSource = positions;
            cmbPosition.SelectedIndex = -1;
            gridEmployee.ItemsSource = list;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<EmployeeDetailModel> searchlist = list;
            if (txtUserNo.Text.Trim() != "")
                searchlist = searchlist.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtName.Text.Trim() != "")
                searchlist = searchlist.Where(x => x.Name.Contains(txtName.Text)).ToList();
            if (txtSurname.Text.Trim() != "")
                searchlist = searchlist.Where(x => x.Surename.Contains(txtSurname.Text)).ToList();
            if (cmbPosition.SelectedIndex != -1)
                searchlist = searchlist.Where(x => x.PositionId == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            if (cmbShop.SelectedIndex != -1)
                searchlist = searchlist.Where(x => x.ShopId == Convert.ToInt32(cmbShop.SelectedValue)).ToList();
            gridEmployee.ItemsSource = searchlist;
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

        ShopDbContext db = new ShopDbContext();
        List<Position> positions = new List<Position>();
        List<EmployeeDetailModel> list = new List<EmployeeDetailModel>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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
            list=db.Employees.Include(x=>x.Position).Include(x => x.Shop).Select(x => new EmployeeDetailModel() { 
                Id=x.Id,
                Name=x.Name,
                Address=x.Address,
                BirthDay = (DateTime)x.BirthDay,
                ShopId = x.ShopId,
                ShopName= x.Shop.ShopName,
                isAdmin=x.IsAdmin,
                Password = x.Password,
                PositionId=x.PositionId,
                PositionName=x.Position.PositionName,
                Salary=x.Salary,
                Surename=x.Surename,
                UserNo=x.UserNo,
                ImagePath=x.ImagePath


            }).ToList();
            gridEmployee.ItemsSource = list;
        }
    }
}
