namespace Banks.Accounts;

public class NotVerificationClient : IVerificationStrategy
{
    public bool IsDeductPossible(decimal limit, decimal value)
    {
        return value <= limit;
    }
}