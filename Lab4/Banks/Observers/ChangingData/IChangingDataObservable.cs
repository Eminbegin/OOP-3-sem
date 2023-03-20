namespace Banks.Observers.ChangingData;

public interface IChangingDataObservable
{
    void AddChangingDataObserver(IChangingDataObserver observer);
    void RemoveChangingDataObserver(IChangingDataObserver observer);
    void NotifyObservers();
}