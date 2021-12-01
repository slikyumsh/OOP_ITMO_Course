using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Backups;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/WorkFiles/");
            FileStream fileStream1 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/WorkFiles/1.txt");
            FileStream fileStream2 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/WorkFiles/2.txt");
            FileStream fileStream3 = File.Create(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/WorkFiles/3.txt");
            fileStream1.Close();
            fileStream2.Close();
            fileStream3.Close();
            Directory.CreateDirectory(
                Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName +
                "/BackupsExtra/BackupWorkFiles/");
            BackupExtraService service = new BackupExtraService();
            BackupJob a = service.CreateBackupJob(new SingleStorageAlgo(), new Repository(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/BackupWorkFiles/"), new ConsoleLogger());
            JobObject x = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/WorkFiles/1.txt");
            JobObject y = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/WorkFiles/2.txt");
            JobObject z = new JobObject(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/WorkFiles/3.txt");
            service.AddJobObject(x, a);
            service.AddJobObject(y, a);
            service.CreateRestorePoint(a);

            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/WorkFiles/", true);
            Directory.Delete(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName + "/BackupsExtra/BackupWorkFiles/", true);
        }
    }
}
