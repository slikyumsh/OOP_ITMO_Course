using System;
using System.IO;
using System.Linq;
using Backups;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {

        [Test]
        public void TryUnpackToAnotherDirectory()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/A.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/B.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/C.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/BackupWorkFiles/");
            var service = new BackupExtraService(new ConsoleLogger(), 3);
            BackupJob a = service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"), new ConsoleLogger());
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            service.AddJobObject(x, a);
            service.AddJobObject(y, a);
            RestorePoint c = service.CreateRestorePoint(a);
            service.AddJobObject(z, a);
            service.CreateRestorePoint(a);
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/Dima/");
            service.RestoreFilesFromPoint(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/RestorePoint9/", Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/Dima/");
            string[] testFiles = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                               "/BackupsExtra.Tests/Dima/");
            Assert.AreEqual(2, testFiles.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                             "/BackupsExtra.Tests/Dima/", true);
        }

        [Test]
        public void TryToCleanPointsByNumber()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/A.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/B.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/C.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/BackupWorkFiles/");
            var service = new BackupExtraService(new ConsoleLogger(), 3);
            BackupJob a = service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"), new ConsoleLogger());
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            service.AddJobObject(x, a);
            service.AddJobObject(y, a);
            RestorePoint c = service.CreateRestorePoint(a);
            service.AddJobObject(z, a);
            service.CreateRestorePoint(a);
            service.Clear(new ClearByNumber(1, service.BackupJobs));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(1, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
        
        [Test]
        public void TryToCleanPointsByDate()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/A.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/B.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/C.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/BackupWorkFiles/");
            var service = new BackupExtraService(new ConsoleLogger(), 3);
            BackupJob a = service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"), new ConsoleLogger());
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            service.AddJobObject(x, a);
            service.AddJobObject(y, a);
            RestorePoint c = service.CreateRestorePoint(a);
            service.AddJobObject(z, a);
            service.CreateRestorePoint(a);
            service.Clear(new ClearByDate(DateTime.Today, service.BackupJobs));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(2, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
        
        [Test]
        public void TryToCleanPointsHybridOneRuleFromTwo()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/A.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/B.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/C.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/BackupWorkFiles/");
            var service = new BackupExtraService(new ConsoleLogger(), 3);
            BackupJob a = service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"), new ConsoleLogger());
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            service.AddJobObject(x, a);
            service.AddJobObject(y, a);
            RestorePoint c = service.CreateRestorePoint(a);
            service.AddJobObject(z, a);
            service.CreateRestorePoint(a);
            service.Clear(new HybridClear(DateTime.Today, service.BackupJobs, 1, false));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(1, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
        
        [Test]
        public void TryToCleanPointsHybridAllRules()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/A.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/B.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/C.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/BackupWorkFiles/");
            var service = new BackupExtraService(new ConsoleLogger(), 3);
            BackupJob a = service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"), new ConsoleLogger());
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            service.AddJobObject(x, a);
            service.AddJobObject(y, a);
            RestorePoint c = service.CreateRestorePoint(a);
            service.AddJobObject(z, a);
            service.CreateRestorePoint(a);
            service.Clear(new HybridClear(DateTime.Today, service.BackupJobs, 1, true));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(2, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
    }
}