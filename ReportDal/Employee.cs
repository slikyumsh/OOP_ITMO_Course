using System;

namespace ReportDal
{
    public class Employee
    {

        public Employee()
        {
            Name = "EmployeeName";
            Boss = null;
            BossId = Guid.Empty;
            Id = Guid.NewGuid();
            Report = new Report("Report" + Id);
        }

        public Employee(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid name");
            Name = name;
            Report = new Report("Report" + name + Id);
            Id = Guid.NewGuid();
        }

        public string Name { get; init; }
        public Guid Id { get; init; }
        public Employee Boss { get; private set; }
        public Guid? BossId { get; private set; }
        public Report Report { get; set; }

        public void SetBoss(Employee boss)
        {
            Boss = boss;
        }

        public void AddTask(Task task)
        {
            if (task is null)
                throw new ArgumentException("Null Task");
            Report.AddTask(task);
        }
    }
}