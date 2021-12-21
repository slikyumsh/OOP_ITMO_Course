using System;

namespace ReportDal
{
    public class Employee
    {

        public Employee()
        {
            Name = "EmployeeName";
            Boss = null;
            Id = Guid.NewGuid();
            Report = new Report("Report" + Id);
        }

        public Employee(string name, Employee boss)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid name");
            Name = name;
            Boss = boss;
            Report = new Report("Report" + name + Id);
            Id = Guid.NewGuid();
        }

        public string Name { get; init; }
        public Guid Id { get; init; }
        public Employee Boss { get; set; }
        public Report Report { get; set; }

        public void AddTask(Task task)
        {
            if (task is null)
                throw new ArgumentException("Null Task");
            Report.AddTask(task);
        }
    }
}