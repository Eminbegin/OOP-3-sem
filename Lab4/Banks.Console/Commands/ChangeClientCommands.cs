namespace Banks.Console.Commands;

public static class ChangeClientCommands
{
    public static readonly List<string> Commands = new List<string>
    {
        Passport,
        Address,
        Email,
        AddEmail,
        SubscribeForMsg,
        DescribeForMsg,
    };

    private const string Passport = "Добавить пасспорт";
    private const string Address = "Добавить адрес";
    private const string Email = "Добавить почту";
    private const string AddEmail = "Разрешить уведомления на почту";
    private const string SubscribeForMsg = "Подписаться на уведомления";
    private const string DescribeForMsg = "Отписаться от уведомлений";
}