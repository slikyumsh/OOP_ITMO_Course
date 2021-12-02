using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class HybridClear : ICleaner
    {
        private List<BackupJob> _backupJobs;
        private bool _ifAllСonditions;
        private DateTime _time;
        private int _number;

        public HybridClear(DateTime time, List<BackupJob> backupJobs, int number, bool ifAllСonditions)
        {
            if (number <= 0)
                throw new ArgumentException("Number should be positive");
            _number = number;
            _ifAllСonditions = ifAllСonditions;
            _time = time;
            _backupJobs = backupJobs ?? throw new ArgumentException("Invalid argument");
        }

        public void ClearPoints()
        {
            if (_ifAllСonditions)
            {
                foreach (BackupJob backupJob in _backupJobs)
                {
                    if (backupJob.Repository.NumberOfRestorePoints > _number)
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
                }

                if (!_backupJobs.Any())
                    throw new ArgumentException("Empty _backupJobs");
            }
            else
            {
                ClearByDate byDate = new ClearByDate(_time, _backupJobs);
                byDate.ClearPoints();
                ClearByNumber byNumber = new ClearByNumber(_number, _backupJobs);
                byNumber.ClearPoints();
            }
        }
    }
}