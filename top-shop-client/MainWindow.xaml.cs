using System.Linq;
using System.Windows;
using top_shop_client.UserControls;
using top_shop_dbconnector;

namespace top_shop_client
{
    public partial class MainWindow : Window
    {
        public TopShopContext db = new();
        public MainWindow()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            ItemList.Children.Clear();
            foreach (var itemCard in from item in db.Items.ToArray() select new ItemCard() { Item = item })
            {
                itemCard.AddClick += ItemCard_AddClick;
                itemCard.BuyClick += ItemCard_BuyClick;
                ItemList.Children.Add(itemCard);
            }
        }

        private void ItemCard_BuyClick(object? sender, RoutedEventArgs e)
        {

        }

        private void ItemCard_AddClick(object? sender, RoutedEventArgs e)
        {

        }
    }
}