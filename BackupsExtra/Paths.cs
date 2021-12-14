using System;
using System.IO;

namespace BackupsExtra
{
    public class Paths
    {
        private string _workPath;
        private string _pathToConfig;

        public Paths()
        {
            _workPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/WorkFiles";
            _pathToConfig = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra";
        }

        public Paths(string workPath, string pathToConfig)
        {
            if (string.IsNullOrWhiteSpace(workPath))
                throw new ArgumentException("Empty workPath");
            if (string.IsNullOrWhiteSpace(pathToConfig))
                throw new ArgumentException("Empty pathToConfig");
            _workPath = workPath;
            _pathToConfig = pathToConfig;
        }

        public string WorkPath => _workPath;
        public string ConfigPath => _pathToConfig;
    }
}