using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using top_shop_dbconnector;
using top_shop_models;

namespace top_shop_warehouse
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public const string defaultAvatarImage = "https://img.freepik.com/premium-psd/paper-bag-concept-with-mock-up_23-2148807278.jpg";

        public Warehouse CurrentWarehouse { get; set; }
        public Provider? CurrentProvider { get; set; }
        public Item? CurrentItem { get; set; }
        public Uri CurrentItemAvatarUri => new(CurrentItem?.AvatarLink ?? defaultAvatarImage);
        public ItemWarehouse? CurrentItemWarehouse =>
            ItemWarehouses.FirstOrDefault(x => x.Item == CurrentItem);
        public ObservableCollection<Provider> Providers { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<ItemType> ItemTypes { get; set; }
        private List<ItemWarehouse> ItemWarehouses { get; set; }

        private TopShopContext db = new();
        public MainWindow()
        {
            CurrentWarehouse = new WarehouseSettings(false, db).CurrentWarehouse;
            Providers = new(db.Providers.ToArray());
            Items = new(db.Items.ToArray());
            ItemTypes = new(db.ItemTypes.ToArray());
            ItemWarehouses = new(db.ItemWarehouses.Where(x => x.Warehouse == CurrentWarehouse).ToArray());
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
            Item? item = datagrid.SelectedItem as Item;
            if (item is not null && ItemWarehouses.FirstOrDefault(x => x.Item == item) is null)
                if (db.Entry(item).State == EntityState.Unchanged || db.Entry(item).State == EntityState.Modified)
                {
                    var newItemWarehouse = new ItemWarehouse()
                    {
                        Amount = 0,
                        Comment = "",
                        Warehouse = CurrentWarehouse,
                        Item = item
                    };
                    db.ItemWarehouses.Add(newItemWarehouse);
                    db.SaveChanges();
                    ItemWarehouses.Add(newItemWarehouse);
                }
            CurrentItem = item;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItem)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItemAvatarUri)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItemWarehouse)));
        }

        private bool _itemsCommiting = false;
        private void DataGrid_EditFinished(object sender, DataGridRowEditEndingEventArgs e)
        {
            var datagrid = (DataGrid)sender;
            if (!_itemsCommiting)
            {
                _itemsCommiting = true;
                datagrid.CommitEdit();
                try
                {
                    dynamic item = e.Row.Item; //todo: fix this crutch

                    if (item.Id == Guid.Empty)
                        db.Add(item);
                    else
                        db.Update(item);
                    db.SaveChanges();
                }
                catch { } // i dont care really
                finally
                {
                    _itemsCommiting = false;
                    datagrid.Items.Refresh();
                }
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

        private void EditItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentItem is not null && CurrentItemWarehouse is not null)
            {
                var dialog = new EditItemDialog()
                {
                    CurrentItem = CurrentItem,
                    CurrentItemWarehouse = CurrentItemWarehouse
                };
                dialog.ShowDialog();
                db.SaveChanges();
            }
        }
    }
}