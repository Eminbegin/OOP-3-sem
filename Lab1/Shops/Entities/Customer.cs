using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Customer : IEquatable<Customer>
{
    private const decimal MinBalance = 0;

    private readonly List<CustomerProduct> _products;
    private decimal _balance;

    public Customer(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
        Id = Guid.NewGuid();
        _products = new List<CustomerProduct>();
    }

    public string Name { get; }
    public Guid Id { get; }
    public IReadOnlyCollection<CustomerProduct> ProductList => _products;
    public decimal Balance
    {
        get
            => _balance;
        set
        {
            if (value < MinBalance)
            {
                throw InvalidCustomerException.NegativeBalance();
            }

            _balance = value;
        }
    }

    public bool IsEnough(decimal balance)
        => Balance >= balance;

    public void AddProduct(Product product, int quantity)
    {
        if (_products.Any(cp => cp.Product.Equals(product)))
        {
            _products.First(cp => cp.Product.Equals(product)).Quantity += quantity;
        }
        else
        {
            _products.Add(new CustomerProduct(product, quantity));
        }
    }

    public void Purchase(decimal amount)
    {
        Balance -= amount;
        _products.Clear();
    }

    public bool Equals(Customer? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Customer);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}