using System;

namespace Backups
{
    public class JobObject
    {
        private readonly string _filePath;

        public JobObject(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Empty String");
            _filePath = path;
        }

        public string Path => _filePath;

        public override string ToString()
        {
            return DateTime.Now + _filePath;
        }
    }
}