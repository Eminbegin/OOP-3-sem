namespace Banks.Console.ConsoleMessages;

public static class ClientCreationMessages
{
    public const string BankId = "Введите [green]id банка[/]:";
    public const string PersonId = "Введите [green]id человека[/]:";
    public const string Surname = "Введите [green]фамилию[/]:";
    public const string Name = "Введите [green]имя[/]:";
    public const string Address = "Добавить [green]адрес[/]?";
    public const string SetAddress = "Введите [green]адрес[/]:";
    public const string Passport = "Добавить [green]паспорт[/]?";
    public const string SetPassport = "Введите [green]паспорт[/]. [red]Пример ввода: 1234 567890[/]:";
    public const string Email = "Добавить [green]почту[/]?";
    public const string SetEmail = "Введите [green]почту[/]:";

    public static string GetMessage(string name, string surname, int id)
    {
        return $"Создан клиент [red]{name} {surname}[/] с [red]id {id}[/]";
    }

    public static string WithException()
    {
        return "[red]Клиент не создан[/]";
    }
}