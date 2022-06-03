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
    /// Interaction logic for PositionList.xaml
    /// </summary>
    public partial class PositionList : UserControl
    {
        public PositionList()
        {
            InitializeComponent();
        }
        ShopDbContext db = new ShopDbContext();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillGrid();
        }

        void FillGrid()
        {
            var list = db.Positions.Include(x => x.Shop).Select(a => new
            {
                positionId = a.Id,
                positionName = a.PositionName,
                shopId = a.ShopId,
                shopName = a.Shop.ShopName
            }).OrderBy(x => x.positionName).ToList();
            List<PositionModel> modelList = new List<PositionModel>();
            foreach (var item in list)
            {
                PositionModel model = new PositionModel();
                model.Id = item.positionId;
                model.PositionName = item.positionName;
                model.ShopId = item.shopId;
                model.ShopName = item.shopName;
                modelList.Add(model);
            }
            gridPosition.ItemsSource = modelList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PositionPage page = new PositionPage();
            page.ShowDialog();
            FillGrid();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            PositionModel model = (PositionModel)gridPosition.SelectedItem;
            if(model!=null && model.Id!=0)
            {
                PositionPage page = new PositionPage();
                page.model = model;
                page.ShowDialog();
                FillGrid();
            }
        }
    }
}
