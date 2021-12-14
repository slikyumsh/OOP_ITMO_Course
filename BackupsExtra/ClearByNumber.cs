using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupsExtra
{
    public class ClearByNumber : ICleaner
    {
        private int _number;
        private BackupJob _backupJob;

        public ClearByNumber(int number, BackupJob backupJob)
        {
            if (number <= 0)
                throw new ArgumentException("Non positive number");
            if (backupJob is null)
                throw new ArgumentException("Null BackupJob");
            _number = number;
            _backupJob = backupJob;
        }

        public Dictionary<DirectoryInfo, int> MarkPoints()
        {
            var markedPoints = new Dictionary<DirectoryInfo, int>();
            if (_backupJob.Repository.RestorePoints.Count > _number)
            {
                var directoryInfo = new DirectoryInfo(_backupJob.Repository.Path);
                DirectoryInfo[] subDirectoriesInfo = directoryInfo.GetDirectories();
                for (int i = 0; i < subDirectoriesInfo.Length - _number; i++)
                {
                    markedPoints.Add(subDirectoriesInfo[i], i);
                }
            }

            return markedPoints;
        }

        public void ClearPoints()
        {
            Dictionary<DirectoryInfo, int> pointsForDelete = MarkPoints();
            var indexes = new List<int>();
            foreach (KeyValuePair<DirectoryInfo, int> pair in pointsForDelete)
            {
                indexes.Add(pair.Value);
            }

            indexes.Sort();
            indexes.Reverse();
            foreach (KeyValuePair<DirectoryInfo, int> pair in pointsForDelete)
            {
                if (Directory.Exists(pair.Key.FullName))
                    Directory.Delete(pair.Key.FullName, true);
            }

            foreach (int index in indexes)
            {
                if (index < _backupJob.Repository.RestorePoints.Count)
                    _backupJob.Repository.RestorePoints.Remove(_backupJob.Repository.RestorePoints[index]);
            }

            if (!_backupJob.Repository.RestorePoints.Any())
                throw new ArgumentException("Backup became empty after cleaning");
        }
    }
}