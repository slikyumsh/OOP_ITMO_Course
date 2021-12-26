using System;
using System.Collections.Generic;
using System.Linq;
using ReportDal;

namespace ReportBLL
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ReportContext _context;

        public EmployeeService(ReportContext context)
        {
            _context = context;
        }

        public Employee CreateEmployee(string name, Guid bossId)
        {
            Employee boss = FindEmployeeById(bossId);
            return CreateEmployee(name, boss);
        }

        public List<Employee> GetAll()
        {
            var employees = _context.Employees.ToList();
            return employees;
        }

        public Employee GetEmployeeById(Guid id)
        {
            List<Employee> employees = GetAll();
            Employee desiredEmployee = employees.SingleOrDefault(desiredEmployee => desiredEmployee.Id == id);
            if (desiredEmployee is null)
                throw new ArgumentException("Can't find this employee");
            return desiredEmployee;
        }
        
        public Employee FindEmployeeById(Guid id)
        {
            List<Employee> employees = GetAll();
            Employee desiredEmployee = employees.SingleOrDefault(desiredEmployee => desiredEmployee.Id == id);
            return desiredEmployee;
        }

        public void Delete(Guid employeeId)
        {
            Employee employee = GetEmployeeById(employeeId);
            Delete(employee);
        }

        public Employee GetEmployeeByNumber(int number)
        {
            if (number < 0)
                throw new ArgumentException("Invalid employee number");
            List<Employee> employees = GetAll();
            if (number >= employees.Count)
                throw new ArgumentException("Number is bigger than length");
            return employees[number];
        }

        public Employee FindTeamLeader()
        {
            var employees = _context.Employees.ToList();
            Employee leader = employees.SingleOrDefault(leader => leader.Boss is null);
            if (leader is null)
                throw new ArgumentException("Can't find leader");
            return leader;
        }
        
        public void UpdateReports()
        {
            var employees = _context.Employees.ToList();
            Employee leader = FindTeamLeader();
            foreach (var employee in employees)
            {
                leader.Report.MergeReports(leader.Report, employee.Report);
            }
        }

        public Report GetFinalReport()
        {
            UpdateReports();
            Employee leader = FindTeamLeader();
            return leader.Report;
        }
        
        private Employee CreateEmployee(string name, Employee boss)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Empty name");
            var employees = _context.Employees.ToList();
            if (!employees.Any() && boss is null)
            {
                var teamlead = new Employee(name);
                _context.Employees.Add(teamlead);
                _context.SaveChanges();
                return teamlead;
            }
            if (boss is null)
            {
                Employee desiredEmployee = employees.SingleOrDefault(desiredEmployee => desiredEmployee.Boss is null);
                if (desiredEmployee is null)
                    throw new ArgumentException("Can't create employee");
                var teamlead = new Employee(name);
                desiredEmployee.SetBoss(teamlead);
                _context.Employees.Add(teamlead);
                _context.SaveChanges();
                return teamlead;
            }

            Employee desiredBoss = employees.SingleOrDefault(desiredBoss => desiredBoss.Id == boss.Id);
            var employee = new Employee(name);
            employee.SetBoss(desiredBoss);
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }
        
        private void Delete(Employee employee)
        {
            if (employee is null)
                throw new ArgumentException("Null employee");
            List<Employee> employees = GetAll();
            if (employee.Boss is null)
            {
                List<Employee> desiredEmployees =
                    employees.FindAll(desiredEmployee => desiredEmployee.BossId == employee.Id);
                if (!desiredEmployees.Any())
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    return;
                }

                desiredEmployees[0].SetBoss(null);
                foreach (var employer in desiredEmployees)
                {
                    if (employer.Boss != null)
                        employer.SetBoss(desiredEmployees[0]);
                }

                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            else
            {
                List<Employee> desiredEmployees =
                    employees.FindAll(desiredEmployee => desiredEmployee.BossId == employee.Id);
                foreach (var employer in desiredEmployees)
                {
                    employer.SetBoss(employee.Boss);
                }

                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
        
    }
}