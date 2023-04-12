using System.ComponentModel;
using System.Windows;
using top_shop_models;

namespace top_shop_warehouse
{
    public partial class EditItemDialog : Window, INotifyPropertyChanged
    {
        public Item CurrentItem 
        { 
            get => (Item)GetValue(CurrentItemProperty); 
            set
            {
                SetValue(CurrentItemProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItem)));
            }
        }
        public ItemWarehouse CurrentItemWarehouse
        {
            get => (ItemWarehouse)GetValue(CurrentItemWarehouseProperty); 
            set
            {
                SetValue(CurrentItemWarehouseProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItemWarehouse)));
            }
        }

        public static DependencyProperty CurrentItemProperty = DependencyProperty.Register(nameof(CurrentItem), typeof(Item), typeof(EditItemDialog), new PropertyMetadata(new Item()));
        public static DependencyProperty CurrentItemWarehouseProperty = DependencyProperty.Register(nameof(CurrentItemWarehouse), typeof(ItemWarehouse), typeof(EditItemDialog), new PropertyMetadata(new ItemWarehouse()));

        public event PropertyChangedEventHandler? PropertyChanged;

        public EditItemDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}