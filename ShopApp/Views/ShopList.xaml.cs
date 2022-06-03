using ShopApp.DB;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


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
