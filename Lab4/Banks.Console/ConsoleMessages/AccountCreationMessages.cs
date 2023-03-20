namespace Banks.Console.ConsoleMessages;

public class AccountCreationMessages
{
    public const string ClientId = "Введите [green]id клиента[/]:";
    public const string Balance = "Введите [green]начальную сумму[/]:";

    public static string WithException()
    {
        return "[red]Аккаунт не создан.[/]";
    }

    public static string DebitCreated(int clientId, decimal balance)
    {
        return $"Создан новый дебетовый счёт для клиента с [green]id = {clientId}[/]. Начальный баланс: [green]{balance}[/]";
    }

    public static string CreditCreated(int clientId, decimal balance)
    {
        return $"Создан новый кредитный счёт для клиента с [green]id = {clientId}[/]. Начальный баланс: [green]{balance}[/]";
    }

    public static string DepositCreated(int clientId, decimal balance)
    {
        return $"Создан новый депозитный счёт для клиента с [green]id = {clientId}[/]. Начальный баланс: [green]{balance}[/]";
    }
}