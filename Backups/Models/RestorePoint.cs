using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Backups
{
    [Serializable]
    public class RestorePoint
    {
        [JsonProperty("nameRestorePoint")]
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

        public override string ToString()
        {
            string toString = DateTime.Now + _name + " ";
            foreach (FileInfo file in _files)
            {
                toString += file.Name;
                toString += " ";
            }

            return toString;
        }
    }
}