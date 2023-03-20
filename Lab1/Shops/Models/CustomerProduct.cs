using Shops.Exceptions;

namespace Shops.Models;

public class CustomerProduct
{
    private const int MinQuantity = 1;
    private int _quantity;

    public CustomerProduct(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }

    public int Quantity
    {
        get
            => _quantity;
        set
        {
            if (value < MinQuantity)
            {
                throw InvalidProductException.ProductQuantity();
            }

            _quantity = value;
        }
    }
}