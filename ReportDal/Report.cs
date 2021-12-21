using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportDal
{
    public class Report
    {
        public Report()
        {
            Name = "NewReport";
            Id = Guid.NewGuid();
            Status = ReportStatus.Open;
            ReportTasks = new List<Task>();
            CreationTime = DateTime.Now;
            Description = String.Empty;
        }
        public Report(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Empty name");
            Id = Guid.NewGuid();
            Name = name;
            Status = ReportStatus.Open;
            ReportTasks = new List<Task>();
            CreationTime = DateTime.Now;
            Description = String.Empty;
        }
        
        public string Name { get; set; }

        public List<Task> ReportTasks { get; set; }
        public Guid Id { get; init; }
        public ReportStatus Status { get; set; }
        public DateTime CreationTime { get; init; }
        public string Description { get; set; }

        public void ChangeStatus(ReportStatus statsus)
        {
            Status = statsus;
        }

        public void AddDescription(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Empty description");
            Description += text;
        }
        
        public Report MergeReports(Report first, Report second)
        {
            if (first is null)
                throw new ArgumentException("Null report");
            if (second is null)
                throw new ArgumentException("Null report");
            first.ReportTasks.Union(second.ReportTasks);
            return first;
        }

        public void AddTask(Task task)
        {
            if (task is null)
                throw new ArgumentException("Null Task");
            ReportTasks.Add(task);
        }
    }

    public enum ReportStatus
    {
        Open,
        Closed
    }
}