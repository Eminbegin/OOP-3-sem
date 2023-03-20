namespace Banks.BanksSystem.BankConfigurations.BankConfigurationBuilders;

public interface ILimitForDoubtfulBuilder
{
    IDebitPercentageBuilder WithLimitForDoubtful(decimal value);
}