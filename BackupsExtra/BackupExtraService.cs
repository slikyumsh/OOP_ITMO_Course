using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups;
using Newtonsoft.Json;

namespace BackupsExtra
{
    [Serializable]
    public class BackupExtraService
    {
        private string _path;
        private int _limitForRestorePointsAtOneBackupJob;
        private List<BackupJob> _backupJobs;
        private string _pathToConfig;
        private ILogger _logger;

        public BackupExtraService(ILogger logger, int numberPoints, Paths paths)
        {
            if (logger is null)
                throw new ArgumentException("Null logger");

            // this minimum limit, because it is an adequate restriction,
            // because it must be positive, and deleting and merging with a single point is impossible
            if (numberPoints <= 2)
                throw new ArgumentException("Limit of restore point per one BackupJob must be positive and bigger than 2");
            _limitForRestorePointsAtOneBackupJob = numberPoints;
            _backupJobs = new List<BackupJob>();
            _pathToConfig = paths.ConfigPath;
            _logger = logger;
            _path = paths.WorkPath;
        }

        public List<BackupJob> BackupJobs => _backupJobs;
        public int NumberOfDownloadedBackupJobs => _backupJobs.Count;
        public BackupJob CreateBackupJob(IAlgorithm algorithm, IRepository repository)
        {
            if (algorithm is null)
                throw new ArgumentException("Null algorithm exception");
            if (repository is null)
                throw new ArgumentException("Null repository");
            var backupJob = new BackupJob(algorithm, repository);
            _backupJobs.Add(backupJob);
            _logger.SendMessage("Created BackupJob");
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
            _logger.SendMessage("Added JobObject");
            backupJob.AddJobObject(jobObject);
        }

        public RestorePoint CreateRestorePoint(BackupJob backupJob)
        {
            if (backupJob is null)
                throw new ArgumentException("BackupJob is null");
            BackupJob desiredBackup = _backupJobs.SingleOrDefault(desiredBackup => desiredBackup.Id == backupJob.Id);
            if (desiredBackup is null)
                throw new ArgumentException("This BackupJob is not contained by list<BackupJob>");
            RestorePoint restorePoint = backupJob.MakePoint();
            if (backupJob.Repository.RestorePoints.Count == _limitForRestorePointsAtOneBackupJob)
            {
                string[] points = Directory.GetFiles(backupJob.Repository.Path);

                // the first two elements are always there, since this minimum limit is set by the constructor
                MergeRestorePoints(points[0], points[1]);
            }

            _logger.SendMessage("Created RestorePoint");
            return restorePoint;
        }

        public JobObject CreateJobObject(string path, BackupJob backupJob)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Empty path");
            BackupJob desiredBackupJob =
                _backupJobs.SingleOrDefault(desiredBackupJob => desiredBackupJob.Id == backupJob.Id);
            if (desiredBackupJob is null)
                throw new ArgumentException("BackupJob is null");
            var jobObject = new JobObject(path);
            desiredBackupJob.AddJobObject(jobObject);
            _logger.SendMessage("Created JobObject");
            return jobObject;
        }

        public void Restore()
        {
            List<Configuration> data =
                JsonConvert.DeserializeObject<List<Configuration>>(File.ReadAllText(_pathToConfig + "/Config.json"));
            foreach (Configuration configuration in data)
            {
                DownloadOneBackupJob(configuration);
            }

            _logger.SendMessage("Restored from Json");
        }

        public void Save()
        {
            var configurations = new List<Configuration>();
            foreach (BackupJob backup in _backupJobs)
            {
                configurations.Add(SaveBackupJob(backup));
            }

            string json = JsonConvert.SerializeObject(configurations, Formatting.Indented);
            File.WriteAllText(_pathToConfig + "/Config.json", json);
            _logger.SendMessage("Saved to Json");
        }

        public void RestoreFilesFromPoint(string pathFromRestore, string pathToRestore)
        {
            if (string.IsNullOrEmpty(pathToRestore))
                throw new ArgumentException("Empty path");
            if (string.IsNullOrEmpty(pathFromRestore))
                throw new ArgumentException("Empty path");
            if (!Directory.Exists(pathFromRestore))
                throw new ArgumentException("Invalid path to RestorePoint");
            if (!Directory.Exists(pathToRestore))
                throw new ArgumentException("Invalid path to RestorePoint");
            var backupDirectoryInfo = new DirectoryInfo(pathFromRestore);
            FileInfo[] filesWithBackup = backupDirectoryInfo.GetFiles();
            foreach (FileInfo fileInfo in filesWithBackup)
            {
                ZipFile.ExtractToDirectory(pathFromRestore + fileInfo.Name, pathToRestore, true);
            }

            foreach (FileInfo fileInfo in filesWithBackup)
            {
                fileInfo.Delete();
            }

            Directory.Delete(pathFromRestore);
            _logger.SendMessage("Restored Files From RestorePoint");
        }

        public void Clear(ICleaner cleaner)
        {
            if (cleaner is null)
                throw new ArgumentException("Null cleaner");
            cleaner.ClearPoints();
            _logger.SendMessage("Cleaned!");
        }

        public void MergeRestorePoints(string oldRestorePointPath, string newRestorePointPath)
        {
            if (string.IsNullOrWhiteSpace(oldRestorePointPath))
                throw new ArgumentException("Empty path");
            if (string.IsNullOrWhiteSpace(newRestorePointPath))
                throw new ArgumentException("Empty path");
            if (!Directory.Exists(oldRestorePointPath))
                throw new ArgumentException("Invalid path to RestorePoint");
            if (!Directory.Exists(newRestorePointPath))
                throw new ArgumentException("Invalid path to RestorePoint");
            BackupJob backupJob1 = FindBackupJobForRestorePoint(oldRestorePointPath);
            BackupJob backupJob2 = FindBackupJobForRestorePoint(newRestorePointPath);
            if (backupJob1 is null)
                throw new ArgumentException("Can't find backup that contains this restore point");
            if (backupJob2 is null)
                throw new ArgumentException("Can't find backup that contains this restore point1");
            if (backupJob1.Id != backupJob2.Id)
                throw new ArgumentException("These points lay in different directories");
            backupJob1.MergeRestorePoints(oldRestorePointPath, newRestorePointPath);
            _logger.SendMessage("RestorePoints merged");
        }

        private BackupJob FindBackupJobForRestorePoint(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Empty path");
            foreach (BackupJob backup in _backupJobs)
            {
                var directoryInfo = new DirectoryInfo(backup.Repository.Path);
                string[] directories = Directory.GetDirectories(directoryInfo.FullName);
                foreach (string directory in directories)
                {
                    if (directory == path)
                        return backup;
                }
            }

            return null;
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
            foreach (DirectoryInfo dirInfo in lst)
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
                    CreateBackupJob(singleStorageAlgo, new Repository(_path, GetListRestorePointByPath()));
                    break;
                case "SplitStorages":
                    var splitStoragesAlgo = new SplitStoragesAlgo();
                    CreateBackupJob(splitStoragesAlgo, new Repository(_path, GetListRestorePointByPath()));
                    break;
                default: throw new ArgumentException("Invalid algorithm " + algorithm);
            }
        }
    }
}