using System.ComponentModel.DataAnnotations;

namespace top_shop_models
{
    public class ItemType
    {
        public Guid Id { get; set; }
        [Required][StringLength(30)] public string Name { get; set; }

        public override string ToString() => Name;
    }
}