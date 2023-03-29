namespace top_shop_models
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ItemWarehouse> ItemWarehouses { get; set; }
    }
}