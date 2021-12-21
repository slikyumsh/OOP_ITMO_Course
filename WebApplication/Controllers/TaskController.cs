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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskDto>> GetTaskId(Guid id)
        {
            var task = _service.FindForId(id);
            return _mapper.Map<TaskDto>(task);
        }
        
        [HttpPost("Create")]
        public async Task<ActionResult<TaskDto>> CreateTask([FromQuery]string name, [FromQuery]Guid employeeId)
        {
            var task = _service.CreateTask(name, employeeId);
            return _mapper.Map<TaskDto>(task);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TaskDto>> AddComment([FromQuery]string comment, [FromRoute] Guid id, [FromQuery]Guid employeeId)
        {
            var task = _service.AddComment(comment, id, employeeId );
            return _mapper.Map<TaskDto>(task);
        }
        
        [HttpGet("ByCreationTime")]
        public async Task<ActionResult<TaskDto>> GetCreationTime(DateTime time)
        {
            Task task = _service.FindTaskForCreatingTime(time);
            return _mapper.Map<TaskDto>(task);
        }
        
        [HttpGet("ByModificationTime")]
        public async Task<ActionResult<List<TaskDto>>> GetModificationTime(DateTime time)
        {
            List<Task> task = _service.FindTaskForModificationTime(time);
            return _mapper.Map<List<TaskDto>>(task);
        }
        
        [HttpGet("ByNumber")]
        public async Task<ActionResult<TaskDto>> GetByNumber(int number)
        {
            Task task = _service.FindTaskForNumber(number);
            return _mapper.Map<TaskDto>(task);
        }
        
        [HttpGet("ByEmployeeId")]
        public async Task<ActionResult<List<TaskDto>>> GetEmployeeId(Guid id)
        {
            List<Task> task = _service.FindTaskForEmployeeTask(id);
            return _mapper.Map<List<TaskDto>>(task);
        }
        
        [HttpGet("AllTasks")]
        public async Task<ActionResult<List<TaskDto>>> GetAllCreation()
        {
            List<Task> task = _service.GetAllTasks();
            return _mapper.Map<List<TaskDto>>(task);
        }
        
        [HttpGet("FindTaskForEmployeeModification")]
        public async Task<ActionResult<List<TaskDto>>> FindTaskForEmployeeModification(Guid id)
        {
            List<Task> task = _service.FindTaskForEmployeeModification(id);
            return _mapper.Map<List<TaskDto>>(task);
        }
        
        [HttpPut("ChangeTaskEmployee")]
        public async Task<ActionResult<TaskDto>> ChangeTaskEmployee(Guid id, Guid employeeId)
        {
            _service.ChangeTaskEmployee(employeeId, id);
            return Ok();
        }
        
        [HttpPut("ChangeTaskStatus")]
        public async Task<ActionResult<TaskDto>> ChangeTaskStatus(TaskStatus status, Guid taskId)
        {
            _service.ChangeTaskStatus(status, taskId);
            return Ok();
        }
        
        [HttpPut("SolveTask")]
        public async Task<ActionResult<TaskDto>> SolveTask(Guid employeeId, Guid taskId)
        {
            _service.SolveTask(employeeId, taskId);
            return Ok();
        }
    }
}