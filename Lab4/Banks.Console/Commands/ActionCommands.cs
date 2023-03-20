namespace Banks.Console.Commands;

public static class ActionCommands
{
    public static readonly List<string> Commands = new List<string>
    {
        Deposit,
        Withdraw,
        Transfer,
        Cancel,
    };

    private const string Withdraw = "Снять деньги со счёта";
    private const string Deposit = "Внести деньги на счёта";
    private const string Transfer = "Перевести деньги со счёта на другой счёт";
    private const string Cancel = "Отменить транзакцию";
}