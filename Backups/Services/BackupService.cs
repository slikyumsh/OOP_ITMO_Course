using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Backups
{
    public class BackupService
    {
        [JsonProperty("algorithm")]
        private IAlgorithm _algorithm;
        private IRepository _repository;
        [JsonProperty("jobObjects")]
        private List<JobObject> _jobObjects;

        public BackupService(IAlgorithm algorithm, IRepository repository)
        {
            _algorithm = algorithm;
            _repository = repository;
            _jobObjects = new List<JobObject>();
        }

        public IAlgorithm Algorithm => _algorithm;
        public IRepository Repository => _repository;
        public List<JobObject> JobObjects => _jobObjects;

        public RestorePoint MakePoint()
        {
            RestorePoint newRestorePoint = _algorithm.MakePoint(_jobObjects);
            _repository.SavePoint(newRestorePoint);
            return newRestorePoint;
        }

        public List<RestorePoint> GetListOfRestorePoints()
        {
            return _repository.RestorePoints;
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