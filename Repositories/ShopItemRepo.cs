using Microsoft.EntityFrameworkCore;
using ShopsAPI.Controllers.Data;
using ShopsAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopsAPI.Repositories
{
    public class ShopItemRepo : GenericRepo<ShopItem>
    {
        public ShopItemRepo(DataContext context) : base(context)
        {
        }

        public override List<ShopItem> GetAll()
        {
            return _dbSet.Include(shopItem => shopItem.Shop).ToList();
        }

        public ShopItem FindByName(string name)
        {
            return _dbSet.Where(shopItem => shopItem.Name == name).SingleOrDefault();
        }
    }
}
