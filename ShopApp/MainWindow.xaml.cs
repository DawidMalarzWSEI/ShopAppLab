using ShopApp.DB;
using ShopApp.ViewModels;
using System.Windows;


namespace ShopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            using (ShopDbContext db = new ShopDbContext()) { 
                
            }

        }

        private void btnShop_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ShopViewModel();
        }

        private void btnPosition_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PositionViewModel();
        }

<<<<<<< HEAD
        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TaskViewModel();
=======
        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new EmployeeViewModel();
>>>>>>> edb921739b5a0e88c7bf1a997117979e690de6b1
        }
    }
}
