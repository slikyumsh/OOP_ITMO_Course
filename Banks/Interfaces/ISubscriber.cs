using Banks.Entities;

namespace Banks.Interfaces
{
    public interface ISubscriber
    {
        int Id { get; }
        void GetUpdates(Message message);
    }
}