using Banks.Accounts;
using Banks.Clients.ClientBuilder;
using Banks.Exceptions;
using Banks.Models;
using Banks.Observers.ChangingData;

namespace Banks.Clients;

public class Client : IClient, IEquatable<Client>
{
    private readonly List<IChangingDataObserver> _observers;
    private readonly List<IMessageSender> _senders;
    private readonly List<IAccount> _accounts;
    private Client(string surname, string name, int passportSeries, int passportNumber, Address address, string email, int personId, int idFromBnak)
    {
        Surname = surname;
        Name = name;
        PassportSeries = passportSeries;
        PassportNumber = passportNumber;
        Address = address;
        Email = email;
        _senders = new List<IMessageSender>();
        _accounts = new List<IAccount>();
        _observers = new List<IChangingDataObserver>();
        Id = idFromBnak;
        PersonId = personId;
    }

    public static ISurnameBuilder Builder => new ClientBuilder();

    public int Id { get; }
    public int PersonId { get; }
    public string Email { get; private set; }
    public string Surname { get; }
    public string Name { get; }
    public int PassportSeries { get; private set; }
    public int PassportNumber { get; private set; }
    public Address Address { get; private set; }

    public void AddAccount(IAccount account)
    {
        _accounts.Add(account);
    }

    public void AddEmailForMessages()
    {
        if (string.IsNullOrEmpty(Email))
        {
            throw ClientBuilderExceptions.EmailNotExists();
        }

        _senders.Add(new EmailMessage(Email));
    }

    public IVerificationStrategy IsReliable()
    {
        if (string.IsNullOrEmpty(Address.FullAddress) || PassportNumber == 0 || PassportSeries == 0)
        {
            return new NotVerificationClient();
        }

        return new VerificationClient();
    }

    public void SetPassport(string passport)
    {
        ClientBuilder.PassportLengthValidation(passport);
        PassportSeries = ClientBuilder.SeriesValidation(passport);
        PassportNumber = ClientBuilder.NumberValidation(passport);
        NotifyObservers();
    }

    public void SetAddress(string address)
    {
        ClientBuilder.AddressValidation(address);
        Address = new Address(address);
        NotifyObservers();
    }

    public void SetEmail(string email)
    {
        ClientBuilder.EmailValidation(email);
        Email = email;
    }

    public bool Equals(Client? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _observers.Equals(other._observers) && _senders.Equals(other._senders) && _accounts.Equals(other._accounts) && Id == other.Id && Email == other.Email && Surname == other.Surname && Name == other.Name && PassportSeries == other.PassportSeries && PassportNumber == other.PassportNumber && Address.Equals(other.Address);
    }

    public void AddChangingDataObserver(IChangingDataObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveChangingDataObserver(IChangingDataObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IChangingDataObserver observer in _observers)
        {
            observer.UpdateVerification(IsReliable());
        }
    }

    public void UpdateNotification(string value)
    {
        foreach (IMessageSender messageSender in _senders)
        {
            messageSender.SendMessage(value);
        }
    }

    private class ClientBuilder : ISurnameBuilder, INameBuilder, ISubjectBuilder, IPreBuild
    {
        private Address _address;
        private int _passportNumber;
        private int _passportSeries;
        private string _email;
        private string _name;
        private string _surname;

        public ClientBuilder()
        {
            _address = new Address(string.Empty);
            _email = string.Empty;
            _name = string.Empty;
            _passportNumber = 0;
            _passportSeries = 0;
            _surname = string.Empty;
        }

        public static void AddressValidation(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw ClientBuilderExceptions.IncorrectAddress();
            }
        }

        public static int SeriesValidation(string passport)
        {
            string series = passport.Substring(BuilderConstants.SeriesStartIndex, BuilderConstants.SeriesLength);
            if (!int.TryParse(series, out int newSeries) || newSeries is not(>= BuilderConstants.MinSeriesValue and <= BuilderConstants.MaxSeriesValue))
            {
                throw ClientBuilderExceptions.PassportSeries();
            }

            return newSeries;
        }

        public static int NumberValidation(string passport)
        {
            string number = passport.Substring(BuilderConstants.NumberStartIndex, BuilderConstants.NumberLength);
            if (!int.TryParse(number, out int newNumber) || newNumber is not(>= BuilderConstants.MinNumberValue and <= BuilderConstants.MaxNumberValue))
            {
                throw ClientBuilderExceptions.PassportNumber();
            }

            return newNumber;
        }

        public static void PassportLengthValidation(string passport)
        {
            if (passport.Length != BuilderConstants.PassportLength)
            {
                throw ClientBuilderExceptions.PassportLength(BuilderConstants.PassportLength);
            }
        }

        public static void EmailValidation(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw ClientBuilderExceptions.IncorrectEmail();
            }
        }

        public INameBuilder WithSurName(string surname)
        {
            SurnameValidation(surname);
            _surname = surname;

            return this;
        }

        public ISubjectBuilder WithName(string name)
        {
            NameValidation(name);
            _name = name;

            return this;
        }

        public ISubjectBuilder WithAddress(string address)
        {
            AddressValidation(address);
            _address = new Address(address);

            return this;
        }

        public ISubjectBuilder WithRussianPassport(string passport)
        {
            PassportLengthValidation(passport);
            _passportSeries = SeriesValidation(passport);
            _passportNumber = NumberValidation(passport);

            return this;
        }

        public ISubjectBuilder WithEmail(string email)
        {
            EmailValidation(email);
            _email = email;

            return this;
        }

        public IPreBuild PreBuild()
        {
            return this;
        }

        public IClient Build(int personId, int idFromBank)
        {
            return new Client(_surname, _name, _passportSeries, _passportNumber, _address, _email, personId, idFromBank);
        }

        private void NameValidation(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw ClientBuilderExceptions.IncorrectName();
            }
        }

        private void SurnameValidation(string surname)
        {
            if (string.IsNullOrEmpty(surname))
            {
                throw ClientBuilderExceptions.IncorrectSurname();
            }
        }
    }
}