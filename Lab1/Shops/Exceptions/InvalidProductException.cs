using Shops.Models;

namespace Shops.Exceptions;

public class InvalidProductException : Exception
{
    private InvalidProductException(string message)
        : base(message) { }

    public static InvalidProductException SameProductException(string name)
        => new InvalidProductException($"There is Product with name \"{name}\"");

    public static InvalidProductException NotSuchProductException(Product product)
        => new InvalidProductException($"There is not Product \"{product.Name}\" in shops");

    public static InvalidProductException ProductQuantity()
        => new InvalidProductException($"Quantity of product can't be negative");

    public static InvalidProductException ProductPrice()
        => new InvalidProductException($"Price of product must be positive");
}