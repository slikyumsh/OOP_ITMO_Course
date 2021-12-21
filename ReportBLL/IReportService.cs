using System;
using System.Collections.Generic;
using ReportDal;

namespace ReportBLL
{
    public interface IReportService
    {
        public Report CreateReport(string name, Guid employeeId);
        public List<Report> GetAllReports();
        public void ChangeStatus(ReportStatus status, Guid reportId);
        public void AddDescription(string text, Guid reportId);
        public void AddTaskToReport(Guid taskId, Guid reportId);
    }
}