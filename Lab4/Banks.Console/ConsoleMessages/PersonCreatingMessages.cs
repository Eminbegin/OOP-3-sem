namespace Banks.Console.ConsoleMessages;

public static class PersonCreatingMessages
{
    public const string Surname = "Введите [green]фамилию[/]:";
    public const string Name = "Введите [green]имя[/]:";
    public static string GetMessage(string name, string surname, int id)
    {
        return $"Создан человек  [red]{name} {surname}[/] с [red]id {id}[/]";
    }
}