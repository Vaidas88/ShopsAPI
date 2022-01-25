using Microsoft.EntityFrameworkCore;
using ShopsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
