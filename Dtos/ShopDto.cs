using System;
using System.Collections.Generic;

namespace ShopsAPI.Dtos
{
    public class ShopDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<ShopItemDto> ShopItems { get; set; }
    }
}
