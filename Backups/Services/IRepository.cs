using System.Collections.Generic;

namespace Backups
{
    public interface IRepository
    {
        public string Path { get; }
        public List<RestorePoint> RestorePoints { get; }
        public int NumberOfRestorePoints { get; }
        public void SavePoint(RestorePoint restorePoint);
    }
}