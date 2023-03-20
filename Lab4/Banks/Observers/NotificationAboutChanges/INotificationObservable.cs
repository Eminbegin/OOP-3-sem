namespace Banks.Observers.NotificationAboutChanges;

public interface INotificationObservable
{
    void AddNotificationObserver(INotificationObserver observer);
    void RemoveNotificationObserver(INotificationObserver observer);
    void NotifyObservers(string value);
}