namespace Banks.Console.Commands;

public static class AccountsCreationCommands
{
    public static readonly List<string> Commands = new List<string>
    {
        Debit,
        Credit,
        Deposit,
    };

    private const string Debit = "Дебетовый счёт";
    private const string Credit = "Кредитный счёт";
    private const string Deposit = "Депозитный счёт";
}