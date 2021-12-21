using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReportDal;

namespace ReportBLL
{
    public class TaskService : ITaskService
    {
        private readonly ReportContext _context;

        public TaskService(ReportContext context)
        {
            _context = context;
        }

        public Task FindTaskForNumber(int number)
        {
            if (number < 0)
                throw new ArgumentException("Invalid task number");
            var tasks = _context.Tasks.ToList();
            Task desiredTask = tasks.SingleOrDefault(desiredTask => desiredTask.Number == number);
            if (desiredTask == null)
                throw new ArgumentException("Can't find this task");
            return desiredTask;
        }
        
        public Task FindForId(Guid id)
        {
            var tasks = _context.Tasks.ToList();
            Task desiredTask = tasks.SingleOrDefault(desiredTask => desiredTask.Id == id);
            if (desiredTask == null)
                throw new ArgumentException("Can't find this task");
            return desiredTask;
        }

        public Task FindTaskForCreatingTime(DateTime time)
        {
            var tasks = _context.Tasks.ToList();
            Task desiredTask = tasks.SingleOrDefault(desiredTask => desiredTask.CreationTime == time);
            if (desiredTask == null)
                throw new ArgumentException("Can't find this task");
            return desiredTask;
        }
        
        public List<Task> FindTaskForModificationTime(DateTime time)
        {
            var tasks = _context.Tasks.ToList();
            List<Task> desiredTasks = tasks.FindAll(desiredTask => desiredTask.ModificationTime == time);
            if (desiredTasks == null)
                throw new ArgumentException("Can't find this task");
            return desiredTasks;
        }
        
        public List<Task> FindTaskForEmployeeTask(Guid employeeId)
        {
            var tasks = _context.Tasks.ToList();
            List<Task> desiredTasks = tasks.FindAll(desiredTask => desiredTask.EmployeeId == employeeId);
            if (desiredTasks == null)
                throw new ArgumentException("Can't find this task");
            return desiredTasks;
        }
        
        public List<Task> FindTaskForEmployeeModification(Guid employeeId)
        {
            var tasks = _context.Tasks.Include(task => task.Modifications).ToList();
            var desiredTasks = new List<Task>();
            foreach (Task task in tasks)
            {
                List<TaskModification> modifications = task.Modifications;
                List<TaskModification> desiredModifications =
                    modifications.FindAll(desiredModification => desiredModification.CreatorId == employeeId);
                if (desiredModifications.Any())
                    desiredTasks.Add(task);
            }

            if (desiredTasks is null)
                throw new ArgumentException("There are not any tasks, that modified by this employee");
            return desiredTasks;
        }

        public Task CreateTask(string name, Guid employeeId)
        {
            Employee employee = FindEmployeeById(employeeId);
            return CreateTask(name, employee);
        }

        public void ChangeTaskStatus(TaskStatus newStatus, Guid taskId)
        {
            Task task = FindForId(taskId);
            ChangeTaskStatus(newStatus, task);
        }

        public void ChangeTaskEmployee(Guid employeeId, Guid taskId)
        {
            Employee employee = FindEmployeeById(employeeId);
            Task task = FindForId(taskId);
            ChangeTaskEmployee(employee, task);
        }

        public Task AddComment(string comment, Guid taskId, Guid employeeId)
        {
            Employee employee = FindEmployeeById(employeeId);
            Task task = FindForId(taskId);
            AddComment(comment, task, employee);
            return task;
        }


        public List<Task> GetAllTasks()
        {
            var tasks = _context.Tasks.ToList();
            return tasks;
        }

        public Task GetTaskByNumber(int number)
        {
            if (number < 0)
                throw new ArgumentException("Invalid employee number");
            List<Task> tasks = GetAllTasks();
            if (number >= tasks.Count)
                throw new ArgumentException("Number is bigger than length");
            return tasks[number];
        }

        public void SolveTask(Guid employeeId, Guid taskId)
        {
            Employee employee = FindEmployeeById(employeeId);
            Task task = FindForId(taskId);
            SolveTask(employee, task);
        }
        
        private Task CreateTask(string name, Employee employee)
        {
            if (employee is null)
                throw new ArgumentException("Null employee");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Empty name");
            var newTask = new Task(name, employee);
            _context.Tasks.Add(newTask);
            _context.SaveChanges();
            return newTask;
        }
        private void ChangeTaskStatus(TaskStatus newStatus, Task task)
        {
            if (task is null)
                throw new ArgumentException("Null task");
            string content = "Status changed from " + task.Status + " to " + newStatus;
            task.ChangeStatus(newStatus);
            var modification = new TaskModification(ChangeType.Type, content, task);
            task.AddModification(modification);
            _context.Add(modification);
            _context.Update(task);
            _context.SaveChanges();
        }
        
        private void ChangeTaskEmployee(Employee newEmployee, Task task)
        {
            if (task is null)
                throw new ArgumentException("Null task");
            if (newEmployee is null)
                throw new ArgumentException("Null task");
            string content = "Employee changed from " + task.EmployeeId + " to " + newEmployee.Id;
            task.ChangeEmployee(newEmployee);
            var modification = new TaskModification(ChangeType.Employer, content, task);
            task.AddModification(modification);
            _context.Add(modification);
            _context.Update(task);
            _context.SaveChanges();
        }
        
        private Task AddComment(string comment, Task task, Employee employee)
        {
            if (task is null)
                throw new ArgumentException("Null task");
            if (string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException("Empty comment");
            if (employee is null)
                throw new ArgumentException("Null employee");
            string content = "Comment added " + comment;
            task.AddComment(comment);
            var modification = new TaskModification(ChangeType.Comment, content, task);
            task.AddModification(modification);
            _context.Update(task);
            _context.Add(modification);
            _context.SaveChanges();
            return task;
        }

        private void SolveTask(Employee employee, Task task)
        {
            if (employee is null)
                throw new ArgumentException("Null employee");
            if (task is null)
                throw new ArgumentException("Null task");
            if (task.EmployeeId != employee.Id)
                throw new ArgumentException("This is not his task");
            task.ChangeStatus(TaskStatus.Resolved);
            _context.Update(task);
            employee.Report.AddTask(task);
            _context.Update(employee);
            _context.SaveChanges();
        }

        private Employee FindEmployeeById(Guid Id)
        {
            Employee desiredEmployee = _context.Employees.SingleOrDefault(desiredEmployee => desiredEmployee.Id == Id);
            return desiredEmployee;
        }
    }
}