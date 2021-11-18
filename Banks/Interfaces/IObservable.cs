namespace Banks
{
    public interface IObservable
    {
        void AddObserver(Client client);
        void RemoveObserver(Client client);
        void NotifyObservers(Message message);
    }
}