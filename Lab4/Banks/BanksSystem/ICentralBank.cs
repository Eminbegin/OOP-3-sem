using Banks.BanksSystem.BankConfigurations;
using Banks.Clients;
using Banks.Clients.ClientBuilder;
using Banks.Observers.SkipDays;

namespace Banks.BanksSystem;

public interface ICentralBank : ISkippingTimeObservable
{
    int AddBank(string name, BankConfiguration bankConfiguration);
    IReadOnlyCollection<Bank> Banks();
    int AddPerson(string name, string surname);
    int AddClient(int bankId, int personId, IPreBuild client);
    void SubscribeClientToNotifications(int clientId);
    void DescribeClientToNotifications(int clientId);
    void AddEmailForMessages(int clientId);
    int AddDebitAccount(int clientId, decimal balance);
    int AddCreditAccount(int clientId, decimal balance);
    int AddDepositAccount(int clientId, decimal balance);
    Bank GetBankById(int bankId);
    int AddMoney(int accountId, decimal money);
    int TakeOffMoney(int accountId, decimal money);
    void SkipNDays(int days);
    int MakeTransfer(int accountFrom, int accountTo, decimal money);
    void RemoveTransaction(int transactionId);
    void SetPassport(int clientId, string passport);
    void SetAddress(int clientId, string address);
    void SetEmail(int clientId, string email);
    bool SetDebitPercentage(int bankId, decimal value);
    bool SetCreditPercentage(int bankId, decimal value);
    bool SetCreditLimit(int bankId, decimal value);
    bool SetLimitForDoubtful(int bankId, decimal value);
    bool SetDepositDays(int bankId, int value);
    bool SetDepositPercentages(int bankId, DepositPercentages value);
    public void IsAccountExists(int accountId);
    public void IsClientExists(int clientId);
}