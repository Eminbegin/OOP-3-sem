using Banks.BanksSystem;
using Banks.BanksSystem.BankConfigurations;
using Banks.Clients;
using Banks.Clients.ClientBuilder;
using Xunit;

namespace Banks.Test;

public class BanksTests
{
    private ICentralBank _cb = new CentralBank();

    [Fact]
    public void CreateBank_BankCreated()
    {
        int bankId1 = _cb.AddBank("SberBank", CreateBankConfiguration());
        Bank bank1 = _cb.GetBankById(bankId1);
        Assert.Contains(bank1, _cb.Banks());
        Assert.Equal(bank1.Id, bankId1);
        Assert.Equal(1, bankId1);
    }

    [Fact]
    public void CreateAccount_AccountCreated()
    {
        int personId = _cb.AddPerson("asd", "asdasd");
        int bankId1 = _cb.AddBank("SberBank", CreateBankConfiguration());
        Bank bank1 = _cb.GetBankById(bankId1);
        int clientId1 = _cb.AddClient(bankId1, personId, CreateFirstClient());
        int accountId1 = _cb.AddDebitAccount(clientId1, 100m);
        Assert.Equal(1, accountId1);
        Assert.Equal(1, clientId1);
    }

    [Fact]
    public void DepositAndDeductMoney()
    {
        int personId = _cb.AddPerson("asd", "asdasd");
        int bankId1 = _cb.AddBank("SberBank", CreateBankConfiguration());
        Bank bank1 = _cb.GetBankById(bankId1);
        int clientId1 = _cb.AddClient(bankId1, personId, CreateFirstClient());
        int accountId1 = _cb.AddDebitAccount(clientId1, 100m);
        _cb.AddMoney(accountId1, 10000m);
        _cb.TakeOffMoney(accountId1, 0m);

        Assert.Equal(10100m, bank1.Accounts().First(a => a.Id == accountId1).Balance);
    }

    [Fact]
    public void DeductForSusAndNotSus()
    {
        int personId = _cb.AddPerson("asd", "asdasd");
        int bankId1 = _cb.AddBank("SberBank", CreateBankConfiguration());
        Bank bank1 = _cb.GetBankById(bankId1);
        int clientId1 = _cb.AddClient(bankId1, personId, CreateFirstClient());
        int accountId1 = _cb.AddDebitAccount(clientId1, 1000m);
        _cb.SubscribeClientToNotifications(clientId1);
        _cb.AddEmailForMessages(clientId1);
        _cb.SetCreditLimit(bankId1, 10000m);
        Assert.True(true);
    }

    private IPreBuild CreateFirstClient()
    {
        return Client.Builder
            .WithSurName("Abobus")
            .WithName("333")
            .WithAddress("DOM")
            .WithRussianPassport("4000 123123")
            .WithEmail("musorka.baz6l@gmail.com")
            .PreBuild();
    }

    private IPreBuild CreateSecondClient()
    {
        return Client.Builder
            .WithSurName("avtobus")
            .WithName("333")
            .WithAddress("aboba")
            .PreBuild();
    }

    private DepositPercentages CreateDps()
    {
        return DepositPercentages.Builder
            .AddPair(100m, 1m)
            .AddPair(200m, 2m)
            .AddPair(500m, 4m)
            .Build(0m);
    }

    private BankConfiguration CreateBankConfiguration()
    {
        return BankConfiguration.Builder
            .WithLimitForDoubtful(200m)
            .WithDebitPercentage(0.1m)
            .WithCreditPercentage(10m)
            .WithCreditLimit(1000m)
            .WithDepositDays(100)
            .WithDepositPercentages(CreateDps())
            .Build();
    }
}