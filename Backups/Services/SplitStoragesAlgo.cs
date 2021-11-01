using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups
{
    public class SplitStoragesAlgo : IAlgorithm
    {
        public RestorePoint MakePoint(List<JobObject> list)
        {
            if (list.Count == 0)
                throw new ArgumentException("List is empty");
            int counterDirectories = 1;
            RestorePoint restorePoint = new RestorePoint("C:\\Users\\dellx\\Desktop\\BackupWorkFiles");
            foreach (var file in list)
            {
                string zipFile = "C:\\Users\\dellx\\Desktop\\BackupWorkFiles\\ZipFile" + Convert.ToString(counterDirectories);
                JobObject job = new JobObject(zipFile);
                counterDirectories++;
                using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(file.Path, Path.GetFileName(file.Path));
                }

                restorePoint.AddFile(job.Path);
            }

            return restorePoint;
        }
    }
}