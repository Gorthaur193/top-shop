using System.ComponentModel.DataAnnotations;

namespace top_shop_models
{
    public class Provider
    {
        public Guid Id { get; set; }
        [Required][StringLength(30)] public string Name { get; set; }
        [Required][StringLength(200)] public string Description { get; set; }
        [Required][StringLength(100)] public string TIN { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public override string ToString() => Name;
    }
}