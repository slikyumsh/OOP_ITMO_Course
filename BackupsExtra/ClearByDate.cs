using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupsExtra
{
    public class ClearByDate : ICleaner
    {
        private DateTime _time;
        private List<BackupJob> _backupJobs;

        public ClearByDate(DateTime time, List<BackupJob> backupJobs)
        {
            _time = time;
            _backupJobs = backupJobs ?? throw new ArgumentException("Invalid argument");
        }

        public void ClearPoints()
        {
            foreach (BackupJob backupJob in _backupJobs)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(backupJob.Repository.Path);
                DirectoryInfo[] subDirectoriesInfo = directoryInfo.GetDirectories();
                for (int i = 0; i < subDirectoriesInfo.Length; i++)
                {
                    DateTime creationTime = Directory.GetCreationTime(subDirectoriesInfo[i].FullName);
                    if (creationTime < _time)
                    {
                        Directory.Delete(subDirectoriesInfo[i].FullName, true);
                        backupJob.Repository.RestorePoints.Remove(backupJob.Repository.RestorePoints[i]);
                    }
                }
            }

            if (!_backupJobs.Any())
                throw new ArgumentException("Empty _backupJobs");
        }
    }
}