using FluentValidation;
using ShopsAPI.Models;

namespace ShopsAPI.Validators
{
    public class ShopItemValidator : AbstractValidator<ShopItem>
    {
        public ShopItemValidator()
        {
            RuleFor(shopItem => shopItem.Name).MinimumLength(4);
            RuleFor(ShopItem => ShopItem.Price).GreaterThan(0M);
        }
    }
}
