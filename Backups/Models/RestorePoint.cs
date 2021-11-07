using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backups
{
    public class RestorePoint
    {
        private string _name;
        private List<FileInfo> _files;

        public RestorePoint(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Empty string");
            _name = name;
            _files = new List<FileInfo>();
        }

        public List<FileInfo> Files => _files;

        public void AddFile(FileInfo file)
        {
            FileInfo desiredString = _files.SingleOrDefault(desiredString => desiredString == file);
            if (desiredString != null)
                throw new ArgumentException("We have already added this file");
            _files.Add(file);
        }
    }
}