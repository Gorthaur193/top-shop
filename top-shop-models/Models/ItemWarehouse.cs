namespace top_shop_models
{
    public class ItemWarehouse
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string? Comment { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual Item Item { get; set; }
    }
}