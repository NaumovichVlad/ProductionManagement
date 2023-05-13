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
                .ForMember(model => model.Login, dto => dto.MapFrom(s => s.Login))
                .ForMember(model => model.Password, dto => dto.MapFrom(s => s.Password))
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.RoleId, dto => dto.MapFrom(s => s.RoleId))
                .ForMember(model => model.EmployeeId, dto => dto.MapFrom(s => s.EmployeeId))
                .ForMember(model => model.EmployeeSurname, dto => dto.MapFrom(s => s.Employee.Surname))
                .ForMember(model => model.EmployeeName, dto => dto.MapFrom(s => s.Employee.Name))
                .ForMember(model => model.EmployeeMiddleName, dto => dto.MapFrom(s => s.Employee.MiddleName))
                .ForMember(model => model.RoleName, dto => dto.MapFrom(s => s.Role.Name));

            CreateMap<UserModel, UserDto>()
                .ForMember(dto => dto.Login, model => model.MapFrom(s => s.Login))
                .ForMember(dto => dto.Password, model => model.MapFrom(s => s.Password))
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.RoleId, model => model.MapFrom(s => s.RoleId))
                .ForMember(dto => dto.EmployeeId, model => model.MapFrom(s => s.EmployeeId));

            CreateMap<RoleDto, RoleModel>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeModel>().ReverseMap();
            CreateMap<CounteragentDto, CounteragentModel>().ReverseMap();
        }
    }
}
