using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Backups;

namespace BackupsExtra
{
    public class Configuration
    {
        public string Algorithm { get; set; }
        public string RepositoryPath { get; set; }
        public string WorkFilesPath { get; set; }
    }
}