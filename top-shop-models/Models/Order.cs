using System.ComponentModel.DataAnnotations;

namespace top_shop_models
{
    public class Order
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }

        [Required] public virtual Client Client { get; set; }
        public virtual ICollection<ItemOrder> ItemOrders { get; set; }
    }
}