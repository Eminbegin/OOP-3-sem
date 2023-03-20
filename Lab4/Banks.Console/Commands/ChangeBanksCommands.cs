namespace Banks.Console.Commands;

public static class ChangeBanksCommands
{
    public static readonly List<string> Commands = new List<string>
    {
        LimitForDoubtful,
        CreditLimit,
        DebitPercentage,
        CreditPercentage,
        DepositDays,
        DepositPairs,
    };

    private const string LimitForDoubtful = "Лимит для сомнительных";
    private const string CreditLimit = "Кредитный лимит";
    private const string DebitPercentage = "Процент на остаток";
    private const string CreditPercentage = "Процент по кредиту";
    private const string DepositDays = "Депозитный период";
    private const string DepositPairs = "Значения депозитных процентов";
}