using System;
using System.Collections.Generic;
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

        public void Clear()
        {
            foreach (BackupJob backupJob in _backupJobs)
            {
                if (backupJob.Repository.NumberOfRestorePoints >= _number)
                {
                    _backupJobs.Remove(backupJob);
                }
            }

            if (!_backupJobs.Any())
                throw new ArgumentException("Empty _backupJobs");
        }
    }
}