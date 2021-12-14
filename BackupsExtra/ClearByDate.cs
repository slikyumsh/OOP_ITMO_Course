using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupsExtra
{
    public class ClearByDate : ICleaner
    {
        private DateTime _time;
        private BackupJob _backupJob;

        public ClearByDate(DateTime time, BackupJob backupJob)
        {
            _time = time;
            _backupJob = backupJob ?? throw new ArgumentException("Null backupJob");
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

        public Dictionary<DirectoryInfo, int> MarkPoints()
        {
            var directoryInfo = new DirectoryInfo(_backupJob.Repository.Path);
            DirectoryInfo[] subDirectoriesInfo = directoryInfo.GetDirectories();
            var markedPoints = new Dictionary<DirectoryInfo, int>();
            for (int i = 0; i < subDirectoriesInfo.Length; i++)
            {
                DateTime creationTime = Directory.GetCreationTime(subDirectoriesInfo[i].FullName);
                if (creationTime < _time)
                {
                    markedPoints.Add(subDirectoriesInfo[i], i);
                }
            }

            return markedPoints;
        }
    }
}