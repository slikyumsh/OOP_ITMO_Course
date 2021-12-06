using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;
using Newtonsoft.Json;

namespace BackupsExtra
{
    [Serializable]
    public class BackupJob : BackupService
    {
        [JsonProperty("idCounter")]
        private static int _idCounter;
        [JsonProperty("dateOfBirth")]
        private DateTime _dateOfBirth;
        [JsonProperty("id")]
        private int _id;

        public BackupJob(IAlgorithm algorithm, IRepository repository)
            : base(algorithm, repository)
        {
            _dateOfBirth = DateTime.Now;
            _id = _idCounter++;
        }

        public int Id => _id;
        public DateTime DateOfBirth => _dateOfBirth;

        public void MergeRestorePoints(string oldRestorePointPath, string newRestorePointPath)
        {
            if (string.IsNullOrWhiteSpace(oldRestorePointPath))
                throw new ArgumentException("Invalid path");
            if (string.IsNullOrWhiteSpace(newRestorePointPath))
                throw new ArgumentException("Invalid path");
            if (!Directory.Exists(oldRestorePointPath))
                throw new ArgumentException("Invalid path to RestorePoint");
            if (!Directory.Exists(newRestorePointPath))
                throw new ArgumentException("Invalid path to RestorePoint!!!");
            if (Algorithm.ToString() == "SingleStorage")
            {
                Directory.Delete(oldRestorePointPath, true);
                Repository.RestorePoints.Remove(new RestorePoint(oldRestorePointPath));
            }
            else
            {
                string[] filesOld = Directory.GetFiles(oldRestorePointPath);
                string[] filesNew = Directory.GetFiles(newRestorePointPath);
                var fileInfoOld = new List<FileInfo>();
                var fileInfoNew = new List<FileInfo>();
                var filesOldList = new List<string>();
                var filesNewList = new List<string>();
                var filesToChange = new List<FileInfo>();
                var filesToDelete = new List<FileInfo>();
                foreach (var file in filesNew)
                {
                    var newFileInfo = new FileInfo(file);
                    fileInfoNew.Add(newFileInfo);
                    filesNewList.Add(newFileInfo.Name);
                }

                foreach (string file in filesOld)
                {
                    var newFileInfo = new FileInfo(file);
                    fileInfoOld.Add(newFileInfo);
                    filesOldList.Add(newFileInfo.Name);
                }

                foreach (string file in filesOldList)
                {
                    if (filesNewList.Contains(file))
                    {
                        FileInfo desiredFileInfo =
                            fileInfoOld.SingleOrDefault(desiredFileInfo => desiredFileInfo.Name == file);
                        if (desiredFileInfo is null)
                            throw new ArgumentException("Can't find info");
                        filesToDelete.Add(desiredFileInfo);
                    }
                    else
                    {
                        FileInfo desiredFileInfo =
                            fileInfoOld.SingleOrDefault(desiredFileInfo => desiredFileInfo.Name == file);
                        if (desiredFileInfo is null)
                            throw new ArgumentException("Can't find info");
                        filesToChange.Add(desiredFileInfo);
                    }
                }

                foreach (FileInfo file in filesToDelete)
                    File.Delete(file.FullName);
                if (Directory.GetFiles(oldRestorePointPath).Length == 0)
                {
                    Directory.Delete(oldRestorePointPath, true);
                    Repository.RestorePoints.Remove(new RestorePoint(oldRestorePointPath));
                }

                foreach (FileInfo file in filesToChange)
                {
                    Directory.Move(file.FullName, newRestorePointPath);
                }
            }
        }
    }
}