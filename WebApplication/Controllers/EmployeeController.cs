using System;
using System.Collections.Generic;
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

        [HttpGet]
        public IEnumerable<EmployeeDto> Get()
        {
            Employee a = new Employee{Name = "Dima", Boss = null, Id = Guid.NewGuid()};
            
            return new List<EmployeeDto>(_mapper.Map<List<EmployeeDto>>(new List<Employee> { a }));
        }
        
    }
}