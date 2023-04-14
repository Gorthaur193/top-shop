using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace top_shop_models
{
    public class Item
    {
        public Guid Id { get; set; }
        [Required][StringLength(30)] public string Name { get; set; }
        [Required][StringLength(500)] public string Description { get; set; }
        [NotMapped] public string? AvatarLink { get; set; }
        public double Price { get; set; }

        [Required] public virtual ItemType ItemType { get; set; }
        [Required] public virtual Provider Provider { get; set; }

        public virtual ICollection<ItemWarehouse> ItemWarehouses { get; set; }
        public virtual ICollection<ItemOrder> ItemOrders { get; set; }
    }
}