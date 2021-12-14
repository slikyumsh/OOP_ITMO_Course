using System;
using System.IO;
using System.Linq;
using Backups;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        private BackupExtraService _service;

        [SetUp]
        public void Setup()
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
            _service = new BackupExtraService(new ConsoleLogger(), 3, new Paths(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/WorkFiles/", Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                                                  "/BackupsExtra.Tests"));
        }

        [Test]
        public void TryUnpackToAnotherDirectory()
        {
            BackupJob a = _service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"));
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            _service.AddJobObject(x, a);
            _service.AddJobObject(y, a);
            RestorePoint c = _service.CreateRestorePoint(a);
            _service.AddJobObject(z, a);
            _service.CreateRestorePoint(a);
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra.Tests/Dima/");
            _service.RestoreFilesFromPoint(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/RestorePoint9/", Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/Dima/");
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
            BackupJob a = _service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"));
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            _service.AddJobObject(x, a);
            _service.AddJobObject(y, a);
            RestorePoint c = _service.CreateRestorePoint(a);
            _service.AddJobObject(z, a);
            _service.CreateRestorePoint(a);
            _service.Clear(new ClearByNumber(1, _service.BackupJobs[0]));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(1, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
        
        [Test]
        public void TryToCleanPointsByDate()
        {
            BackupJob a = _service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"));
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            _service.AddJobObject(x, a);
            _service.AddJobObject(y, a);
            RestorePoint c = _service.CreateRestorePoint(a);
            _service.AddJobObject(z, a);
            _service.CreateRestorePoint(a);
            _service.Clear(new ClearByDate(DateTime.Today, _service.BackupJobs[0]));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(2, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
        
        [Test]
        public void TryToCleanPointsHybridOneRuleFromTwo()
        {
            BackupJob d = _service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"));
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            _service.AddJobObject(x, d);
            _service.AddJobObject(y, d);
            _service.CreateRestorePoint(d);
            _service.CreateRestorePoint(d);
            _service.Clear(new HybridClear(DateTime.Today, d, 1, false));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(1, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
        
        [Test]
        public void TryToCleanPointsHybridAllRules()
        {
            BackupJob a = _service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/"));
            var x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/A.txt");
            var y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/B.txt");
            var z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/C.txt");
            _service.AddJobObject(x, a);
            _service.AddJobObject(y, a);
            RestorePoint c = _service.CreateRestorePoint(a);
            _service.AddJobObject(z, a);
            _service.CreateRestorePoint(a);
            _service.Clear(new HybridClear(DateTime.Today, _service.BackupJobs[0], 1, true));
            string[] directories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/");
            Assert.AreEqual(2, directories.Length);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra.Tests/BackupWorkFiles/", true);
        }
    }
}