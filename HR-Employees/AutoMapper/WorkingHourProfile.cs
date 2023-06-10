using AutoMapper;
using HR_Employees.Dtos;
using HR_Employees.Entities;

namespace HR_Employees.AutoMapper
{
    public class WorkingHourProfile : Profile
    {
        public WorkingHourProfile()
        {
            CreateMap<WorkingHour, WorkingHourDto>().AfterMap((src, dest) =>
            dest.WorkingHours = DateTime.Now - src.SigninTime);

        }
    }
}
