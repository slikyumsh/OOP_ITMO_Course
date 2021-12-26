using System;

namespace ReportDal
{
    public class TaskModification
    {
        public TaskModification()
        {
            Time = DateTime.Now;
            Id = Guid.NewGuid();
        }
        public TaskModification(ChangeType type, string content, Task task)
        {
            Time = DateTime.Now;
            CreatorId = task.EmployeeId;
            Type = type;
            Content = content;
            Task = task;
            TaskId = task.Id;
            Id = Guid.NewGuid();
        }
        public DateTime Time { get; set; }
        public Guid CreatorId { get; set; }
        public ChangeType Type { get; set; }
        public string Content { get; set; }
        public Task Task { get; set; }
        public Guid TaskId { get; set; }
        public Guid Id { get; set; }
    }

    public enum ChangeType
    {
        Type,
        Comment,
        Employer
    }
}