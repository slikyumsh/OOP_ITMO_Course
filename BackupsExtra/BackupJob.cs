using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Backups;
using Json.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BackupsExtra
{
    [Serializable]
    public class BackupJob : BackupService
    {
        [JsonProperty("idCounter")]
        private static int _idCounter;
        [JsonProperty("dateOfBirth")]
        private DateTime _dateOfBirth;
        [JsonProperty("id")]
        private int _id;
        private ILogger _logger;
        public BackupJob(IAlgorithm algorithm, IRepository repository, ILogger logger)
            : base(algorithm, repository)
        {
            _logger = logger;
            _dateOfBirth = DateTime.Now;
            _id = _idCounter++;
        }

        public int Id => _id;
        public DateTime DateOfBirth => _dateOfBirth;

        public RestorePoint MakePointWithNotice()
        {
            RestorePoint restorePoint = MakePoint();
            _logger.SendMessage("MadeRestorePoint" + restorePoint);
            return restorePoint;
        }

        public void AddJobObjectWithNotice(JobObject jobObject)
        {
            AddJobObject(jobObject);
            _logger.SendMessage("AddedJobObject" + jobObject);
        }

        public void DeleteJobObjectWithNotice(JobObject jobObject)
        {
            DeleteJobObject(jobObject);
            _logger.SendMessage("DeletedJobObject" + jobObject);
        }
    }
}