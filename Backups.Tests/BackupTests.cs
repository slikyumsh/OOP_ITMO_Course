using System;
using System.IO;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTests
    {
        [Test]
        public void TestFromTusk1()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/1.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/2.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/3.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/BackupWorkFiles/");
            BackupService a = new BackupService(new SplitStoragesAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles/"));
            JobObject x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/1.txt");
            JobObject y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/2.txt");
            JobObject z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/3.txt");

            a.AddJobObject(x);
            a.AddJobObject(y);
            a.MakePoint();
            a.DeleteJobObject(y);
            a.MakePoint();
            string[] allFiles = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles", "*", SearchOption.AllDirectories);
            Assert.AreEqual(3, allFiles.Length);
            
            string[] allDirectories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles", "*", SearchOption.AllDirectories);
            Assert.AreEqual(2, allDirectories.Length);
            
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName+ "/Backups/BackupWorkFiles/", true);
        }
        
        [Test]

        public void TestFromTusk2()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/1.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/2.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/WorkFiles/3.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/Backups/BackupWorkFiles/");
            BackupService a = new BackupService(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles/"));
            JobObject x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/1.txt");
            JobObject y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/2.txt");
            JobObject z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/3.txt");

            a.AddJobObject(x);
            a.AddJobObject(y);
            a.MakePoint();
            
            string[] allFiles = Directory.GetFiles(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles", "*", SearchOption.AllDirectories);
            Assert.AreEqual(1, allFiles.Length);
            
            string[] allDirectories = Directory.GetDirectories(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/BackupWorkFiles", "*", SearchOption.AllDirectories);
            Assert.AreEqual(1, allDirectories.Length);
            
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/Backups/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName+ "/Backups/BackupWorkFiles/", true);
        }
    }
}