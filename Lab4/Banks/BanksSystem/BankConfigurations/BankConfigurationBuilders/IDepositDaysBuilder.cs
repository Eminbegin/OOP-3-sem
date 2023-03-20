namespace Banks.BanksSystem.BankConfigurations.BankConfigurationBuilders;

public interface IDepositDaysBuilder
{
    IDepositPercentagesBuilder WithDepositDays(int value);
}