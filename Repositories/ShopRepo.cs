﻿using Microsoft.EntityFrameworkCore;
using ShopsAPI.Controllers.Data;
using ShopsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsAPI.Repositories
{
    public class ShopRepo : GenericRepo<Shop>
    {
        public ShopRepo(DataContext context) : base(context)
        {
        }

        public override List<Shop> GetAll()
        {
            return _dbSet.Include(shop => shop.ShopItems).ToList();
        }

        public Shop FindByName(string name)
        {
            return _dbSet.Where(shop => shop.Name == name).SingleOrDefault();
        }
    }
}