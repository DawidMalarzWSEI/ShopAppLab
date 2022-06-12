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
using Task = ShopApp.DB.Task;

namespace ShopApp.Views
{
    /// <summary>
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {
        public TaskList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TaskPage page = new TaskPage();
            page.ShowDialog();
            FillDataGrid();
        }

        ShopDbContext db = new ShopDbContext();
        List<TaskDetailModel> taskList = new List<TaskDetailModel>();
        List<TaskDetailModel> searchList = new List<TaskDetailModel>();
        List<Position> positionsList = new List<Position>();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        void FillDataGrid()
        {
            taskList = db.Tasks.Include(x => x.TaskStateNavigation).Include(x => x.Employee)
               .ThenInclude(x => x.Shop).ThenInclude(x => x.Positions).Select(x => new TaskDetailModel()
               {
                   Id = x.Id,
                   EmployeeId = (int)x.EmployeeId,
                   Name = x.Employee.Name,
                   StateName = x.TaskStateNavigation.StateName,
                   Surname = x.Employee.Surename,
                   TaskContent = x.TaskDescription,
                   TaskDeliveryDate = x.TaskDeliveryDate,
                   TaskStartDate = (DateTime)x.TaskStartDate,
                   TaskState = (int)x.TaskState,
                   TaskTitle = x.TaskTitle,
                   UserNo = x.Employee.UserNo,
                   ShopId = x.Employee.ShopId,
                   PositionId = x.Employee.PositionId
               }).ToList();

            gridTask.ItemsSource = taskList;
            searchList = taskList;
            cmbShop.ItemsSource = db.Shops.ToList();
            cmbShop.DisplayMemberPath = "ShopName";
            cmbShop.SelectedValuePath = "Id";
            cmbShop.SelectedIndex = -1;
            positionsList = db.Positions.ToList();
            cmbPosition.ItemsSource = positionsList;
            cmbPosition.DisplayMemberPath = "PositionName";
            cmbPosition.SelectedValuePath = "Id";
            cmbPosition.SelectedIndex = -1;
            List<TaskState> taskstates = db.TaskStates.ToList();
            cmbState.ItemsSource = taskstates;
            cmbState.DisplayMemberPath = "NameState";
            cmbState.SelectedValuePath = "Id";
            cmbState.SelectedIndex = -1;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<TaskDetailModel> search = searchList;
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
            if (cmbState.SelectedIndex != -1)
                search = search.Where(x => x.TaskState == Convert.ToInt32(cmbState.SelectedValue)).ToList();
            if (rbStart.IsChecked == true)
                search = search.Where(x => x.TaskStartDate > dpStart.SelectedDate && x.TaskStartDate < dpDelivery.SelectedDate).ToList();
            if (rbDelivery.IsChecked == true)
                search = search.Where(x => x.TaskDeliveryDate > dpStart.SelectedDate && x.TaskDeliveryDate < dpDelivery.SelectedDate).ToList();
            gridTask.ItemsSource = search;
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

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            dpDelivery.SelectedDate = DateTime.Today;
            dpStart.SelectedDate = DateTime.Today;
            cmbShop.SelectedIndex = -1;
            cmbState.SelectedIndex = -1;
            cmbPosition.ItemsSource = positionsList;
            cmbPosition.SelectedIndex = -1;
            rbDelivery.IsChecked = false;
            rbStart.IsChecked = false;
            gridTask.ItemsSource = taskList;
        }

        TaskDetailModel model = new TaskDetailModel();
        private void gridTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model = (TaskDetailModel)gridTask.SelectedItem;

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            TaskPage page = new TaskPage();
            page.model = model;
            page.ShowDialog();
            FillDataGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete", "Question", MessageBoxButton.YesNo
               , MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (model.Id != 0)
                {
                    TaskDetailModel taskmodel = (TaskDetailModel)gridTask.SelectedItem;
                    Task task = db.Tasks.First(x => x.Id == taskmodel.Id);
                    db.Tasks.Remove(task);
                    db.SaveChanges();
                    MessageBox.Show("Task was Deleted");
                    FillDataGrid();
                }
            }
        }
    }
}
