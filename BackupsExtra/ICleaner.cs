using System.Collections.Generic;
using System.IO;
using Backups;

namespace BackupsExtra
{
    public interface ICleaner
    {
        void ClearPoints();
        Dictionary<DirectoryInfo, int> MarkPoints();
    }
}