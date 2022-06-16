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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Shop model = (Shop)gridShop.SelectedItem;
            if (model != null && model.Id != 0)
            {
                if (MessageBox.Show("Are you sure to delete", "Question", MessageBoxButton.YesNo
                    , MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ShopDbContext db = new ShopDbContext();
                    List<Position> positions = db.Positions.Where(x => x.ShopId == model.Id).ToList();
                    foreach (var item in positions)
                    {
                        db.Positions.Remove(item);
                    }
                    db.SaveChanges();

                    Shop shop = db.Shops.Find(model.Id);
                    db.Shops.Remove(shop);
                    db.SaveChanges();
                    MessageBox.Show("Shop was deleted");
                    gridShop.ItemsSource = db.Shops.ToList();
                }

            }
        }
    }
}
