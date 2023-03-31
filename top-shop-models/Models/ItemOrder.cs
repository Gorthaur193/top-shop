using System.ComponentModel.DataAnnotations;

namespace top_shop_models
{
    public class ItemOrder
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public double Discount { get; set; }

        [Required] public virtual Order Order { get; set; }
        [Required] public virtual Item Item { get; set; }
    }
}