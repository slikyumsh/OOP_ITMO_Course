using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupsExtra
{
    public class ClearByNumber : ICleaner
    {
        private int _number;
        private List<BackupJob> _backupJobs;

        public ClearByNumber(int number, List<BackupJob> backupJobs)
        {
            if (number <= 0)
                throw new ArgumentException("Non positive number");
            if (backupJobs is null)
                throw new ArgumentException("Null list<BackupJob>");
            _number = number;
            _backupJobs = backupJobs;
        }

        public void ClearPoints()
        {
            foreach (BackupJob backupJob in _backupJobs)
            {
                if (backupJob.Repository.NumberOfRestorePoints > _number)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(backupJob.Repository.Path);
                    DirectoryInfo[] subDirectoriesInfo = directoryInfo.GetDirectories();
                    for (int i = 0; i < subDirectoriesInfo.Length - _number; i++)
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