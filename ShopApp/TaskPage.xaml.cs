using ShopApp.DB;
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
using System.Windows.Shapes;
using Task = ShopApp.DB.Task;

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Window
    {
        public TaskPage()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        ShopDbContext db = new ShopDbContext();
        List<Employee> employeeList = new List<Employee>();
        List<Position> positionsList = new List<Position>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            employeeList = db.Employees.OrderBy(x => x.Name).ToList();
            gridEmployee.ItemsSource = employeeList;
            cmbShop.ItemsSource = db.Shops.ToList();
            cmbShop.DisplayMemberPath = "DepartmentName";
            cmbShop.SelectedValuePath = "Id";
            cmbShop.SelectedIndex = -1;
            positionsList = db.Positions.ToList();
            cmbPosition.ItemsSource = positionsList;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;
        }
        int EmployeeId = 0;

        private void gridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee employee = (Employee)gridEmployee.SelectedItem;
            txtUserNo.Text = employee.UserNo.ToString();
            txtName.Text = employee.Name;
            txtSurname.Text = employee.Surename;
            EmployeeId = employee.Id;
        }

        private void cmbShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ShopId = Convert.ToInt32(cmbShop.SelectedValue);
            if (cmbShop.SelectedIndex != -1)
            {
                cmbPosition.ItemsSource = positionsList.Where(x => x.ShopId == ShopId).ToList();
                cmbPosition.DisplayMemberPath = "PositionName";
                cmbPosition.SelectedValuePath = "Id";
                cmbPosition.SelectedIndex = -1;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(EmployeeId ==0)
            {
                MessageBox.Show("Please select an eployee from table");
            }
            else if(txtTitle.Text.Trim() == "" || txtContent.Text.Trim() == "")
            {
                MessageBox.Show("Please fill the necessary areas");
            }
            else
            {
                Task task = new Task();
                task.EmployeeId = EmployeeId;
                task.TaskStartDate = DateTime.Now;
                task.TaskTitle = txtTitle.Text;
                task.TaskDescription = txtContent.Text;
                task.TaskState = Definitions.TaskStates.OnEmployee;
                db.Tasks.Add(task);
                db.SaveChanges();
                MessageBox.Show("task was added");
                EmployeeId = 0;
                txtContent.Clear();
                txtTitle.Clear();
                txtUserNo.Clear();
                txtSurname.Clear();
            }
        }
    }
}
