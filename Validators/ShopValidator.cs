using FluentValidation;
using ShopsAPI.Models;

namespace ShopsAPI.Validators
{
    public class ShopValidator : AbstractValidator<Shop>
    {
        public ShopValidator()
        {
            RuleFor(shop => shop.Name).MinimumLength(4);
        }
    }
}
