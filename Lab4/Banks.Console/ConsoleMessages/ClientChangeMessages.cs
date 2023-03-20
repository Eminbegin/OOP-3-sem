namespace Banks.Console.ConsoleMessages;

public static class ClientChangeMessages
{
    public const string Client = "Введите [green]id[/] клиента";
    public const string Passport = "Введите [green]паспорт[/]. [red]Пример ввода: 1234 567890[/]:";
    public const string Address = "Введите [green]адрес[/]:";
    public const string Email = "Введите [green]почту[/]:";

    public static string BadPassport()
    {
        return "[red]Пасспорт не добавлен.[/]";
    }

    public static string GoodPassport(int clientId)
    {
        return $"Пасспорт добавлен клиенту с [green]id = {clientId}[/].";
    }

    public static string BadAddress()
    {
        return "[red]Пасспорт не добавлен.[/]";
    }

    public static string GoodAddress(int clientId)
    {
        return $"Адрес добавлен клиенту с [green]id = {clientId}[/].";
    }

    public static string BadEmail()
    {
        return "[red]Пасспорт не добавлен.[/]";
    }

    public static string GoodEmail(int clientId)
    {
        return $"Почта добавлена клиенту с [green]id = {clientId}[/].";
    }

    public static string BadAddEmail()
    {
        return "[red]Клиенту не добавлена почта для уведомлений.[/]";
    }

    public static string GoodAddEmail(int clientId)
    {
        return $"Подключена почта для уведомлений у клиента с [green]id = {clientId}[/].";
    }

    public static string BadSubscribe()
    {
        return "[red]Не получилось подписать клиента на уведомления[/].";
    }

    public static string GoodSubscribe(int clientId)
    {
        return $"Теперь клиент с [green]id = {clientId}[/] будет получать уведомления на почту, если он её добавил.";
    }

    public static string BadDescribe()
    {
        return "[red]Не получилось отписать клиента от уведомлений.[/]";
    }

    public static string GoodDescribe(int clientId)
    {
        return $"Теперь клиент с [green]id = {clientId}[/] не будет получать уведомления на почту.";
    }
}