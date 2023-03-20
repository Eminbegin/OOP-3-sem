namespace Banks.Exceptions;

public class ActionExceptions : Exception
{
    private ActionExceptions(string message)
    : base(message)
    {
    }

    public static ActionExceptions TransactionNotExists(int id)
        => new ActionExceptions($"Транзакция не найдена (id = {id}).");

    public static ActionExceptions ImpossibleWithdraw(int id)
        => new ActionExceptions($"Невозможно снять деньги с аккаунта (id = {id}).");

    public static ActionExceptions ImpossibleDeposit(int id)
        => new ActionExceptions($"Невозможно перевести деньги на аккаунт (id = {id}).");

    public static ActionExceptions ImpossiblePerform(int id)
        => new ActionExceptions($"Невозможно совершить уже выполненную транзакцию (id = {id}).");

    public static ActionExceptions ImpossibleCancel(int id)
        => new ActionExceptions($"Невозможно отменить уже отменнёную, либо невыполненную транзакцию (id = {id}).");
}