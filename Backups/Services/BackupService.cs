using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups
{
    public class BackupService
    {
        private IAlgorithm _algorithm;
        private IRepository _repository;
        private List<JobObject> _jobs;

        public BackupService(IAlgorithm algorithm, IRepository repository)
        {
            _algorithm = algorithm;
            _repository = repository;
            _jobs = new List<JobObject>();
        }

        public void MakePoint()
        {
            _repository.SavePoint(_algorithm.MakePoint(_jobs));
        }

        public void AddJobObject(JobObject job)
        {
            if (string.IsNullOrEmpty(job.Path))
                throw new ArgumentException("Empty Job");
            _jobs.Add(job);
        }

        public void DeleteJobObject(JobObject job)
        {
            if (string.IsNullOrEmpty(job.Path))
                throw new ArgumentException("Empty Job");
            JobObject desiredJobObject = _jobs.SingleOrDefault(desiredJobObject => desiredJobObject.Path == job.Path);
            if (desiredJobObject == null)
                throw new ArgumentException("We haven't this job at this Point");
            _jobs.Remove(job);
        }
    }
}