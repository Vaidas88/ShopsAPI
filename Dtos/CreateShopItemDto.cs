namespace ShopsAPI.Dtos
{
    public class CreateShopItemDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ShopId { get; set; }
    }
}
