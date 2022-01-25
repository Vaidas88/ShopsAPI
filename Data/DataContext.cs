using Microsoft.EntityFrameworkCore;
using ShopsAPI.Models;

namespace ShopsAPI.Controllers.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        DbSet<Shop> Shops { get; set; }
        DbSet<ShopItem> ShopItems { get; set; }
    }
}
