using System;

namespace Report.Models
{
    public class EmployeeDto
    {
        public string Name { get; set; }
        public string BossName { get; set; }
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
    }
}