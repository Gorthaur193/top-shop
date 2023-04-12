using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using top_shop_dbconnector;
using top_shop_models;

namespace top_shop_warehouse
{
    public partial class WarehouseSettings : Window
    {
        private const string settingsFilePath = @"warehouse_appsettings.json";
        private TopShopContext db;

        public ObservableCollection<Warehouse> Warehouses { get; set; }
        public Warehouse CurrentWarehouse { get; set; }
        public WarehouseSettings(bool show, TopShopContext db)
        {
            this.db = db;
            Configure();
            if (show)
                InitializeComponent();
            else
                Close();
        }
        private void Configure()
        {
            Warehouses = new(db.Warehouses.ToArray());
            if (File.Exists(settingsFilePath))
            {
                var settingsJson = JObject.Parse(File.ReadAllText(settingsFilePath));
                CurrentWarehouse = Warehouses.FirstOrDefault(x => x.Id == (Guid)(settingsJson["WarehouseId"] ?? Guid.Empty)) ?? Warehouses.First();
            }
            CurrentWarehouse ??= Warehouses.First();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            File.WriteAllText(settingsFilePath, JObject.FromObject(new
            {
                WarehouseId = CurrentWarehouse.Id
            }).ToString());
        }
    }
}