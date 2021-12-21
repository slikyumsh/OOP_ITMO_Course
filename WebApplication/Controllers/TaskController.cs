using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Report.Models;
using ReportBLL;
using ReportDal;
using Task = ReportDal.Task;
using TaskStatus = ReportDal.TaskStatus;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaskService _service;

        public TaskController(IMapper mapper, ITaskService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<TaskDto> Get()
        {
            Task a = new Task{Employee = new Employee("Dima", null), Number = 1, Status = TaskStatus.Open, EmployeeId = Guid.NewGuid(), Comment = "lol"};

            return new List<TaskDto>(_mapper.Map<List<TaskDto>>(new List<Task> { a }));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskDto>> GetTaskId(Guid id)
        {
            var task = _service.FindForId(id);
            return _mapper.Map<TaskDto>(task);
        }
        
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask([FromQuery]string name, [FromQuery]Guid employeeId)
        {
            var task = _service.CreateTask(name, employeeId);
            return _mapper.Map<TaskDto>(task);
        }
    }
}