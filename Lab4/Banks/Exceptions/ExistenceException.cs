namespace Banks.Exceptions;

public class ExistenceException : Exception
{
    private ExistenceException(string message)
        : base(message)
    {
    }

    public static ExistenceException BankNotExists(int id)
        => new ExistenceException($"Нет банка с id = {id}.");

    public static ExistenceException PersonNotExists(int id)
        => new ExistenceException($"Нет человека с id = {id}.");

    public static ExistenceException ClientNotExists(int id)
        => new ExistenceException($"Нет клиента с id = {id}.");

    public static ExistenceException AccountNotExists(int id)
        => new ExistenceException($"Нет аккаунта с id = {id}.");

    public static ExistenceException BadValue()
        => new ExistenceException($"Плохое значние.");
}