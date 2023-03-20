using Shops.Entities;

namespace Shops.Exceptions;

public class InvalidPurchaseException : Exception
{
    private InvalidPurchaseException(string message)
        : base(message) { }

    public static InvalidPurchaseException EmptyProductListException(Customer customer)
        => new InvalidPurchaseException($"Customer's (id = {customer.Id}) list of Products is Empty");

    public static InvalidPurchaseException NotEnoughProducts(Shop shop, Customer customer)
        => new InvalidPurchaseException($"There are not enough products from customer (id = {customer.Id}) list in shop (id = {shop.Id})");

    public static InvalidPurchaseException NotEnoughMoney(Customer customer)
        => new InvalidPurchaseException($"Customer (id = {customer.Id}) doesn't have enough money for purchase");

    public static InvalidPurchaseException ShopNotFound(Customer customer)
        => new InvalidPurchaseException($"There is not shop with products from customer (id = {customer.Id}) product list");
}