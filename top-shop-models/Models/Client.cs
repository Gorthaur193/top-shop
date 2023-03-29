namespace top_shop_models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Discount { get; set; }
        public string Username { get; set; }
        public string Passhash { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}