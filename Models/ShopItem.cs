using System.ComponentModel.DataAnnotations;

namespace ShopsAPI.Models
{
    public class ShopItem : Entity
    {
        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        public Shop Shop { get; set; }
    }
}
