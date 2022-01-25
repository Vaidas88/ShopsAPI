using System.ComponentModel.DataAnnotations;

namespace ShopsAPI.Models
{
    public class ShopItem : Entity
    {
        [Required]
        public decimal Price { get; set; }

        public Shop Shop { get; set; }
    }
}
