namespace top_shop_models
{
    public class Provider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TIN { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}