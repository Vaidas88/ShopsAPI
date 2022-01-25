namespace ShopsAPI.Dtos
{
    public class EditShopItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int ShopId { get; set; }
    }
}
