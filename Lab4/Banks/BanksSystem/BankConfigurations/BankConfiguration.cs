using Banks.BanksSystem.BankConfigurations.BankConfigurationBuilders;
using Banks.Exceptions;

namespace Banks.BanksSystem.BankConfigurations;

public class BankConfiguration
{
    private BankConfiguration(decimal limitForDoubtful, decimal debitPercentage, decimal creditPercentage, decimal creditLimit, int depositDays, DepositPercentages depositPercentages)
    {
        DebitPercentage = debitPercentage;
        CreditPercentage = creditPercentage;
        CreditLimit = creditLimit;
        LimitForDoubtful = limitForDoubtful;
        DepositDays = depositDays;
        DepositPercentages = depositPercentages;
    }

    public static ILimitForDoubtfulBuilder Builder => new BankConfigurationBuilder();
    public decimal DebitPercentage { get; private set; }
    public decimal CreditPercentage { get; private set; }
    public decimal CreditLimit { get; private set; }
    public decimal LimitForDoubtful { get; private set; }
    public int DepositDays { get; private set; }
    public DepositPercentages DepositPercentages { get; private set; }

    public void SetDebitPercentage(decimal value)
    {
        BankConfigurationBuilder.IsNotNegative(value);
        DebitPercentage = value;
    }

    public void SetCreditPercentage(decimal value)
    {
        BankConfigurationBuilder.IsNotNegative(value);
        CreditPercentage = value;
    }

    public void SetCreditLimit(decimal value)
    {
        BankConfigurationBuilder.IsPositive(value);
        CreditLimit = value;
    }

    public void SetLimitForDoubtful(decimal value)
    {
        BankConfigurationBuilder.IsNotNegative(value);
        LimitForDoubtful = value;
    }

    public void SetDepositDays(int value)
    {
        BankConfigurationBuilder.IsPositive(value);
        DepositDays = value;
    }

    public void SetDepositPercentages(DepositPercentages value)
    {
        if (value.Equals(DepositPercentages.Empty))
        {
            throw new Exception();
        }

        DepositPercentages = value;
    }

    private class BankConfigurationBuilder :
        ILimitForDoubtfulBuilder,
        IDebitPercentageBuilder,
        ICreditPercentageBuilder,
        ICreditLimitBuilder,
        IDepositDaysBuilder,
        IDepositPercentagesBuilder,
        ISubjectBuilder
    {
        private decimal _debitPercentage;
        private decimal _creditPercentage;
        private decimal _creditLimit;
        private decimal _limitForDoubtful;
        private int _depositDays;
        private DepositPercentages _depositPercentages;

        public BankConfigurationBuilder()
        {
            _debitPercentage = 0;
            _creditPercentage = 0;
            _creditLimit = 0;
            _limitForDoubtful = 0;
            _depositDays = 0;
            _depositPercentages = DepositPercentages.Empty;
        }

        public static void IsNotNegative(decimal value)
        {
            if (value < 0)
            {
                throw ExistenceException.BadValue();
            }
        }

        public static void IsPositive(decimal value)
        {
            if (value <= 0)
            {
                throw ExistenceException.BadValue();
            }
        }

        public IDebitPercentageBuilder WithLimitForDoubtful(decimal value)
        {
            IsNotNegative(value);
            _limitForDoubtful = value;
            return this;
        }

        public ICreditPercentageBuilder WithDebitPercentage(decimal value)
        {
            IsNotNegative(value);
            _debitPercentage = value;
            return this;
        }

        public ICreditLimitBuilder WithCreditPercentage(decimal value)
        {
            IsNotNegative(value);
            _creditPercentage = value;
            return this;
        }

        public IDepositDaysBuilder WithCreditLimit(decimal value)
        {
            IsPositive(value);
            _creditLimit = value;
            return this;
        }

        public IDepositPercentagesBuilder WithDepositDays(int value)
        {
            IsPositive(value);
            _depositDays = value;
            return this;
        }

        public ISubjectBuilder WithDepositPercentages(DepositPercentages value)
        {
            if (value.Equals(DepositPercentages.Empty))
            {
                throw ExistenceException.BadValue();
            }

            _depositPercentages = value;
            return this;
        }

        public BankConfiguration Build()
        {
            return new BankConfiguration(
                _limitForDoubtful,
                _debitPercentage,
                _creditPercentage,
                _creditLimit,
                _depositDays,
                _depositPercentages);
        }
    }
}