using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;
using Json.Net;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BackupsExtra
{
    [Serializable]
    public class BackupExtraService
    {
        private string _path;
        private List<BackupJob> _backupJobs;
        private string _pathToConfig;

        public BackupExtraService()
        {
            _backupJobs = new List<BackupJob>();

            _pathToConfig = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "\\BackupsExtra";
        }

        public BackupJob CreateBackupJob(IAlgorithm algorithm, IRepository repository, ILogger logger)
        {
            if (algorithm is null)
                throw new ArgumentException("Null algorithm exception");
            if (repository is null)
                throw new ArgumentException("Null repository");
            if (logger is null)
                throw new ArgumentException("Null logger");
            var backupJob = new BackupJob(algorithm, repository, new ConsoleLogger());
            _backupJobs.Add(backupJob);
            return backupJob;
        }

        public void AddJobObject(JobObject jobObject, BackupJob backupJob)
        {
            if (jobObject is null)
                throw new ArgumentException("Null jobObject");
            if (backupJob is null)
                throw new ArgumentException("Null backupObject");
            BackupJob desiredBackup = _backupJobs.SingleOrDefault(desiredBackup => desiredBackup.Id == backupJob.Id);
            if (desiredBackup is null)
                throw new ArgumentException("This BackupJob is not contained by list<BackupJob>");
            backupJob.AddJobObject(jobObject);
        }

        public RestorePoint CreateRestorePoint(BackupJob backupJob)
        {
            if (backupJob is null)
                throw new ArgumentException("BackupJob is null");
            BackupJob desiredBackup = _backupJobs.SingleOrDefault(desiredBackup => desiredBackup.Id == backupJob.Id);
            if (desiredBackup is null)
                throw new ArgumentException("This BackupJob is not contained by list<BackupJob>");
            RestorePoint restorePoint = backupJob.MakePointWithNotice();
            return restorePoint;
        }

        public JobObject CreateJobObject(string path, BackupJob backupJob)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Null or Empty string");
            BackupJob desiredBackupJob =
                _backupJobs.SingleOrDefault(desiredBackupJob => desiredBackupJob.Id == backupJob.Id);
            if (desiredBackupJob is null)
                throw new ArgumentException("BackupJob is null");
            var jobObject = new JobObject(path);
            desiredBackupJob.AddJobObjectWithNotice(jobObject);
            return jobObject;
        }

        public void Download()
        {
            List<Configuration> data =
                JsonConvert.DeserializeObject<List<Configuration>>(File.ReadAllText(_pathToConfig + "\\Config.json"));
            foreach (Configuration configuration in data)
            {
                DownloadOneBackupJob(configuration);
            }
        }

        public void Save()
        {
            var configurations = new List<Configuration>();
            foreach (BackupJob backup in _backupJobs)
            {
                configurations.Add(SaveBackupJob(backup));
            }

            string json = JsonConvert.SerializeObject(configurations, Formatting.Indented);
            File.WriteAllText(_pathToConfig + "\\Config.json", json);
        }

        public int TestNumberOfDownloadedBackupJobs()
        {
            return _backupJobs.Count;
        }

        private Configuration SaveBackupJob(BackupJob backupJob)
        {
            if (backupJob is null)
                throw new ArgumentException("BackupJob is null");
            BackupJob desiredBackup = _backupJobs.SingleOrDefault(desiredBackup => desiredBackup.Id == backupJob.Id);
            if (desiredBackup is null)
                throw new ArgumentException("This BackupJob is not contained by list<BackupJob>");
            var data = new Configuration();
            data.Algorithm = backupJob.Algorithm.ToString();
            data.RepositoryPath = backupJob.Repository.Path;
            data.WorkFilesPath = _path;
            return data;
        }

        private List<RestorePoint> GetListRestorePointByPath()
        {
            var result = new List<RestorePoint>();
            var dir = new DirectoryInfo(_path);
            DirectoryInfo[] lst = dir.GetDirectories();
            foreach (var dirInfo in lst)
            {
                var restorePoint = new RestorePoint(dirInfo.Name);
                result.Add(restorePoint);
            }

            return result;
        }

        private void DownloadOneBackupJob(Configuration data)
        {
            if (data is null)
                throw new ArgumentException("Null data");
            _path = data.RepositoryPath;
            string algorithm = data.Algorithm;
            switch (algorithm)
            {
                case "SingleStorage":
                    var singleStorageAlgo = new SingleStorageAlgo();
                    CreateBackupJob(singleStorageAlgo, new Repository(_path, GetListRestorePointByPath()), new ConsoleLogger());
                    break;
                case "SplitStorages":
                    var splitStoragesAlgo = new SplitStoragesAlgo();
                    CreateBackupJob(splitStoragesAlgo, new Repository(_path, GetListRestorePointByPath()), new ConsoleLogger());
                    break;
                default: throw new ArgumentException("Invalid algorithm " + algorithm);
            }
        }
    }
}