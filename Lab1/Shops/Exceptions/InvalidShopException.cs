using Shops.Entities;
using Shops.Models;
using Shops.Services;

namespace Shops.Exceptions;

public class InvalidShopException : Exception
{
    private InvalidShopException(string message)
        : base(message) { }

    public static InvalidShopException SameShopException(string name, string adress)
        => new InvalidShopException($"There is Shop with name \"{name}\" and adress \"{adress}\"");

    public static InvalidShopException NotSuchShopException(Guid id)
        => new InvalidShopException($"There is not Shop with id \"{id}\"");

    public static InvalidShopException NotSuchProduct(Product product, Shop shop)
        => new InvalidShopException($"There is not product with name \"{product.Name}\" in shop (id = {shop.Id})");
}