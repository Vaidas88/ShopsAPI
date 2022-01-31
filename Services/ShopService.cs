using AutoMapper;
using ShopsAPI.Dtos;
using ShopsAPI.Models;
using ShopsAPI.Repositories;
using ShopsAPI.Validators;
using System;
using System.Collections.Generic;
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

        public async Task<List<ShopDto>> GetAllAsync()
        {
            var shops = await _shopRepo.GetAllAsync();
            List<ShopDto> shopsDto = new List<ShopDto>();

            shops.ForEach(shop => shopsDto.Add(
                _mapper.Map<ShopDto>(shop)
                ));

            return shopsDto;
        }

        public async Task<ShopDto> GetByIdAsync(int id)
        {
            var shop = await _shopRepo.GetByIdAsync(id);

            if (shop == null)
            {
                throw new ArgumentNullException("Shop not found.");
            }

            return _mapper.Map<ShopDto>(shop);
        }

        public async Task CreateAsync(CreateShopDto createShopDto)
        {
            var shop = _mapper.Map<Shop>(createShopDto);

            await validateShop(shop);

            await _shopRepo.CreateAsync(shop);
        }

        public async Task UpdateAsync(EditShopDto editShopDto)
        {
            var shop = await _shopRepo.GetByIdAsync(editShopDto.Id);

            if (shop == null)
            {
                throw new ArgumentNullException($"There is no shop with Id: {editShopDto.Id}. Nothing to update.");
            }

            shop.Name = editShopDto.Name;

            await validateShop(shop);

            await _shopRepo.UpdateAsync(shop);
        }

        public async Task DeleteAsync(int id)
        {
            var shop = await _shopRepo.GetByIdAsync(id);

            if (shop == null)
            {
                throw new ArgumentNullException($"There is no shop with Id: {id}. Nothing to delete.");
            }

            await _shopRepo.DeleteAsync(shop);
        }

        private async Task<bool> isNameUnique(string name)
        {
            var shop = await _shopRepo.FindByNameAsync(name);
            if (shop != null)
            {
                return false;
            }

            return true;
        }

        private async Task validateShop(Shop shop)
        {
            var nameUnique = await isNameUnique(shop.Name);
            if (!nameUnique)
            {
                throw new ArgumentException($"Shop with name: '{shop.Name}' already exists. Can't update to this new name.");
            }

            var shopValidated = await _shopValidator.ValidateAsync(shop);

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
                errorsString += $"Error for: {error.PropertyName}, error was: {error.ErrorMessage}\n";
            }

            return errorsString;
        }
    }
}
