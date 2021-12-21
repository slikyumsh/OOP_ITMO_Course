using System;

namespace Report.Models
{
    public class TaskDto
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public string Comment { get; set; }
        public Guid Id { get; set; }
    }
}