using System;

namespace ReportDal
{
    public class TaskModification
    {
        public DateTime Time { get; set; }
        public Guid CreatorId { get; set; }
        public ChangeType Type { get; set; }
        public string Content { get; set; }
    }

    public enum ChangeType
    {
        Type,
        Comment,
        Employer
    }
}