using Microsoft.EntityFrameworkCore;
using ShopsAPI.Controllers.Data;
using ShopsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsAPI.Repositories
{
    public class ShopRepo : GenericRepo<Shop>
    {
        public ShopRepo(DataContext context) : base(context)
        {
        }

        public override async Task<List<Shop>> GetAllAsync()
        {
            return await _dbSet.Include(shop => shop.ShopItems).ToListAsync();
        }
    }
}
