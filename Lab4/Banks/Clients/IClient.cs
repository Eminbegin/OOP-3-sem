using Banks.Accounts;
using Banks.Models;
using Banks.Observers.ChangingData;
using Banks.Observers.NotificationAboutChanges;

namespace Banks.Clients;

public interface IClient : IChangingDataObservable, INotificationObserver
{
    string Surname { get; }
    string Name { get; }
    int Id { get; }
    int PersonId { get; }
    string Email { get; }
    int PassportSeries { get; }
    int PassportNumber { get; }
    Address Address { get; }
    void AddAccount(IAccount account);
    void AddEmailForMessages();
    IVerificationStrategy IsReliable();
    void SetPassport(string passport);
    void SetAddress(string address);
    void SetEmail(string email);
}