using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop : IEquatable<Shop>
{
    private readonly List<ShopProduct> _products;
    private decimal _balance;
    public Shop(string name, string address)
    {
        Name = name;
        Address = address;
        _products = new List<ShopProduct>();
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Address { get; }
    public decimal Balance
    {
        get => _balance;
        set => _balance = value;
    }

    public void AddProduct(Product product, int quantity, decimal price)
    {
        if (_products.Any(p => p.Product.Equals(product)))
        {
            ShopProduct shopProduct = _products.First(p => p.Product.Equals(product));
            shopProduct.Quantity += quantity;
            shopProduct.Price = price;
            return;
        }

        _products.Add(new ShopProduct(product, price, quantity));
    }

    public decimal GetSummaryCost(IReadOnlyCollection<CustomerProduct> customerProducts)
    {
        return customerProducts
            .Select(cp => cp.Quantity * _products
                .First(sp => sp.IsSameProduct(cp)).Price)
            .Sum();
    }

    public bool CheckPossibility(IReadOnlyCollection<CustomerProduct> customerProducts)
    {
        return customerProducts
            .All(cp => _products
                .Any(sp => sp.IsSameProduct(cp) && sp.Quantity >= cp.Quantity));
    }

    public void Purchase(Customer customer)
    {
        IReadOnlyCollection<CustomerProduct> customerProducts = customer.ProductList;
        foreach (CustomerProduct customerProduct in customerProducts)
        {
            _products.First(sp => sp.IsSameProduct(customerProduct)).Quantity -= customerProduct.Quantity;
        }

        Balance += GetSummaryCost(customerProducts);
    }

    public void SetPrice(Product product, decimal newPrice)
    {
        if (!_products.Any(sp => sp.Product.Equals(product)))
        {
            throw InvalidShopException.NotSuchProduct(product, this);
        }

        _products.First(sp => sp.Product.Equals(product)).Price = newPrice;
    }

    public bool Equals(Shop? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Shop);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}