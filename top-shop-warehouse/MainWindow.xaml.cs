using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using top_shop_dbconnector;
using top_shop_models;

namespace top_shop_warehouse // todo: create button for itemtype list
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Provider? CurrentProvider { get; set; }
        public Item? CurrentItem { get; set; }
        public ObservableCollection<Provider> Providers { get; set; } 
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<ItemType> ItemTypes { get; set; }

        private TopShopContext db = new();
        public MainWindow() 
        {
            Providers = new(db.Providers.ToArray());
            Items = new(db.Items.ToArray());
            ItemTypes = new(db.ItemTypes.ToArray());
            InitializeComponent();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _providersCommiting = false;
        private void Providers_Edit(object sender, DataGridRowEditEndingEventArgs e)
        {
            var datagrid = (DataGrid)sender;
            if (!_providersCommiting)
            {
                _providersCommiting = true;
                datagrid.CommitEdit();
                var provider = (Provider)e.Row.Item;

                if (provider.Id == Guid.Empty)
                    db.Add(provider);
                else
                    db.Update(provider);
                db.SaveChanges();
                _providersCommiting = false;
                datagrid.Items.Refresh();
            }
        }
        private void Providers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = (DataGrid)sender;
            CurrentProvider = datagrid.SelectedItem as Provider;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentProvider)));
        }

        private bool _itemsCommiting = false;
        private void Items_Edit(object sender, DataGridRowEditEndingEventArgs e)
        {
            var datagrid = (DataGrid)sender;
            if (!_itemsCommiting)
            {
                _itemsCommiting = true;
                datagrid.CommitEdit();
                var item = (Item)e.Row.Item;

                if (item.Id == Guid.Empty)
                    db.Add(item);
                else
                    db.Update(item);
                db.SaveChanges();
                _itemsCommiting = false;
                datagrid.Items.Refresh();
            }
        }
        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid datagrid = (DataGrid)sender;
            CurrentItem = datagrid.SelectedItem as Item;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentItem)));
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}