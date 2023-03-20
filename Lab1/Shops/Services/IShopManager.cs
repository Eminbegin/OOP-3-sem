using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    Shop RegistrateShop(string name, string address);
    Customer RegistrateCustomer(string name, decimal balance);
    void MobilizeCustomer(Guid id);
    Product RegistrateProduct(string name);
    Customer? FindCustomer(Guid id);
    Customer GetCustomer(Guid id);
    Shop? FindShop(Guid id);
    Shop GetShop(Guid id);
    void DonateCustomer(Guid id, decimal amount);
    void UpdatePrice(Shop shop, Product product, decimal newPrice);
    Product SupplyProduct(Shop shop, string name, decimal price, int quantity);
    void AddToCart(Customer customer, Product product, int quantity);
    void Purchase(Shop shop, Customer customer);
    void MinPurchase(Customer customer);
}