namespace Banks.Exceptions;

public class ClientBuilderExceptions : Exception
{
    private ClientBuilderExceptions(string message)
        : base(message)
    {
    }

    public static ClientBuilderExceptions IncorrectName()
        => new ClientBuilderExceptions($"Неверное имя");

    public static ClientBuilderExceptions IncorrectSurname()
        => new ClientBuilderExceptions($"Неверная фамилия");
    public static ClientBuilderExceptions PassportLength(int value)
        => new ClientBuilderExceptions($"Длина пасспотра должна быть {value}");

    public static ClientBuilderExceptions PassportNumber()
        => new ClientBuilderExceptions($"Неверный номер пассорта");

    public static ClientBuilderExceptions PassportSeries()
        => new ClientBuilderExceptions($"Неверная серия пассорта");

    public static ClientBuilderExceptions IncorrectAddress()
        => new ClientBuilderExceptions($"Неверный адрес");

    public static ClientBuilderExceptions IncorrectEmail()
        => new ClientBuilderExceptions($"Неверная почта");

    public static ClientBuilderExceptions EmailNotExists()
        => new ClientBuilderExceptions("У клиента нет почты");
}