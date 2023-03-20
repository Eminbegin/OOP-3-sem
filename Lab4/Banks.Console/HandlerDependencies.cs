using Banks.Accounts;
using Banks.Console.Handlers;
using Banks.Console.Handlers.ActionHandlers;
using Banks.Console.Handlers.ChangeBankHandlers;
using Banks.Console.Handlers.ChangeClientHandlers;
using Banks.Console.Handlers.CreatingHandlers;
using Banks.Console.Handlers.CreatingHandlers.DifferentAccountsHandler;
using Banks.Console.Handlers.StartHandlers;

namespace Banks.Console;

public class HandlerDependencies
{
    private static HandlerDependencies? _instance;
    private readonly SuperStartStartHandler _superStartStartHandler = new SuperStartStartHandler();
    private readonly CreateSmthStartHandler _createSmthStartHandler = new CreateSmthStartHandler();
    private readonly DoSmthStartHandler _doSmthStartHandler = new DoSmthStartHandler();
    private readonly ChangeClientStartHandler _changeClientStartHandler = new ChangeClientStartHandler();
    private readonly ChangeBankStartHandler _changeBankStartHandler = new ChangeBankStartHandler();
    private readonly TimeStartHandler _timeStartHandler = new TimeStartHandler();
    private readonly InfoAboutStartHandler _infoAboutStartHandler = new InfoAboutStartHandler();
    private readonly ExitStartHandler _exitStartHandler = new ExitStartHandler();

    private readonly PersonCreatingHandler _personCreatingHandler = new PersonCreatingHandler();
    private readonly BankCreatingHandler _bankCreatingHandler = new BankCreatingHandler();
    private readonly ClientCreatingHandler _clientCreatingHandler = new ClientCreatingHandler();
    private readonly AccountCreatingHandler _accountCreatingHandler = new AccountCreatingHandler();

    private readonly DebitAccountHandler _debitAccountHandler = new DebitAccountHandler();
    private readonly DepositAccountHandler _depositAccountHandler = new DepositAccountHandler();
    private readonly CreditAccountHandler _creditAccountHandler = new CreditAccountHandler();

    private readonly DepositActionHandler _depositActionHandler = new DepositActionHandler();
    private readonly WithdrawActionHandler _withdrawActionHandler = new WithdrawActionHandler();
    private readonly TransferActionHandler _transferActionHandler = new TransferActionHandler();
    private readonly CanceledActionHandler _canceledActionHandler = new CanceledActionHandler();

    private readonly PassportClientHandler _passportClientHandler = new PassportClientHandler();
    private readonly AddressClientHandler _addressClientHandler = new AddressClientHandler();
    private readonly EmailClientHandler _emailClientHandler = new EmailClientHandler();
    private readonly AddEmailClientHandler _addEmailClientHandler = new AddEmailClientHandler();
    private readonly SubscribeClientHandler _subscribeClientHandler = new SubscribeClientHandler();
    private readonly DescribeClientHandler _describeClientHandler = new DescribeClientHandler();

    private readonly LimitForDoubtfulHandler _limitForDoubtfulHandler = new LimitForDoubtfulHandler();
    private readonly CreditLimitHandler _creditLimitHandler = new CreditLimitHandler();
    private readonly DebitPercentageHandler _debitPercentageHandler = new DebitPercentageHandler();
    private readonly CreditPercentageHandler _creditPercentageHandler = new CreditPercentageHandler();
    private readonly DepositDaysHandler _depositDaysHandler = new DepositDaysHandler();
    private readonly DepositPairsHandler _depositPairsHandler = new DepositPairsHandler();

    public HandlerDependencies()
    {
        SetStartSuccessor();
        SetCreatingSuccessor();
        SetAccountCreating();
        SetActions();
        SetClientChanges();
        SetBankChanges();
    }

    public StartHandler StartHandlerFirst => _superStartStartHandler;
    public CreatingHandler CreatingHandlerFirst => _personCreatingHandler;
    public AccountsHandler AccountCreatingFirst => _debitAccountHandler;
    public ActionHandler ActionHandlerFirst => _depositActionHandler;
    public PassportClientHandler ClientHandlerFirst => _passportClientHandler;
    public LimitForDoubtfulHandler BankHandlerFirst => _limitForDoubtfulHandler;

    public static HandlerDependencies GetInstance()
    {
        return _instance ??= new HandlerDependencies();
    }

    private void SetStartSuccessor()
    {
        _superStartStartHandler.Successor = _createSmthStartHandler;
        _createSmthStartHandler.Successor = _doSmthStartHandler;
        _doSmthStartHandler.Successor = _changeClientStartHandler;
        _changeClientStartHandler.Successor = _changeBankStartHandler;
        _changeBankStartHandler.Successor = _timeStartHandler;
        _timeStartHandler.Successor = _infoAboutStartHandler;
        _infoAboutStartHandler.Successor = _exitStartHandler;
        _exitStartHandler.Successor = _superStartStartHandler;
    }

    private void SetCreatingSuccessor()
    {
        _personCreatingHandler.Successor = _bankCreatingHandler;
        _bankCreatingHandler.Successor = _clientCreatingHandler;
        _clientCreatingHandler.Successor = _accountCreatingHandler;
        _accountCreatingHandler.Successor = _personCreatingHandler;
    }

    private void SetAccountCreating()
    {
        _debitAccountHandler.Successor = _creditAccountHandler;
        _creditAccountHandler.Successor = _depositAccountHandler;
        _depositAccountHandler.Successor = _debitAccountHandler;
    }

    private void SetActions()
    {
        _depositActionHandler.Successor = _withdrawActionHandler;
        _withdrawActionHandler.Successor = _transferActionHandler;
        _transferActionHandler.Successor = _canceledActionHandler;
        _canceledActionHandler.Successor = _depositActionHandler;
    }

    private void SetClientChanges()
    {
        _passportClientHandler.Successor = _addressClientHandler;
        _addressClientHandler.Successor = _emailClientHandler;
        _emailClientHandler.Successor = _addEmailClientHandler;
        _addEmailClientHandler.Successor = _subscribeClientHandler;
        _subscribeClientHandler.Successor = _describeClientHandler;
        _describeClientHandler.Successor = _passportClientHandler;
    }

    private void SetBankChanges()
    {
        _limitForDoubtfulHandler.Successor = _creditLimitHandler;
        _creditLimitHandler.Successor = _debitPercentageHandler;
        _debitPercentageHandler.Successor = _creditPercentageHandler;
        _creditPercentageHandler.Successor = _depositDaysHandler;
        _depositDaysHandler.Successor = _depositPairsHandler;
        _depositPairsHandler.Successor = _limitForDoubtfulHandler;
    }
}