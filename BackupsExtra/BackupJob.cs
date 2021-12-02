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
        public BackupJob(IAlgorithm algorithm, IRepository repository)
            : base(algorithm, repository)
        {
            _dateOfBirth = DateTime.Now;
            _id = _idCounter++;
        }

        public int Id => _id;
        public DateTime DateOfBirth => _dateOfBirth;
    }
}