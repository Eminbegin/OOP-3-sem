using Banks.Accounts;
using Banks.BanksSystem.BankConfigurations;
using Banks.Clients;
using Banks.Clients.ClientBuilder;
using Banks.Exceptions;
using Banks.Observers.NotificationAboutChanges;
using Banks.Utilities;

namespace Banks.BanksSystem;

public class Bank : IEquatable<Bank>, INotificationObservable
{
    private readonly List<IClient> _clients;
    private readonly IdGenerator _clientsIdGenerator;
    private readonly List<IAccount> _accounts;
    private readonly List<INotificationObserver> _observers;
    private readonly IdGenerator _accountIdGenerator;

    public Bank(string name, int id, IdGenerator accountIdeGenerator, BankConfiguration bankConfiguration, IdGenerator clientsIdGenerator)
    {
        Id = id;
        Name = name;
        BankConfiguration = bankConfiguration;
        _clientsIdGenerator = clientsIdGenerator;
        _clients = new List<IClient>();
        _accounts = new List<IAccount>();
        _accountIdGenerator = accountIdeGenerator;
        _observers = new List<INotificationObserver>();
    }

    public BankConfiguration BankConfiguration { get; }
    public string Name { get; }
    public int Id { get; }

    public IReadOnlyCollection<IAccount> Accounts() => _accounts;
    public IReadOnlyCollection<IClient> Clients() => _clients;
    public IVerificationStrategy GetVerificationStrategy(IClient client)
    {
        if (string.IsNullOrEmpty(client.Address.FullAddress) || client.PassportNumber == 0 || client.PassportSeries == 0)
        {
            return new NotVerificationClient();
        }

        return new VerificationClient();
    }

    public int AddClient(IPreBuild client, int personId)
    {
        if (_clients.Any(c => c.PersonId == personId))
        {
            throw new Exception();
        }

        IClient newClient = client.Build(personId, _clientsIdGenerator.Next());
        _clients.Add(newClient);
        return newClient.Id;
    }

    public IAccount AddDebitAccount(int clientId, decimal balance)
    {
        IClient client = GetClientById(clientId);
        IAccount account = new DebitAccount(
            _accountIdGenerator.Next(),
            balance,
            BankConfiguration.DebitPercentage,
            BankConfiguration.LimitForDoubtful,
            GetVerificationStrategy(client));

        _accounts.Add(account);
        client.AddAccount(account);
        client.AddChangingDataObserver(account);
        return account;
    }

    public IAccount AddCreditAccount(int clientId, decimal balance)
    {
        IClient client = GetClientById(clientId);
        IAccount account = new CreditAccount(
            _accountIdGenerator.Next(),
            balance,
            BankConfiguration.CreditPercentage,
            BankConfiguration.LimitForDoubtful,
            BankConfiguration.CreditLimit,
            GetVerificationStrategy(client));

        _accounts.Add(account);
        client.AddAccount(account);
        client.AddChangingDataObserver(account);
        return account;
    }

    public IAccount AddDepositAccount(int clientId, decimal balance)
    {
        IClient client = GetClientById(clientId);
        IAccount account = new DepositAccount(
            _accountIdGenerator.Next(),
            balance,
            BankConfiguration.LimitForDoubtful,
            BankConfiguration.DepositPercentages,
            BankConfiguration.DepositDays,
            GetVerificationStrategy(client));

        _accounts.Add(account);
        client.AddAccount(account);
        client.AddChangingDataObserver(account);
        return account;
    }

    public IClient GetClientById(int clientId)
    {
        IClient? client = _clients.FirstOrDefault(c => c.Id == clientId);
        if (client is null)
        {
            throw ExistenceException.ClientNotExists(clientId);
        }

        return client;
    }

    public bool Equals(Bank? other)
    {
        return other is not null && Name == other.Name && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Bank);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Id);
    }

    public IAccount GetAccountById(int accountId)
    {
        IAccount? account = _accounts.FirstOrDefault(a => a.Id == accountId);
        if (account is null)
        {
            throw new Exception();
        }

        return account;
    }

    public bool SetDebitPercentage(decimal value)
    {
        if (value == BankConfiguration.DebitPercentage) return false;
        BankConfiguration.SetDebitPercentage(value);
        NotifyHelper("DebitPercentage");
        return true;
    }

    public bool SetCreditPercentage(decimal value)
    {
        if (value == BankConfiguration.CreditPercentage) return false;
        BankConfiguration.SetCreditPercentage(value);
        NotifyHelper("CreditPercentage");
        return true;
    }

    public bool SetCreditLimit(decimal value)
    {
        if (value == BankConfiguration.CreditLimit) return false;
        BankConfiguration.SetCreditLimit(value);
        NotifyHelper("CreditLimit");
        return true;
    }

    public bool SetLimitForDoubtful(decimal value)
    {
        if (value == BankConfiguration.LimitForDoubtful) return false;
        BankConfiguration.SetLimitForDoubtful(value);
        NotifyHelper("LimitForDoubtful");
        return true;
    }

    public bool SetDepositDays(int value)
    {
        if (value == BankConfiguration.DepositDays) return false;
        BankConfiguration.SetDepositDays(value);
        NotifyHelper("DebitPercentage");
        return true;
    }

    public bool SetDepositPercentages(DepositPercentages value)
    {
        if (value == BankConfiguration.DepositPercentages) return false;
        BankConfiguration.SetDepositPercentages(value);
        NotifyHelper("DepositPercentages");
        return true;
    }

    public void AddNotificationObserver(INotificationObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveNotificationObserver(INotificationObserver observer)
    {
        if (!_observers.Remove(observer))
        {
            throw new Exception();
        }
    }

    public void NotifyObservers(string value)
    {
        foreach (INotificationObserver observer in _observers)
        {
            observer.UpdateNotification(value);
        }
    }

    private void NotifyHelper(string value)
    {
        NotifyObservers($"{value} was changed in BANK with id = {Id}");
    }
}
