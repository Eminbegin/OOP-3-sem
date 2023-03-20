namespace Banks.Accounts;

public interface IVerificationStrategy
{
    bool IsDeductPossible(decimal limit, decimal value);
}