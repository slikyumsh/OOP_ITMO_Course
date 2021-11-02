using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Backups
{
    public class SplitStoragesAlgo : IAlgorithm
    {
        private static int _counterDirectories = 1;
        public RestorePoint MakePoint(List<JobObject> list)
        {
            if (!list.Any())
                throw new ArgumentException("List is empty");
            RestorePoint restorePoint = new RestorePoint(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles");
            foreach (var file in list)
            {
                string zipFile = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles/ZipFile" + Convert.ToString(_counterDirectories);
                JobObject jobObject = new JobObject(zipFile);
                _counterDirectories++;
                using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(file.Path, Path.GetFileName(file.Path));
                }

                restorePoint.AddFile(new FileInfo(zipFile));
            }

            return restorePoint;
        }
    }
}