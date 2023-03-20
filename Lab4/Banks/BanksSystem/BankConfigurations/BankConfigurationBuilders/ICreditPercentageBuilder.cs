namespace Banks.BanksSystem.BankConfigurations.BankConfigurationBuilders;

public interface ICreditPercentageBuilder
{
    ICreditLimitBuilder WithCreditPercentage(decimal value);
}