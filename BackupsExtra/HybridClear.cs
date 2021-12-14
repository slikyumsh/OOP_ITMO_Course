using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupsExtra
{
    public class HybridClear : ICleaner
    {
        private BackupJob _backupJob;
        private bool _ifAllСonditions;
        private DateTime _time;
        private int _number;

        public HybridClear(DateTime time, BackupJob backupJob, int number, bool ifAllСonditions)
        {
            if (number <= 0)
                throw new ArgumentException("Number should be positive");
            _number = number;
            _ifAllСonditions = ifAllСonditions;
            _time = time;
            _backupJob = backupJob ?? throw new ArgumentException("Invalid argument");
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
            var markedPoints = new Dictionary<DirectoryInfo, int>();
            if (_ifAllСonditions)
            {
               var byDate = new ClearByDate(_time, _backupJob);
               Dictionary<DirectoryInfo, int> markedPoints1 = byDate.MarkPoints();
               var byNumber = new ClearByNumber(_number, _backupJob);
               Dictionary<DirectoryInfo, int> markedPoints2 = byNumber.MarkPoints();
               foreach (KeyValuePair<DirectoryInfo, int> pair in markedPoints1)
               {
                   if (markedPoints2.ContainsKey(pair.Key))
                       markedPoints.Add(pair.Key, pair.Value);
               }
            }
            else
            {
                var byDate = new ClearByDate(_time, _backupJob);
                Dictionary<DirectoryInfo, int> markedPoints1 = byDate.MarkPoints();
                var byNumber = new ClearByNumber(_number, _backupJob);
                Dictionary<DirectoryInfo, int> markedPoints2 = byNumber.MarkPoints();
                foreach (KeyValuePair<DirectoryInfo, int> pair in markedPoints1)
                {
                    markedPoints.Add(pair.Key, pair.Value);
                }

                foreach (KeyValuePair<DirectoryInfo, int> pair in markedPoints2)
                {
                    if (!markedPoints.ContainsKey(pair.Key))
                        markedPoints.Add(pair.Key, pair.Value);
                }
            }

            return markedPoints;
        }
    }
}