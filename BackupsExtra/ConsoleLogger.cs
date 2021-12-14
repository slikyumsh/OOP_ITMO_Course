using System;

namespace BackupsExtra
{
    public class ConsoleLogger : ILogger
    {
        public void SendMessage(object message)
        {
            if (message is null)
                throw new ArgumentException("Invalid argument");
            Console.WriteLine(message);
        }
    }
}