using System;

namespace ShopsAPI.Dtos
{
    public class GetShopDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
