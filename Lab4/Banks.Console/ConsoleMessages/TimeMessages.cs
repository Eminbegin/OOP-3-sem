namespace Banks.Console.ConsoleMessages;

public static class TimeMessages
{
    public const string Days = "Введите количество дней, которое хотите пропустить:";
    public const string BadDays = "Можно пропустить только положительное число дней";

    public static string GoodDays(int value)
    {
        return $"Успешно пропуено {value} дней\\дня\\день\\днёв.";
    }
}