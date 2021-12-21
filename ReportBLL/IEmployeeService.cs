using System;
using System.Collections.Generic;
using ReportDal;

namespace ReportBLL
{
    public interface IEmployeeService
    {
        public Employee CreateEmployee(string name, Guid bossId);
        public List<Employee> GetAll();
        public Employee GetEmployeeById(Guid id);
        public void Delete(Guid employeeId);
        public Employee GetEmployeeByNumber(int number);
        public Report GetFinalReport();
    }
}