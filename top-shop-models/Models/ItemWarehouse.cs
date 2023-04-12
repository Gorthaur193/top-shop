using System.ComponentModel.DataAnnotations;

namespace top_shop_models
{
    public class ItemWarehouse // todo unique index ItemWarehouse based on Item and Warehouse
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        [StringLength(500)] public string? Comment { get; set; }

        [Required] public virtual Warehouse Warehouse { get; set; }
        [Required] public virtual Item Item { get; set; }
    }
}