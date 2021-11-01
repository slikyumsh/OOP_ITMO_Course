using System;

namespace Backups
{
    public class JobObject
    {
        private string _filePath;

        public JobObject(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Empty String");
            _filePath = path;
        }

        public string Path => _filePath;
    }
}