using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private List<Customer> _customers;
    private List<Shop> _shops;
    private List<Product> _products;

    public ShopManager()
    {
        _customers = new List<Customer>();
        _shops = new List<Shop>();
        _products = new List<Product>();
    }

    public IReadOnlyCollection<Shop> Shops => _shops;

    public Shop RegistrateShop(string name, string address)
    {
        ArgumentNullException.ThrowIfNull(name, "Impossible registrate shop without name");
        ArgumentNullException.ThrowIfNull(address, "Impossible registrate shop without adress");

        if (_shops.Any(s => s.Address.Equals(address) && s.Name.Equals(name)))
        {
            throw InvalidShopException.SameShopException(name, address);
        }

        var shop = new Shop(name, address);
        _shops.Add(shop);
        return shop;
    }

    public Customer RegistrateCustomer(string name, decimal balance)
    {
        ArgumentNullException.ThrowIfNull(name, "Impossible registrate customer without name");
        var customer = new Customer(name, balance);
        _customers.Add(customer);
        return customer;
    }

    public void MobilizeCustomer(Guid id)
    {
        _customers.Remove(GetCustomer(id));
    }

    public Product RegistrateProduct(string name)
    {
        ArgumentNullException.ThrowIfNull(name, "Impossible registrate customer without name");
        if (_products.Any(p => p.Name.Equals(name)))
        {
            throw InvalidProductException.SameProductException(name);
        }

        var product = new Product(name);
        _products.Add(product);
        return product;
    }

    public Customer? FindCustomer(Guid id)
        => _customers.FirstOrDefault(c => c.Id.Equals(id));

    public Customer GetCustomer(Guid id)
    {
        Customer? customer = FindCustomer(id);
        if (customer is null)
        {
            throw InvalidCustomerException.NotSuchCustmoer(id);
        }

        return customer;
    }

    public Shop? FindShop(Guid id)
        => _shops.FirstOrDefault(s => s.Id.Equals(id));

    public Shop GetShop(Guid id)
    {
        Shop? shop = FindShop(id);
        if (shop is null)
        {
            throw InvalidShopException.NotSuchShopException(id);
        }

        return shop;
    }

    public void DonateCustomer(Guid id, decimal amount)
    {
        Customer customer = GetCustomer(id);
        customer.Balance += amount;
    }

    public void UpdatePrice(Shop shop, Product product, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(shop, "Impossible update price without shop");
        ArgumentNullException.ThrowIfNull(product, "Impossible update price without product");

        if (_shops.Any(s => s.Equals(shop)))
        {
            throw InvalidShopException.NotSuchShopException(shop.Id);
        }

        _shops.First(s => s.Equals(shop)).SetPrice(product, newPrice);
    }

    public Product SupplyProduct(Shop shop, string name, decimal price, int quantity)
    {
        ArgumentNullException.ThrowIfNull(shop, "Impossible supply product without shop");
        ArgumentNullException.ThrowIfNull(name, "Impossible supply product without name");
        Product product = GetOrCreate(name);
        shop.AddProduct(product, quantity, price);
        return product;
    }

    public void AddToCart(Customer customer, Product product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product, "Impossible add null product to cart");
        ArgumentNullException.ThrowIfNull(customer, "Impossible add product to null customer's cart");
        customer.AddProduct(product, quantity);
    }

    public void Purchase(Shop shop, Customer customer)
    {
        if (!_shops.Any(s => s.Equals(shop)))
        {
            throw InvalidShopException.NotSuchShopException(shop.Id);
        }

        if (!_customers.Any(c => c.Equals(customer)))
        {
            throw InvalidCustomerException.NotSuchCustmoer(customer.Id);
        }

        IReadOnlyCollection<CustomerProduct> customerProducts = customer.ProductList;

        if (!customerProducts.Any())
        {
            throw InvalidPurchaseException.EmptyProductListException(customer);
        }

        if (!shop.CheckPossibility(customerProducts))
        {
            throw InvalidPurchaseException.NotEnoughProducts(shop, customer);
        }

        decimal finalCost = shop.GetSummaryCost(customerProducts);

        if (!customer.IsEnough(finalCost))
        {
            throw InvalidPurchaseException.NotEnoughMoney(customer);
        }

        shop.Purchase(customer);
        customer.Purchase(finalCost);
    }

    public void MinPurchase(Customer customer)
    {
        Shop? minShop = null;
        decimal minCost = decimal.MaxValue;
        IReadOnlyCollection<CustomerProduct> customerProducts = customer.ProductList;
        if (!customerProducts.Any())
        {
            throw InvalidPurchaseException.EmptyProductListException(customer);
        }

        foreach (Shop shop in _shops)
        {
            if (shop.CheckPossibility(customerProducts))
            {
                if (shop.GetSummaryCost(customerProducts) < minCost)
                {
                    minShop = shop;
                    minCost = minShop.GetSummaryCost(customerProducts);
                }
            }
        }

        if (minShop is null)
        {
            throw InvalidPurchaseException.ShopNotFound(customer);
        }

        if (!customer.IsEnough(minCost))
        {
            throw InvalidPurchaseException.NotEnoughMoney(customer);
        }

        minShop.Purchase(customer);
        customer.Purchase(minCost);
    }

    private Product GetOrCreate(string name)
    {
        Product? product = _products.FirstOrDefault(p => p.Name.Equals(name));
        if (product is not null)
        {
            return product;
        }

        product = new Product(name);
        _products.Add(product);
        return product;
    }
}