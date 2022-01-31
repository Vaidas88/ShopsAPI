using Microsoft.EntityFrameworkCore;
using ShopsAPI.Controllers.Data;
using ShopsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsAPI.Repositories
{
    public class ShopItemRepo : GenericRepo<ShopItem>
    {
        public ShopItemRepo(DataContext context) : base(context)
        {
        }

        public override async Task<List<ShopItem>> GetAllAsync()
        {
            return await _dbSet.Include(shopItem => shopItem.Shop).ToListAsync();
        }
    }
}
