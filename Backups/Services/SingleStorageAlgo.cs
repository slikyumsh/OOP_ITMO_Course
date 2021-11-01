using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups
{
    public class SingleStorageAlgo : IAlgorithm
    {
        public RestorePoint MakePoint(List<JobObject> list)
        {
            if (list.Count == 0)
                throw new ArgumentException("List is empty");
            string zipFile = "C:\\Users\\dellx\\Desktop\\BackupWorkFiles\\ZipFile";
            RestorePoint restorePoint = new RestorePoint("C:\\Users\\dellx\\Desktop\\BackupWorkFiles");
            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                foreach (var file in list)
                {
                    archive.CreateEntryFromFile(file.Path, Path.GetFileName(file.Path));
                }

                restorePoint.AddFile(zipFile);
            }

            return restorePoint;
        }
    }
}