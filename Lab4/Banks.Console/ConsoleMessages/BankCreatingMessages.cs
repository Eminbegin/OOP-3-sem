namespace Banks.Console.ConsoleMessages;

public static class BankCreatingMessages
{
    public const string Name = "Введите [green]имя[/] банка:";

    public const string DepositPairs = "Ведите [green]количество[/] значения дебетовых процентов:";
    public const string Value = "Ведите значение [green]деняк[/]:";
    public const string Percentage = "Ведите [green]процент[/]:";

    public const string LimitForDoubtful = "Введите [green]лимит для сомнительных[/] пользователей:";
    public const string DebitPercentage = "Введите [green]процент на остаток[/] для дебетового счёта:";
    public const string CreditPercentage = "Ввдеите [green]процент по кредиту[/]:";
    public const string CreditLimit = "Введите [green]кредитный лимит[/]:";
    public const string DepositDays = "Введите [green]депозитный период[/]:";

    public static string GetMessage(string name, int id)
    {
        return $"Создан банк [red]{name}[/] с [red]id {id}[/].";
    }

    public static string NotCreated()
    {
        return $"Банк не создан.";
    }
}