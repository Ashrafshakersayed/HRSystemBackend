using AutoMapper;
using HR_Employees.Dtos;
using HR_Employees.Entities;

namespace HR_Employees.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, AuthenticateResponse>();
        }
    }
}
