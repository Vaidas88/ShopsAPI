using AutoMapper;
using ShopsAPI.Dtos;
using ShopsAPI.Models;
using ShopsAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsAPI.Services
{
    public class ShopService
    {
        private readonly IMapper _mapper;
        private readonly ShopRepo _shopRepo;

        public ShopService(IMapper mapper, ShopRepo shopRepo)
        {
            _mapper = mapper;
            _shopRepo = shopRepo;
        }

        public List<ShopDto> GetAll()
        {
            var shops = _shopRepo.GetAll();
            List<ShopDto> shopsDto = new List<ShopDto>();

            shops.ForEach(shop => shopsDto.Add(
                _mapper.Map<ShopDto>(shop)
                ));

            return shopsDto;
        }

        public ShopDto GetById(int id)
        {
            var shop = _shopRepo.GetById(id);

            if (shop == null)
            {
                throw new ArgumentException("Shop not found.");
            }

            return _mapper.Map<ShopDto>(shop);
        }

        public void Create(CreateShopDto createShopDto)
        {
            if (!isNameUnique(createShopDto.Name))
            {
                throw new ArgumentException($"Shop with name: '{createShopDto.Name}' already exists.");
            }

            var shop = _mapper.Map<Shop>(createShopDto);
            _shopRepo.Create(shop);
            _shopRepo.SaveChanges();
        }

        public void Update(EditShopDto editShopDto)
        {
            var shop = _shopRepo.GetById(editShopDto.Id);

            if (shop == null)
            {
                throw new ArgumentException($"There is no shop with Id: {editShopDto.Id}. Nothing to update.");
            }

            if (!isNameUnique(editShopDto.Name))
            {
                throw new ArgumentException($"Shop with name: '{editShopDto.Name}' already exists. Can't update to this new name.");
            }

            shop.Name = editShopDto.Name;

            _shopRepo.Update(shop);
            _shopRepo.SaveChanges();
        }

        public void Delete(int id)
        {
            var shop = _shopRepo.GetById(id);

            if (shop == null)
            {
                throw new ArgumentException($"There is no shop with Id: {id}. Nothing to delete.");
            }

            _shopRepo.Delete(shop);
            _shopRepo.SaveChanges();
        }

        private bool isNameUnique(string name)
        {
            if (_shopRepo.FindByName(name) != null)
            {
                return false;
            }

            return true;
        }
    }
}
