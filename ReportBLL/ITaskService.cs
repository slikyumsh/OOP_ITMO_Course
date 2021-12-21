using System;
using System.Collections.Generic;
using ReportDal;

namespace ReportBLL
{
    public interface ITaskService
    {
        public Task FindTaskForNumber(int number);
        public Task FindForId(Guid id);
        public Task FindTaskForCreatingTime(DateTime time);
        public List<Task> FindTaskForModificationTime(DateTime time);
        public List<Task> FindTaskForEmployeeTask(Guid employeeId);
        public List<Task> FindTaskForEmployeeModification(Guid employeeId);
        public Task CreateTask(string name, Guid employeeId);
        public void ChangeTaskStatus(TaskStatus newStatus, Guid taskId);
        public void ChangeTaskEmployee(Guid employeeId, Guid taskId);
        public Task AddComment(string comment, Guid taskId, Guid employeeId);
        public List<Task> GetAllTasks();
        public void SolveTask(Guid employeeId, Guid taskId);
    }
}