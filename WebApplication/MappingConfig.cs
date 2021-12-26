using AutoMapper;
using Report.Models;
using ReportDal;

namespace WebApplication
{
    public class MappingConfig  : Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Task, TaskDto>();
            CreateMap<ReportDal.Report, ReportDto>();
        }
    }
}