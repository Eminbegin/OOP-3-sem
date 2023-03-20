using Banks.Accounts;

namespace Banks.Observers.ChangingData;

public interface IChangingDataObserver
{
    void UpdateVerification(IVerificationStrategy verification);
}