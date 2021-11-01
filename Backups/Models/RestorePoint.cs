using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backups
{
    public class RestorePoint
    {
        private string _name;
        private List<string> _files;

        public RestorePoint(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Empty string");
            _name = name;
            _files = new List<string>();
        }

        public List<string> Jobs => _files;

        public void AddFile(string file)
        {
            if (string.IsNullOrEmpty(file))
                throw new ArgumentException("Empty string");
            string desiredString = _files.SingleOrDefault(desiredString => desiredString == file);
            if (desiredString != null)
                throw new ArgumentException("We have already added this file");
            _files.Add(file);
        }
    }
}