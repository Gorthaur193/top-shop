using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using top_shop_models;

namespace top_shop_client.UserControls
{
    public partial class ItemCard : UserControl, INotifyPropertyChanged
    {
        public const string defaultAvatarLink = "https://img.freepik.com/free-psd/cap-mockup_1310-495.jpg";
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<RoutedEventArgs> AddClick;
        public event EventHandler<RoutedEventArgs> BuyClick;

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set
            {
                SetValue(CountProperty, value > 99 ? 99 :
                                        value < 0 ? 0 : value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }
        public static DependencyProperty CountProperty = DependencyProperty.Register(nameof(Count), typeof(int), typeof(ItemCard), new PropertyMetadata(0));
        public Item Item
        {
            get => (Item)GetValue(ItemProperty);
            set
            {
                SetValue(ItemProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Item)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvatarUri)));
            }
        }
        public static DependencyProperty ItemProperty = DependencyProperty.Register(nameof(Item), typeof(Item), typeof(ItemCard), new PropertyMetadata(new Item()));
        public Uri AvatarUri => new(Item.AvatarLink ?? defaultAvatarLink, UriKind.Absolute);

        public ItemCard() => InitializeComponent();
        private void BuyButton_Click(object sender, RoutedEventArgs e) => BuyClick?.Invoke(this, e);
        private void AddButton_Click(object sender, RoutedEventArgs e)=> AddClick?.Invoke(this, e);
        private void PlusButton_Click(object sender, RoutedEventArgs e) => Count = Math.Min(99, Count + 1);
        private void MinusButton_Click(object sender, RoutedEventArgs e) => Count = Math.Max(0, Count - 1);
    }
}