namespace Banks.Console.Commands;

public static class CreationCommands
{
    public static readonly List<string> Commands = new List<string>()
    {
        CreatePerson,
        CreateBank,
        CreateClient,
        CreateAccount,
    };

    private const string CreatePerson = "Создать человека";
    private const string CreateBank = "Создать банк";
    private const string CreateClient = "Создать клиента";
    private const string CreateAccount = "Создать аккаунт";

    public static string ClientCreated(string surname, string name, int id)
    {
        return $"Создан клиент [red] {surname} {name} [/] с id [red] {id} [/]";
    }

    public static string BankCreated(string name, int id)
    {
        return $"Создан банк [red] {name} [/] с id [red] {id} [/]";
    }
}