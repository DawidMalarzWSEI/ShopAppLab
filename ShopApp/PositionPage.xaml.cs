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

namespace ShopApp
{
    /// <summary>
    /// Interaction logic for PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Window
    {
        public PositionPage()
        {
            InitializeComponent();
        }

        ShopDbContext db = new ShopDbContext();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = db.Shops.ToList().OrderBy(x => x.ShopName);
            cmbShop.ItemsSource = list;
            cmbShop.DisplayMemberPath = "ShopName";
            cmbShop.SelectedValuePath = "Id";
            cmbShop.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbShop.SelectedIndex == -1 || txtPositionname.Text.Trim() == "")
                MessageBox.Show("Please fill all areas");
            else
            {
                Position position = new Position();
                position.PositionName = txtPositionname.Text;
                position.ShopId = Convert.ToInt32(cmbShop.SelectedValue);
                db.Positions.Add(position);
                db.SaveChanges();
                cmbShop.SelectedIndex = -1;
                txtPositionname.Clear();
                MessageBox.Show("Position was Added");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
