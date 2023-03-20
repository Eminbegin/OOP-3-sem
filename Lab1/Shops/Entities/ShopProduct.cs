using System.ComponentModel;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class ShopProduct : IEquatable<ShopProduct>
{
    private const decimal MinPrice = 0;
    private const int MinQuantity = 0;
    private int _quantity;
    private decimal _price;
    public ShopProduct(Product product, decimal price, int quantity)
    {
        Product = product;
        Price = price;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < MinQuantity)
            {
                throw InvalidProductException.ProductQuantity();
            }

            _quantity = value;
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value <= MinPrice)
            {
                throw InvalidProductException.ProductPrice();
            }

            _price = value;
        }
    }

    public bool IsSameProduct(CustomerProduct other)
    {
        return Product.Equals(other.Product);
    }

    public bool Equals(ShopProduct? other)
    {
        return other is not null && Product.Equals(other.Product);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ShopProduct);
    }

    public override int GetHashCode()
    {
        return Product.GetHashCode();
    }
}