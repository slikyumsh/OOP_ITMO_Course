using System;
using System.Collections.Generic;
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

        public void Clear()
        {
            foreach (BackupJob backupJob in _backupJobs)
            {
                if (backupJob.DateOfBirth < _time)
                {
                    _backupJobs.Remove(backupJob);
                }
            }

            if (!_backupJobs.Any())
                throw new ArgumentException("Empty _backupJobs");
        }
    }
}