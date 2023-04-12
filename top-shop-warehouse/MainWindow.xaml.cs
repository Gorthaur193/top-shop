using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using top_shop_dbconnector;
using top_shop_models;

namespace top_shop_warehouse
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Warehouse CurrentWarehouse { get; set; }
        public Provider? CurrentProvider { get; set; }
        public Item? CurrentItem { get; set; }
        public ObservableCollection<Provider> Providers { get; set; } 
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<ItemType> ItemTypes { get; set; }

        private TopShopContext db = new();
        public MainWindow() 
        {
            CurrentWarehouse = new WarehouseSettings(false, db).CurrentWarehouse;
            Providers = new(db.Providers.ToArray());
            Items = new(db.Items.ToArray());
            ItemTypes = new(db.ItemTypes.ToArray());
            InitializeComponent();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void Providers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = (DataGrid)sender;
            CurrentProvider = datagrid.SelectedItem as Provider;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentProvider)));
        }
        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = (DataGrid)sender;
            CurrentItem = datagrid.SelectedItem as Item;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItem)));
        }

        private bool _itemsCommiting = false;
        private void DataGrid_EditFinished(object sender, DataGridRowEditEndingEventArgs e)
        {
            var datagrid = (DataGrid)sender;
            if (!_itemsCommiting)
            {
                _itemsCommiting = true;
                datagrid.CommitEdit();
                dynamic item = e.Row.Item; //todo: fix this crutch

                if (item.Id == Guid.Empty)
                    db.Add(item);
                else
                    db.Update(item);
                db.SaveChanges();
                _itemsCommiting = false;
                datagrid.Items.Refresh();
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var warehouseSettingsForm = new WarehouseSettings(true, db);
            warehouseSettingsForm.ShowDialog();
            CurrentWarehouse = warehouseSettingsForm.CurrentWarehouse;
            // todo: trigger data update for current warehouse
        }
    }
}