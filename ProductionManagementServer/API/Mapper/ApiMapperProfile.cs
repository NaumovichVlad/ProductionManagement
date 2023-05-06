using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;

namespace API.Mapper
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile() 
        {
            CreateMap<SalaryDto, SalaryModel>()
                .ForMember(model => model.EmployeeSurname, dto => dto.MapFrom(s => s.Employee.Surname))
                .ForMember(model => model.EmployeeName, dto => dto.MapFrom(s => s.Employee.Name))
                .ForMember(model => model.EmployeeMiddleName, dto => dto.MapFrom(s => s.Employee.MiddleName))
                .ReverseMap();

            CreateMap<UserDto, UserModel>()
                .ForMember(model => model.Login, dto => dto.MapFrom(m => m.Login))
                .ForMember(model => model.Password, dto => dto.MapFrom(m => m.Password))
                .ReverseMap();

            CreateMap<RoleDto, RoleModel>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeModel>().ReverseMap();
        }
    }
}
