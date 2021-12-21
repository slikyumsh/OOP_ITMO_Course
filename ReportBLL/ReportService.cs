using System;
using System.Collections.Generic;
using System.Linq;
using ReportDal;

namespace ReportBLL
{
    public class ReportService : IReportService
    {
        private readonly ReportContext _context;

        public ReportService(ReportContext context)
        {
            _context = context;
        }
        

        public Report CreateReport(string name, Guid employeeId)
        {
            Employee employee = FindEmployeeById(employeeId);
            return CreateReport(name, employee);
        }

        public Report FindReportForId(Guid id)
        {
            Report desiredReport = _context.Reports.SingleOrDefault(desiredReport => desiredReport.Id == id);
            return desiredReport;
        }

        public List<Report> GetAllReports()
        {
            var reports = _context.Reports.ToList();
            return reports;
        }

        public void ChangeStatus(ReportStatus status, Guid reportId)
        {
            Report report = FindReportForId(reportId);
            ChangeStatus(status, report);
        }

        public void AddDescription(string text, Guid reportId)
        {
            Report report = FindReportForId(reportId);
            AddDescription(text, report);
        }

        public void AddTaskToReport(Guid taskId, Guid reportId)
        {
            Report report = FindReportForId(reportId);
            Task task = FindTaskById(taskId);
            AddTaskToReport(task, report);
        }

        private void ChangeStatus(ReportStatus status, Report report)
        {
            if (report is null)
                throw new ArgumentException("Null report");
            var reports = _context.Reports.ToList();
            Report desiredReport = reports.SingleOrDefault(desiredReport => desiredReport.Id == report.Id);
            if (desiredReport is null)
                throw new ArgumentException("Can't find this report");
            desiredReport.ChangeStatus(status);
            _context.SaveChanges();
        }
        
        
        private void AddDescription(string text, Report report)
        {
            if (report is null)
                throw new ArgumentException("Null report");
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Empty text");
            var reports = _context.Reports.ToList();
            Report desiredReport = reports.SingleOrDefault(desiredReport => desiredReport.Id == report.Id);
            if (desiredReport is null)
                throw new ArgumentException("Can't find this report");
            desiredReport.AddDescription(text);
            _context.SaveChanges();
        }

        private void AddTaskToReport(Task task, Report report)
        {
            if (report is null)
                throw new ArgumentException("Null report");
            if (task is null)
                throw new ArgumentException("Null task");
            var reports = _context.Reports.ToList();
            Report desiredReport = reports.SingleOrDefault(desiredReport => desiredReport.Id == report.Id);
            if (desiredReport is null)
                throw new ArgumentException("Can't find this report");
            desiredReport.ReportTasks.Add(task);
            _context.SaveChanges();
        }
        
        private Report CreateReport(string name, Employee employee)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Empty Report name");
            if (employee is null)
                throw new ArgumentException("Null employee");
            var report = new Report(name);
            employee.Report = report;
            _context.Reports.Add(report);
            _context.SaveChanges();
            return report;
        }
        
        private Employee FindEmployeeById(Guid Id)
        {
            Employee desiredEmployee = _context.Employees.SingleOrDefault(desiredEmployee => desiredEmployee.Id == Id);
            return desiredEmployee;
        }
        
        private Task FindTaskById(Guid Id)
        {
            Task desiredTask = _context.Tasks.SingleOrDefault(desiredTask => desiredTask.Id == Id);
            return desiredTask;
        }
    }
}