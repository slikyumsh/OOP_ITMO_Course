using System;
using System.Collections.Generic;

namespace ReportDal
{
    public class Task
    {
        private static int _currentNumberOfNextTask = 0;

        public Task()
        {
            Number = _currentNumberOfNextTask++;
            Name = "NewTask";
            Status = TaskStatus.Open;
            CreationTime = DateTime.Now;
            ModificationTime = DateTime.Now;
            Comment = String.Empty;
            Id = Guid.NewGuid();
            Modifications = new List<TaskModification>();
        }
        public Task(string name, Employee employee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Empty name");
            if (employee is null)
                throw new ArgumentException("Null employee");
            Name = name;
            Number = _currentNumberOfNextTask++;
            Status = TaskStatus.Open;
            Employee = employee;
            EmployeeId = employee.Id;
            CreationTime = DateTime.Now;
            ModificationTime = DateTime.Now;
            Modifications = new List<TaskModification>();
            Id = Guid.NewGuid();
        }
        
        public string Name { get; private set; }
        public int Number { get; init; }
        public Guid Id { get; init; }
        public TaskStatus Status { get; set; }
        public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
        
        public DateTime CreationTime { get; init; }
        public DateTime ModificationTime { get; set; }

        public List<TaskModification> Modifications { get; set; }
        public string Comment { get; set; }

        public void AddComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Empty comment");
            Comment += comment;
            ModificationTime = DateTime.Now;
        }
        
        public void ChangeStatus(TaskStatus newStatus)
        {
            Status = newStatus;
            ModificationTime = DateTime.Now;
        }

        public void ChangeEmployee(Employee employee)
        {
            if (employee is null)
                throw new ArgumentException("Null Employee");
            Employee = employee;
            EmployeeId = employee.Id;
            ModificationTime = DateTime.Now;
        }

        public void AddModification(TaskModification modification)
        {
            if (modification is null)
                throw new ArgumentException("Null modification");
            Modifications.Add(modification);
        }
        
        
    }

    public enum TaskStatus
    {
        Open,
        Active,
        Resolved
    }
}