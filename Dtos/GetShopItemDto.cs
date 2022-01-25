﻿namespace ShopsAPI.Dtos
{
    public class GetShopItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public GetShopDto Shop { get; set; }
    }
}