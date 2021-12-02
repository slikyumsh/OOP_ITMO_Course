using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackupsExtra
{
    public class ClearBeforeDate : ICleaner
    {
        private DateTime _time;
        private List<BackupJob> _backupJobs;

        public ClearBeforeDate(DateTime time, List<BackupJob> backupJobs)
        {
            _time = time;
            _backupJobs = backupJobs ?? throw new ArgumentException("Invalid argument");
        }

        public void ClearPoints()
        {
            foreach (BackupJob backupJob in _backupJobs)
            {
                for (int i = 0; i < backupJob.Repository.NumberOfRestorePoints; i++)
                {
                    DateTime creationTime =
                        Directory.GetCreationTime(
                            backupJob.Repository.Path + "\\RestorePoint" + Convert.ToString(i + 1));
                    if (creationTime < _time)
                    {
                        Directory.Delete(backupJob.Repository.Path + "\\RestorePoint" + Convert.ToString(i + 1), true);
                        backupJob.Repository.RestorePoints.Remove(backupJob.Repository.RestorePoints[i]);
                    }
                }
            }

            if (!_backupJobs.Any())
                throw new ArgumentException("Empty _backupJobs");
        }
    }
}