using System;
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class Repository : IRepository
    {
        private static int _numberOfRestorePoints = 0;
        private readonly string _path;
        private List<RestorePoint> _restorePoints;

        public Repository(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Empty String");
            _path = path;
            _restorePoints = new List<RestorePoint>();
        }

        public Repository(string path, List<RestorePoint> points)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Empty String");
            if (points is null)
                throw new ArgumentException("Null list of points");
            _path = path;
            _restorePoints = points;
            _numberOfRestorePoints = points.Count;
        }

        public string Path => _path;
        public List<RestorePoint> RestorePoints => _restorePoints;
        public int NumberOfRestorePoints => _numberOfRestorePoints;

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

            _restorePoints.Add(restorePoint);
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