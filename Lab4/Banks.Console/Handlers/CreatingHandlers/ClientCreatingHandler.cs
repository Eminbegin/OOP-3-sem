using Banks.BanksSystem;
using Banks.Clients;
using Banks.Clients.ClientBuilder;
using Banks.Console.ConsoleMessages;
using Banks.Exceptions;
using Spectre.Console;

namespace Banks.Console.Handlers.CreatingHandlers;

public class ClientCreatingHandler : CreatingHandler
{
    public override void HandleRequest(int condition)
    {
        if (condition == 2)
        {
            int id = 0;
            ICentralBank cb = CentralBank.GetInstance();
            int bankId = AnsiConsole.Ask<int>(ClientCreationMessages.BankId);
            int personId = AnsiConsole.Ask<int>(ClientCreationMessages.PersonId);
            string surname = AnsiConsole.Ask<string>(ClientCreationMessages.Surname);
            string name = AnsiConsole.Ask<string>(ClientCreationMessages.Name);
            ISubjectBuilder subjectBuilder =
                Client.Builder
                    .WithSurName(surname)
                    .WithName(name);

            AnsiConsole.Clear();
            subjectBuilder = SetAddress(subjectBuilder);
            subjectBuilder = SetPassport(subjectBuilder);
            subjectBuilder = SetEmail(subjectBuilder);
            try
            {
                id = cb.AddClient(bankId, personId, subjectBuilder.PreBuild());
            }
            catch (ExistenceException exception)
            {
                HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, $"{ClientCreationMessages.WithException()} {exception.Message}");
                return;
            }

            HandlerDependencies.GetInstance().StartHandlerFirst.HandleRequest(-1, ClientCreationMessages.GetMessage(name, surname, id));
        }
        else
        {
            Successor?.HandleRequest(condition);
        }
    }

    private static ISubjectBuilder SetAddress(ISubjectBuilder builder)
    {
        if (!AnsiConsole.Confirm(ClientCreationMessages.Address)) return builder;
        builder.WithAddress(AnsiConsole.Ask<string>(ClientCreationMessages.SetAddress));

        return builder;
    }

    private static ISubjectBuilder SetPassport(ISubjectBuilder builder)
    {
        if (!AnsiConsole.Confirm(ClientCreationMessages.Passport)) return builder;
        try
        {
            builder.WithRussianPassport(AnsiConsole.Ask<string>(ClientCreationMessages.SetPassport));
        }
        catch (Exception exception)
        {
            AnsiConsole.WriteLine(exception.Message);
        }

        return builder;
    }

    private static ISubjectBuilder SetEmail(ISubjectBuilder builder)
    {
        if (!AnsiConsole.Confirm(ClientCreationMessages.Email)) return builder;
        builder.WithEmail(AnsiConsole.Ask<string>(ClientCreationMessages.SetEmail));

        return builder;
    }
}