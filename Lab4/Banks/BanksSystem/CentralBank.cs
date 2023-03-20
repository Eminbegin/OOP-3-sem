using System.Security;
using Banks.Accounts;
using Banks.BanksSystem.BankConfigurations;
using Banks.Clients;
using Banks.Clients.ClientBuilder;
using Banks.Exceptions;
using Banks.Observers.SkipDays;
using Banks.Transactions;
using Banks.Utilities;

namespace Banks.BanksSystem;

public class CentralBank : ICentralBank
{
    private static ICentralBank? _instance;
    private readonly ITimeManager _timeManager;
    private readonly List<ISkippingTimeObserver> _observers;
    private readonly List<Bank> _banks;
    private readonly HashSet<Person> _persons;
    private readonly IdGenerator _personIdGenerator;
    private readonly IdGenerator _clientIdGenerator;
    private readonly List<ITransaction> _transactions;
    private readonly IdGenerator _transactionsIdGenerator;
    private readonly IdGenerator _banksIdGenerator;
    private readonly IdGenerator _accountsIdGenerator;

    public CentralBank()
    {
        _personIdGenerator = new IdGenerator();
        _observers = new List<ISkippingTimeObserver>();
        _accountsIdGenerator = new IdGenerator();
        _banksIdGenerator = new IdGenerator();
        _persons = new HashSet<Person>();
        _banks = new List<Bank>();
        _timeManager = new SimpleTimeManager(new DateTime(2000, 1, 1));
        _transactions = new List<ITransaction>();
        _transactionsIdGenerator = new IdGenerator();
        _clientIdGenerator = new IdGenerator();
    }

    public static ICentralBank GetInstance()
    {
        if (_instance is null)
        {
            _instance = new CentralBank();
        }

        return _instance;
    }

    public IReadOnlyCollection<Bank> Banks() => _banks.AsReadOnly();

    public int AddPerson(string name, string surname)
    {
        var person = new Person(surname, name, _personIdGenerator.Next());
        if (!_persons.Add(person))
        {
            throw new Exception();
        }

        return person.Id;
    }

    public int AddClient(int bankId, int personId, IPreBuild client)
    {
        if (_persons.All(p => p.Id != personId))
        {
            throw ExistenceException.PersonNotExists(personId);
        }

        return GetBankById(bankId).AddClient(client, personId);
    }

    public int AddBank(string name, BankConfiguration bankConfiguration)
    {
        var bank = new Bank(name, _banksIdGenerator.Next(), _accountsIdGenerator, bankConfiguration, _clientIdGenerator);
        _banks.Add(bank);
        return bank.Id;
    }

    public void SubscribeClientToNotifications(int clientId)
    {
        Bank bank = GetBankByClientId(clientId);
        bank.AddNotificationObserver(bank.GetClientById(clientId));
    }

    public void DescribeClientToNotifications(int clientId)
    {
        Bank bank = GetBankByClientId(clientId);
        bank.RemoveNotificationObserver(bank.GetClientById(clientId));
    }

    public void AddEmailForMessages(int clientId)
    {
        GetBankByClientId(clientId).GetClientById(clientId).AddEmailForMessages();
    }

    public int AddDebitAccount(int clientId, decimal balance)
    {
        Bank bank = GetBankByClientId(clientId);
        IAccount account = bank.AddDebitAccount(clientId, balance);
        AddSkippingDayObserver(account);
        return account.Id;
    }

    public int AddCreditAccount(int clientId, decimal balance)
    {
        Bank bank = GetBankByClientId(clientId);
        IAccount account = bank.AddCreditAccount(clientId, balance);
        AddSkippingDayObserver(account);
        return account.Id;
    }

    public int AddDepositAccount(int clientId, decimal balance)
    {
        Bank bank = GetBankByClientId(clientId);
        IAccount account = bank.AddDepositAccount(clientId, balance);
        AddSkippingDayObserver(account);
        return account.Id;
    }

    public int AddMoney(int accountId, decimal money)
    {
        ITransaction transaction = new DepositTransaction(
            money,
            _timeManager.Time,
            _transactionsIdGenerator.Next(),
            GetAccountById(accountId));

        _transactions.Add(transaction);
        transaction.Perform();
        return transaction.Id;
    }

    public int TakeOffMoney(int accountId, decimal money) // slowly
    {
        ITransaction transaction = new WithdrawTransaction(
            money,
            _timeManager.Time,
            _transactionsIdGenerator.Next(),
            GetAccountById(accountId));

        _transactions.Add(transaction);
        transaction.Perform();
        return transaction.Id;
    }

    public void SkipNDays(int days)
    {
        if (days <= 0)
        {
            throw ExistenceException.BadValue();
        }

        for (int i = 0; i < days; i++)
        {
            _timeManager.NextDay();
            SkippingDay();
            if (_timeManager.IsTheFirstDayOfMonth())
            {
                MonthPassed();
            }
        }
    }

    public int MakeTransfer(int accountFrom, int accountTo, decimal money)
    {
        IAccount accountFirst = GetAccountById(accountFrom);
        IAccount accountSecond = GetAccountById(accountTo);

        ITransaction transaction = new TransferTransaction(
            money,
            _timeManager.Time,
            _transactionsIdGenerator.Next(),
            accountFirst,
            accountSecond);

        _transactions.Add(transaction);
        transaction.Perform();
        return transaction.Id;
    }

    public void RemoveTransaction(int transactionId)
    {
        ITransaction transaction = GetTransactionById(transactionId);
        transaction.Cancel();
    }

    public void SetPassport(int clientId, string passport)
    {
        Bank bank = GetBankByClientId(clientId);
        bank.GetClientById(clientId).SetPassport(passport);
    }

    public void SetAddress(int clientId, string address)
    {
        Bank bank = GetBankByClientId(clientId);
        bank.GetClientById(clientId).SetAddress(address);
    }

    public void SetEmail(int clientId, string email)
    {
        Bank bank = GetBankByClientId(clientId);
        bank.GetClientById(clientId).SetEmail(email);
    }

    public bool SetDebitPercentage(int bankId, decimal value)
    {
        Bank bank = GetBankById(bankId);
        return bank.SetDebitPercentage(value);
    }

    public bool SetCreditPercentage(int bankId, decimal value)
    {
        Bank bank = GetBankById(bankId);
        return bank.SetCreditPercentage(value);
    }

    public bool SetCreditLimit(int bankId, decimal value)
    {
        Bank bank = GetBankById(bankId);
        return bank.SetCreditLimit(value);
    }

    public bool SetLimitForDoubtful(int bankId, decimal value)
    {
        Bank bank = GetBankById(bankId);
        return bank.SetLimitForDoubtful(value);
    }

    public bool SetDepositDays(int bankId, int value)
    {
        Bank bank = GetBankById(bankId);
        return bank.SetDepositDays(value);
    }

    public bool SetDepositPercentages(int bankId, DepositPercentages value)
    {
        Bank bank = GetBankById(bankId);
        return bank.SetDepositPercentages(value);
    }

    public void IsAccountExists(int accountId)
    {
        GetBankByAccountId(accountId);
    }

    public void IsClientExists(int clientId)
    {
        GetBankByClientId(clientId);
    }

    public void AddSkippingDayObserver(ISkippingTimeObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveSkippingDayObserver(ISkippingTimeObserver observer)
    {
        _observers.Remove(observer);
    }

    public void SkippingDay()
    {
        foreach (ISkippingTimeObserver observer in _observers)
        {
            observer.UpdateAmount();
        }
    }

    public void MonthPassed()
    {
        foreach (ISkippingTimeObserver observer in _observers)
        {
            observer.UpdateBalance();
        }
    }

    public Bank GetBankById(int bankId)
    {
        Bank? bank = _banks.FirstOrDefault(b => b.Id == bankId);
        if (bank is null)
        {
            throw ExistenceException.BankNotExists(bankId);
        }

        return bank;
    }

    private IAccount GetAccountById(int accountId)
    {
        return GetBankByAccountId(accountId).GetAccountById(accountId);
    }

    private Bank GetBankByAccountId(int accountId)
    {
        Bank? bank = _banks.FirstOrDefault(b => b.Accounts().Any(a => a.Id == accountId));
        if (bank is null)
        {
            throw ExistenceException.AccountNotExists(accountId);
        }

        return bank;
    }

    private Bank GetBankByClientId(int clientId)
    {
        Bank? bank = _banks.FirstOrDefault(b => b.Clients().Any(a => a.Id == clientId));
        if (bank is null)
        {
            throw ExistenceException.ClientNotExists(clientId);
        }

        return bank;
    }

    private ITransaction GetTransactionById(int transactionId)
    {
        ITransaction? transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transaction is null)
        {
            throw ActionExceptions.TransactionNotExists(transactionId);
        }

        return transaction;
    }
}