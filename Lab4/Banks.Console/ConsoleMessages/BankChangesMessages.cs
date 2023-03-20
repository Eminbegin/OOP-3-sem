namespace Banks.Console.ConsoleMessages;

public static class BankChangesMessages
{
    public const string Bank = "Ведите [green]id[/] банка:";
    public const string DepositPairs = "Ведите [green]количество[/] значения дебетовых процентов:";
    public const string Value = "Ведите значение [green]деняк[/]:";
    public const string Percentage = "Ведите [green]процент[/]:";

    public const string LimitForDoubtful = "Введите [green]лимит для сомнительных[/] пользователей:";
    public const string CreditLimit = "Введите [green]кредитный лимит[/]:";
    public const string DebitPercentage = "Введите [green]процент на остаток[/] для дебетового счёта:";
    public const string CreditPercentage = "Ввдеите [green]процент по кредиту[/]:";
    public const string DepositDays = "Введите [green]депозитный период[/]:";

    public static string BadLimitForDoubtful(int bankId)
    {
        return $"Лимит для сомнительных клиентов не обновлен в с [green]id = {bankId}[/].";
    }

    public static string GoodLimitForDoubtful(int bankId)
    {
        return $"Лимит для сомнительных клиентов обновлен в банке с [green]id = {bankId}[/].\nКлиенты, подписавашиеся на уведомления получили информацию.";
    }

    public static string BadCreditLimit(int bankId)
    {
        return $"Кредитный лимит не обновлен в с [green]id = {bankId}[/].";
    }

    public static string GoodCreditLimit(int bankId)
    {
        return $"Кредитный лимит обновлен в банке с [green]id = {bankId}[/].\nКлиенты, подписавашиеся на уведомления получили информацию.";
    }

    public static string BadDebitPercentage(int bankId)
    {
        return $"Процент на остаток не обновлен в с [green]id = {bankId}[/].";
    }

    public static string GoodDebitPercentage(int bankId)
    {
        return $"Процент на остаток обновлен в банке с [green]id = {bankId}[/].\nКлиенты, подписавашиеся на уведомления получили информацию.";
    }

    public static string BadCreditPercentage(int bankId)
    {
        return $"Процент по кредиту не обновлен в с [green]id = {bankId}[/].";
    }

    public static string GoodCreditPercentage(int bankId)
    {
        return $"Процент по кредиту обновлен в банке с [green]id = {bankId}[/].\nКлиенты, подписавашиеся на уведомления получили информацию.";
    }

    public static string BadDepositDays(int bankId)
    {
        return $"Депозитный период не обновлен в с [green]id = {bankId}[/].";
    }

    public static string GoodDepositDays(int bankId)
    {
        return $"Депозитный период обновлен в банке с [green]id = {bankId}[/].\nКлиенты, подписавашиеся на уведомления получили информацию.";
    }

    public static string BadDepositPairs(int bankId)
    {
        return $"Значения депозитных процентов не обновлен в с [green]id = {bankId}[/].";
    }

    public static string GoodDepositPairs(int bankId)
    {
        return $"Значения депозитных процентов обновлен в банке с [green]id = {bankId}[/].\nКлиенты, подписавашиеся на уведомления получили информацию.";
    }
}