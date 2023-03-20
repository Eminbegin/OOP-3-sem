namespace Banks.BanksSystem.BankConfigurations.BankConfigurationBuilders;

public interface ICreditLimitBuilder
{
    IDepositDaysBuilder WithCreditLimit(decimal value);
}