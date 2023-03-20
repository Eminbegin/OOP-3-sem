namespace Banks.Console.ConsoleMessages;

public static class ActionMessages
{
    public const string Deposit = "Введите [green]id[/] счёта, на который будут зачилены денеги:";
    public const string Withdraw = "Введите [green]id[/] счёта с которого будут сняты деньги:";
    public const string Cancel = "Введите [green]id[/] транзакции, которую необходимо отменить:";
    public const string Balance = "Введите [green]сумму[/] для перевода";

    public static string UnSuccessful()
    {
        return "[red]Транзакция не совершена.[/]";
    }

    public static string Successful(int id)
    {
        return "Транзакция совершена, её [red]id = {id}[/]";
    }

    public static string NotCanceled()
    {
        return "[red]Транзакция не отменена.[/]";
    }

    public static string Canceled(int id)
    {
        return $"Транзакция отменена, её [red]id = {id}[/]";
    }
}