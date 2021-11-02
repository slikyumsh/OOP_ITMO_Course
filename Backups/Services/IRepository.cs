namespace Backups
{
    public interface IRepository
    {
        public void SavePoint(RestorePoint restorePoint);
    }
}