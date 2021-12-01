using System;
using System.IO;

namespace BackupsExtra
{
    public class FileLogger : ILogger
    {
        private string _path;

        public FileLogger(string path)
        {
            if (path is null)
                throw new ArgumentException("Invalid path");
            _path = path;
        }

        public void SendMessage(object message)
        {
            if (message is null)
                throw new ArgumentException("Null message");
            File.WriteAllText(_path, message.ToString());
        }
    }
}