using AutoMapper;
using HR_Employees.Dtos;
using HR_Employees.Entities;

namespace HR_Employees.AutoMapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeesListDto>().AfterMap((src, dest) => dest.ManagerName =
                    src.Manager?.Name); 
        }
    }
}
