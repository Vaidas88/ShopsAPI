using AutoMapper;
using ShopsAPI.Dtos;
using ShopsAPI.Models;
using ShopsAPI.Repositories;
using ShopsAPI.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsAPI.Services
{
    public class ShopService
    {
        private readonly ShopValidator _shopValidator = new ShopValidator();
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
                throw new ArgumentNullException("Shop not found.");
            }

            return _mapper.Map<ShopDto>(shop);
        }

        public void Create(CreateShopDto createShopDto)
        {
            var shop = _mapper.Map<Shop>(createShopDto);

            validateShop(shop);

            _shopRepo.Create(shop);
            _shopRepo.SaveChanges();
        }

        public void Update(EditShopDto editShopDto)
        {
            var shop = _shopRepo.GetById(editShopDto.Id);

            if (shop == null)
            {
                throw new ArgumentNullException($"There is no shop with Id: {editShopDto.Id}. Nothing to update.");
            }

            shop.Name = editShopDto.Name;

            validateShop(shop);

            _shopRepo.Update(shop);
            _shopRepo.SaveChanges();
        }

        public void Delete(int id)
        {
            var shop = _shopRepo.GetById(id);

            if (shop == null)
            {
                throw new ArgumentNullException($"There is no shop with Id: {id}. Nothing to delete.");
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

        private void validateShop(Shop shop)
        {
            if (!isNameUnique(shop.Name))
            {
                throw new ArgumentException($"Shop with name: '{shop.Name}' already exists. Can't update to this new name.");
            }

            var shopValidated = _shopValidator.Validate(shop);

            if (!shopValidated.IsValid)
            {
                throw new ArgumentException(errorsToString(shopValidated.Errors));
            }
        }

        private string errorsToString(List<FluentValidation.Results.ValidationFailure> errors)
        {
            string errorsString = string.Empty;

            foreach (var error in errors)
            {
                errorsString = errorsString + $"Error for: {error.PropertyName}, error was: {error.ErrorMessage}\n";
            }

            return errorsString;
        }
    }
}
