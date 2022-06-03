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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ShopList.xaml
    /// </summary>
    public partial class ShopList : UserControl
    {
        public ShopList()
        {
            InitializeComponent();
            using (ShopDbContext db = new ShopDbContext())
            {
                List<Shop> list = db.Shops.OrderBy(x => x.ShopName).ToList();
                gridShop.ItemsSource = list;
            }
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow window = new ShopWindow(); 
            window.ShowDialog();
            using (ShopDbContext db = new ShopDbContext())
            {
                List<Shop> list = db.Shops.OrderBy(x => x.ShopName).ToList();
                gridShop.ItemsSource = list;
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Shop shop = (Shop)gridShop.SelectedItem;
            ShopWindow window = new ShopWindow();
            window.shop = shop;
            window.ShowDialog();
            using (ShopDbContext db = new ShopDbContext()) {
                gridShop.ItemsSource = db.Shops.OrderBy(x => x.ShopName).ToList();
            }
        }
    }
}
