using Shops.Entities;

namespace Shops.Exceptions;

public class InvalidCustomerException : Exception
{
    private InvalidCustomerException(string message)
        : base(message) { }

    public static InvalidCustomerException NotSuchCustmoer(Guid id)
        => new InvalidCustomerException($"There is not Customer with id = {id}");

    public static InvalidCustomerException NegativeBalance()
        => new InvalidCustomerException($"Balance can't be negative");
}