using AutoMapper;
using ShopsAPI.Dtos;
using ShopsAPI.Models;
using ShopsAPI.Repositories;
using ShopsAPI.Validators;
using System;
using System.Collections.Generic;

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

        public List<GetShopItemDto> GetAll()
        {
            var shopItems = _shopItemRepo.GetAll();
            List<GetShopItemDto> shopItemsDto = new List<GetShopItemDto>();

            shopItems.ForEach(shopItem => shopItemsDto.Add(
                _mapper.Map<GetShopItemDto>(shopItem)
                ));

            return shopItemsDto;
        }

        public GetShopItemDto GetById(int id)
        {
            var shopItem = _shopItemRepo.GetById(id);

            if (shopItem == null)
            {
                throw new ArgumentNullException("Shop item not found.");
            }

            return _mapper.Map<GetShopItemDto>(shopItem);
        }

        public void Create(CreateShopItemDto createShopItemDto)
        {
            var shopItem = _mapper.Map<ShopItem>(createShopItemDto);
            shopItem.Shop = _shopRepo.GetById(createShopItemDto.ShopId);

            validateShopItem(shopItem);

            _shopItemRepo.Create(shopItem);
            _shopItemRepo.SaveChanges();
        }

        public void Update(EditShopItemDto editShopItemDto)
        {
            var shopItem = _shopItemRepo.GetById(editShopItemDto.Id);

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

            shopItem.Shop = _shopRepo.GetById(editShopItemDto.ShopId);

            validateShopItem(shopItem);

            _shopItemRepo.Update(shopItem);
            _shopItemRepo.SaveChanges();
        }

        public void Delete(int id)
        {
            var shopItem = _shopItemRepo.GetById(id);

            if (shopItem == null)
            {
                throw new ArgumentNullException($"There is no shop item with Id: {id}. Nothing to delete.");
            }

            _shopItemRepo.Delete(shopItem);
            _shopItemRepo.SaveChanges();
        }

        private bool isNameUnique(int id, string name)
        {
            var shopItem = _shopItemRepo.FindByName(name);
            if (shopItem != null && id != shopItem.Id)
            {
                return false;
            }

            return true;
        }

        private void validateShopItem(ShopItem shopItem)
        {
            if (!isNameUnique(shopItem.Id, shopItem.Name))
            {
                throw new ArgumentException($"Shop item with name: '{shopItem.Name}' already exists. Can't update to this new name.");
            }

            var shopItemValidated = _shopItemValidator.Validate(shopItem);

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
                errorsString = errorsString + $"Error for: {error.PropertyName}, error was: {error.ErrorMessage}\n";
            }

            return errorsString;
        }
    }
}
