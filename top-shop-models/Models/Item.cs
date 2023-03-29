namespace top_shop_models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public virtual ItemType ItemType { get; set; }
        public virtual Provider Provider { get; set; }

        public virtual ICollection<ItemWarehouse> ItemWarehouses { get; set; }
        public virtual ICollection<ItemOrder> ItemOrders { get; set; }
    }
}