using Banks.Entities;

namespace Banks.Interfaces
{
    public interface ILogger
    {
        void SendMessage(Message message);
    }
}