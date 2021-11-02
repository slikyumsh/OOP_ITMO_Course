using System;
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class MyRepository : IRepository
    {
        private static int _numberOfRestorePoints = 1;
        private string _path;

        public MyRepository(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Empty String");
            _path = path;
        }

        public void SavePoint(RestorePoint restorePoint)
        {
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentException("Empty String");
            int counterFiles = 1; // A counter for counting files, as well as for their correct naming
            Directory.CreateDirectory(_path + "/RestorePoint" + Convert.ToString(_numberOfRestorePoints));

            foreach (var file in restorePoint.Jobs)
            {
                File.Move(Convert.ToString(file), _path + "/RestorePoint" + Convert.ToString(_numberOfRestorePoints) + "/" + Convert.ToString(counterFiles) + ".txt");
                counterFiles++;
            }

            _numberOfRestorePoints++;
        }
    }
}