namespace Banks.BanksSystem.BankConfigurations.BankConfigurationBuilders;

public interface IDebitPercentageBuilder
{
    ICreditPercentageBuilder WithDebitPercentage(decimal value);
}