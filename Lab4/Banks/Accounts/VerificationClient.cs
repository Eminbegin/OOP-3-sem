namespace Banks.Accounts;

public class VerificationClient : IVerificationStrategy
{
    public bool IsDeductPossible(decimal limit, decimal value)
    {
        return true;
    }
}