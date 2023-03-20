namespace Banks.Console.Commands;

public static class StartCommandsRus
{
    public static readonly List<string> StartCommands = new List<string>
    {
        Create,
        Do,
        ChangeClient,
        ChangeBank,
        Time,
        Info,
        Exit,
    };

    private const string Create = "Создание";
    private const string Do = "Переводы, Снятие, Внесение";
    private const string ChangeClient = "Действие с клиентом";
    private const string ChangeBank = "Имзенить банк";
    private const string Time = "Имзенить время";
    private const string Info = "Информация";
    private const string Exit = "Выход";
}