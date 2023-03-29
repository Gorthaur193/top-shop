namespace top_shop_models
{ 
    public class ItemOrder
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public double Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Item Item { get; set; }
    }
}