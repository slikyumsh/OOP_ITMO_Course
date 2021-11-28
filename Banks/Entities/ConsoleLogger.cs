using System;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class ConsoleLogger : ILogger
    {
        public void SendMessage(Message message)
        {
            if (message == null)
                throw new ArgumentException("Invalid argument");
            Console.WriteLine(message.Text);
        }
    }
}