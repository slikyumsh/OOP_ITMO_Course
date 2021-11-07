using System;
using System.IO;

namespace Backups
{
    public class Repository : IRepository
    {
        private static int _numberOfRestorePoints = 0;
        private readonly string _path;

        public Repository(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Empty String");
            _path = path;
        }

        public void SavePoint(RestorePoint restorePoint)
        {
            _numberOfRestorePoints++;
            int counterFiles = 0; // A counter for counting files, as well as for their correct naming
            Directory.CreateDirectory(_path + "/RestorePoint" + Convert.ToString(_numberOfRestorePoints));

            foreach (var file in restorePoint.Files)
            {
                counterFiles++;
                File.Move(Convert.ToString(file), GetNewFilePath(file, counterFiles));
            }
        }

        private string GetNewFilePath(FileInfo info, int numberOfFiles)
        {
            if (numberOfFiles <= 0)
                throw new ArgumentException("Invalid number of files");
            return _path + "/RestorePoint" + Convert.ToString(_numberOfRestorePoints) + "/" +
                   Convert.ToString(numberOfFiles);
        }
    }
}