using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups
{
    public class SingleStorageAlgo : IAlgorithm
    {
        private readonly string _rootPath;

        public SingleStorageAlgo()
        {
            _rootPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName +
                        "/BackupWorkFiles";
        }

        public RestorePoint MakePoint(List<JobObject> list)
        {
            if (list.Count == 0)
                throw new ArgumentException("List is empty");
            string zipFile = _rootPath + "/ZipFile";
            RestorePoint restorePoint = new RestorePoint(_rootPath);
            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                foreach (var file in list)
                {
                    archive.CreateEntryFromFile(file.Path, Path.GetFileName(file.Path));
                }

                restorePoint.AddFile(new FileInfo(zipFile));
            }

            return restorePoint;
        }

        public override string ToString()
        {
            return "SingleStorage";
        }
    }
}