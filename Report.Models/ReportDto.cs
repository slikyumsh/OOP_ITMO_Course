using System;

namespace Report.Models
{
    public class ReportDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
    }
}