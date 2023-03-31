using System.ComponentModel.DataAnnotations;

namespace top_shop_models
{
    public class Client
    {
        public Guid Id { get; set; }
        [Required][StringLength(100)] public string Name { get; set; }
        [Required][StringLength(15)] public string Phone { get; set; }
        public double Discount { get; set; }
        [Required][StringLength(30)] public string Username { get; set; }
        [Required] public string Passhash { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}