using System.ComponentModel;
using System.Linq;
using System.Windows;
using top_shop_client.Dialogs;
using top_shop_client.UserControls;
using top_shop_dbconnector;
using top_shop_models;

namespace top_shop_client
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Client? Client { get; set; } 

        public Visibility SignInButtonsVisibility => Client is null ? Visibility.Visible: Visibility.Collapsed;
        public Visibility UserButtonVisibility => Client is not null ? Visibility.Visible: Visibility.Collapsed;

        public TopShopContext db = new();

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public Order Order { get; set; } = new();
        private void ItemCard_BuyClick(object? sender, RoutedEventArgs e)
        {

        }

        private void ItemCard_AddClick(object? sender, RoutedEventArgs e)
        {
            
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            SignInDialog signInDialog = new(db);
            signInDialog.ShowDialog();
            Client= signInDialog.Client;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Client)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SignInButtonsVisibility)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserButtonVisibility)));
            Order.Client = Client!;
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpDialog dialog = new(db);
            dialog.ShowDialog();
        }
    }
}