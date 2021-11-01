using System.IO;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTests
    {
        [Test]
        [Ignore("Fails because GIT FS" )]
        public void TestFromTask1()
        {
            BackupService a = new BackupService(new SplitStoragesAlgo(), new MyRepository(@"C:\\Users\\dellx\\Desktop\\BackupWorkFiles\\"));

            JobObject x = new JobObject(@"C:\\Users\\dellx\\Desktop\\WorkFiles\\1.txt");
            JobObject y = new JobObject(@"C:\\Users\\dellx\\Desktop\\WorkFiles\\2.txt");
            JobObject z = new JobObject(@"C:\\Users\\dellx\\Desktop\\WorkFiles\\3.txt");
            a.AddJobObject(x);
            a.AddJobObject(y);
            a.MakePoint();
            a.DeleteJobObject(y);
            a.MakePoint();
            string[] AllFiles = Directory.GetFiles("C:\\Users\\dellx\\Desktop\\BackupWorkFiles","*",SearchOption.AllDirectories);
            Assert.AreEqual(3, AllFiles.Length);
            string[] AllDirectories = Directory.GetDirectories("C:\\Users\\dellx\\Desktop\\BackupWorkFiles");
            Assert.AreEqual(2, AllDirectories.Length);
            DirectoryInfo folder = new DirectoryInfo(@"C:\\Users\\dellx\\Desktop\\BackupWorkFiles");

            foreach (FileInfo file in folder.GetFiles())
            {
                file.Delete(); 
            }

            foreach (DirectoryInfo dir in folder.GetDirectories())
            {
                dir.Delete(true); 
            }
        }
        
        [Test]
        [Ignore("Fails because GIT FS" )]
        public void TestFromTask2()
        {
            BackupService a = new BackupService(new SingleStorageAlgo(), new MyRepository(@"C:\\Users\\dellx\\Desktop\\BackupWorkFiles\\"));

            JobObject x = new JobObject(@"C:\\Users\\dellx\\Desktop\\WorkFiles\\1.txt");
            JobObject y = new JobObject(@"C:\\Users\\dellx\\Desktop\\WorkFiles\\2.txt");
            JobObject z = new JobObject(@"C:\\Users\\dellx\\Desktop\\WorkFiles\\3.txt");
            a.AddJobObject(x);
            a.AddJobObject(y);
            a.MakePoint();
            
            string[] AllFiles = Directory.GetFiles("C:\\Users\\dellx\\Desktop\\BackupWorkFiles","*",SearchOption.AllDirectories);
            Assert.AreEqual(1, AllFiles.Length);
            string[] AllDirectories = Directory.GetDirectories("C:\\Users\\dellx\\Desktop\\BackupWorkFiles");
            Assert.AreEqual(1, AllDirectories.Length);
            DirectoryInfo folder = new DirectoryInfo(@"C:\\Users\\dellx\\Desktop\\BackupWorkFiles");

            foreach (FileInfo file in folder.GetFiles())
            {
                file.Delete(); 
            }

            foreach (DirectoryInfo dir in folder.GetDirectories())
            {
                dir.Delete(true); 
            }
        }
    }
}