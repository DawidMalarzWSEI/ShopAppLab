using ShopApp.DB;
using System.Windows;


namespace ShopApp
{
    /// <summary>
    /// Logika interakcji dla klasy ShopWindow.xaml
    /// </summary>
    public partial class ShopWindow : Window
    {
        public ShopWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Shop shop;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtShopName.Text.Trim() == "")
            {
                MessageBox.Show("Uzupełnij pole!!!");
            }
            else {
                using (ShopDbContext db = new ShopDbContext())
                {
                    if (shop != null && shop.Id != 0)
                    {
                        Shop update = new Shop();
                        update.Id = shop.Id;
                        update.ShopName = txtShopName.Text;
                        db.Shops.Update(update);
                        db.SaveChanges();
                    }
                    else
                    {
                        Shop shop = new Shop();
                        shop.ShopName = txtShopName.Text;
                        db.Shops.Add(shop);
                        db.SaveChanges();
                        txtShopName.Clear();
                    }
                    
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (shop != null && shop.Id != 0)
            { 
                txtShopName.Text = shop.ShopName;
            }
        }
    }
}
