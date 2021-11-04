using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups
{
    public class BackupService
    {
        private IAlgorithm _algorithm;
        private IRepository _repository;
        private List<JobObject> _jobObjects;

        public BackupService(IAlgorithm algorithm, IRepository repository)
        {
            _algorithm = algorithm;
            _repository = repository;
            _jobObjects = new List<JobObject>();
        }

        public void MakePoint()
        {
            _repository.SavePoint(_algorithm.MakePoint(_jobObjects));
        }

        public void AddJobObject(JobObject jobObject)
        {
            if (string.IsNullOrEmpty(jobObject.Path))
                throw new ArgumentException("Empty Job");
            _jobObjects.Add(jobObject);
        }

        public void DeleteJobObject(JobObject jobObject)
        {
            if (string.IsNullOrEmpty(jobObject.Path))
                throw new ArgumentException("Empty Job");
            JobObject desiredJobObject = _jobObjects.SingleOrDefault(desiredJobObject => desiredJobObject.Path == jobObject.Path);
            if (desiredJobObject == null)
                throw new ArgumentException("We haven't this job at this Point");
            _jobObjects.Remove(jobObject);
        }
    }
}