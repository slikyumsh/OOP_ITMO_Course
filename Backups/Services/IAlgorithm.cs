using System.Collections.Generic;

namespace Backups
{
    public interface IAlgorithm
    {
        public RestorePoint MakePoint(List<JobObject> listJobObjects);
    }
}