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
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReportService _service;

        public ReportController(IMapper mapper, IReportService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<ReportDto> Get()
        {
            var a = new ReportDal.Report
                {Name = "Dima", Status = ReportStatus.Open, Id = Guid.NewGuid()};
            return new List<ReportDto>(_mapper.Map<List<ReportDto>>(new List<ReportDal.Report> {a}));
        }
    }
}