namespace Banks.Models;

public class Address
{
    public Address(string address)
    {
        FullAddress = address;
    }

    public string FullAddress { get; }
}