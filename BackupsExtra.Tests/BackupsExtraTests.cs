using System;
using System.IO;
using Backups;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        [Test]
        public void TestDownloadAndSave()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/WorkFiles/1.txt");
            fileStream1.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/BackupWorkFiles/");
            BackupExtraService service = new BackupExtraService();
            Assert.AreEqual(0,service.TestNumberOfDownloadedBackupJobs());
            service.Download();
            int x = service.TestNumberOfDownloadedBackupJobs();
            BackupJob job = service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/BackupWorkFiles/"), new ConsoleLogger());
            service.Save();
            int y = service.TestNumberOfDownloadedBackupJobs();
            Assert.AreEqual(1, y - x);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/BackupWorkFiles/", true);

        }
    }
}