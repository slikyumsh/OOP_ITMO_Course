using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Report.Models;
using ReportBLL;
using ReportDal;


namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;

        public EmployeeController(IMapper mapper, IEmployeeService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<EmployeeDto>> CreateTask([FromQuery]string name, [FromQuery]Guid employeeId)
        {
            var employee = _service.CreateEmployee(name, employeeId);
            return _mapper.Map<EmployeeDto>(employee);
        }
        
        [HttpDelete("Delete")]
        public async Task<ActionResult<EmployeeDto>> DeleteTask(Guid employeeId)
        {
            _service.Delete(employeeId);
            return Ok();
        }
        
        [HttpGet("AllEmployees")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAll()
        {
            List<Employee> employees = _service.GetAll();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }
        
        [HttpGet("GetEmployeeById")]
        public async Task<ActionResult<EmployeeDto>>GetEmployeeById(Guid id)
        {
            Employee employee = _service.GetEmployeeById(id);
            return _mapper.Map<EmployeeDto>(employee);
        }
        
        [HttpGet("GetEmployeeByNumber")]
        public async Task<ActionResult<EmployeeDto>>GetEmployeeByNumber(int number)
        {
            Employee employee = _service.GetEmployeeByNumber(number);
            return _mapper.Map<EmployeeDto>(employee);
        }
        
        [HttpGet("GetFinalReport")]
        public async Task<ActionResult<ReportDto>>GetEmployeeByNumber()
        {
            ReportDal.Report report = _service.GetFinalReport();
            return _mapper.Map<ReportDto>(report);
        }
    }
}