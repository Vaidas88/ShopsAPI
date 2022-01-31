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
    public class ShopItemService
    {
        private readonly ShopItemValidator _shopItemValidator = new ShopItemValidator();
        private readonly IMapper _mapper;
        private readonly ShopItemRepo _shopItemRepo;
        private readonly ShopRepo _shopRepo;

        public ShopItemService(IMapper mapper, ShopItemRepo shopItemRepo, ShopRepo shopRepo)
        {
            _mapper = mapper;
            _shopItemRepo = shopItemRepo;
            _shopRepo = shopRepo;
        }

        public async Task<List<GetShopItemDto>> GetAllAsync()
        {
            var shopItems = await _shopItemRepo.GetAllAsync();
            List<GetShopItemDto> shopItemsDto = new List<GetShopItemDto>();

            shopItems.ForEach(shopItem => shopItemsDto.Add(
                _mapper.Map<GetShopItemDto>(shopItem)
                ));

            return shopItemsDto;
        }

        public async Task<GetShopItemDto> GetByIdAsync(int id)
        {
            var shopItem = await _shopItemRepo.GetByIdAsync(id);

            if (shopItem == null)
            {
                throw new ArgumentNullException("Shop item not found.");
            }

            return _mapper.Map<GetShopItemDto>(shopItem);
        }

        public async Task CreateAsync(CreateShopItemDto createShopItemDto)
        {
            var shopItem = _mapper.Map<ShopItem>(createShopItemDto);
            shopItem.Shop = await _shopRepo.GetByIdAsync(createShopItemDto.ShopId);

            await validateShopItem(shopItem);

            await _shopItemRepo.CreateAsync(shopItem);
        }

        public async Task UpdateAsync(EditShopItemDto editShopItemDto)
        {
            var shopItem = await _shopItemRepo.GetByIdAsync(editShopItemDto.Id);

            if (shopItem == null)
            {
                throw new ArgumentNullException($"There is no shop item with Id: {editShopItemDto.Id}. Nothing to update.");
            }

            if (editShopItemDto.Name != null)
            {
                shopItem.Name = editShopItemDto.Name;
            }
            if (editShopItemDto.Price > 0)
            {
                shopItem.Price = editShopItemDto.Price;
            }

            shopItem.Shop = await _shopRepo.GetByIdAsync(editShopItemDto.ShopId);

            await validateShopItem(shopItem);

            await _shopItemRepo.UpdateAsync(shopItem);
        }

        public async Task DeleteAsync(int id)
        {
            var shopItem = await _shopItemRepo.GetByIdAsync(id);

            if (shopItem == null)
            {
                throw new ArgumentNullException($"There is no shop item with Id: {id}. Nothing to delete.");
            }

            await _shopItemRepo.DeleteAsync(shopItem);
        }

        private async Task<bool> isNameUnique(int id, string name)
        {
            var shopItem = await _shopItemRepo.FindByNameAsync(name);

            if (shopItem != null && id != shopItem.Id)
            {
                return false;
            }

            return true;
        }

        private async Task validateShopItem(ShopItem shopItem)
        {
            var nameUnique = await isNameUnique(shopItem.Id, shopItem.Name);

            if (!nameUnique)
            {
                throw new ArgumentException($"Shop item with name: '{shopItem.Name}' already exists. Can't update to this new name.");
            }

            var shopItemValidated = await _shopItemValidator.ValidateAsync(shopItem);

            if (!shopItemValidated.IsValid)
            {
                throw new ArgumentException(errorsToString(shopItemValidated.Errors));
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
