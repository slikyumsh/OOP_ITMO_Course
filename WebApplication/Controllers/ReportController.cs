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
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportService _service;

        public ReportController(IMapper mapper, IReportService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ReportDto>> CreateTask(string name, Guid employeeId)
        {
            var report = _service.CreateReport(name, employeeId);
            return _mapper.Map<ReportDto>(report);
        }
        
        [HttpGet("AllReports")]
        public async Task<ActionResult<List<ReportDto>>> GetAll()
        {
            List<ReportDal.Report> report = _service.GetAllReports();
            return _mapper.Map<List<ReportDto>>(report);
        }
        
        [HttpPut("ChangeStatus")]
        public async Task<ActionResult<TaskDto>> ChangeTaskStatus(ReportStatus status, Guid reportId)
        {
            _service.ChangeStatus(status, reportId);
            return Ok();
        }
        
        [HttpPut("AddDiscription")]
        public async Task<ActionResult<TaskDto>> AddDiscription(string text, Guid reportId)
        {
            _service.AddDescription(text, reportId);
            return Ok();
        }
        
        [HttpPut("AddTaskToReport")]
        public async Task<ActionResult<TaskDto>> AddTaskToReport(Guid taskId, Guid reportId)
        {
            _service.AddTaskToReport(taskId, reportId);
            return Ok();
        }
    }
}