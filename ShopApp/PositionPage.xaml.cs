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
            if(model!=null && model.Id!=0)
            {
                cmbShop.SelectedValue = model.ShopId;
                txtPositionname.Text = model.PositionName;
            }
        }

        public PositionModel model;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbShop.SelectedIndex == -1 || txtPositionname.Text.Trim() == "")
                MessageBox.Show("Please fill all areas");
            else
            {
                if(model != null && model.Id != 0)
                {
                    Position pst = new Position();
                    pst.ShopId = (int)cmbShop.SelectedValue;
                    pst.Id = model.Id;
                    pst.PositionName = txtPositionname.Text;
                    db.Positions.Update(pst);
                    db.SaveChanges();
                    MessageBox.Show("Positions was Update");
                }
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
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
