using System;
using System.Collections.Generic;

namespace ShopsAPI.Models
{
    public class Shop : Entity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<ShopItem> ShopItems { get; set; }
    }
}
