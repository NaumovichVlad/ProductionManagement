using AutoMapper;
using BusinessLogic.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mapper
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile() 
        {
            CreateMap<Counteragent, CounteragentDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<FinishedProduct, FinishedProductDto>().ReverseMap();
            CreateMap<FinishedProductsForOrder, FinishedProductsForOrderDto>().ReverseMap();
            CreateMap<Material, MaterialDto>().ReverseMap();
            CreateMap<MaterialOrder, MaterialOrderDto>().ReverseMap();
            CreateMap<MaterialReserve, MaterialReserveDto>().ReverseMap();
            CreateMap<MaterialsForFinishedProducts, MaterialsForFinishedProductsDto>().ReverseMap();
            CreateMap<MaterialsForProducts, MaterialsForProductsDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductOrder, ProductOrderDto>().ReverseMap();
            CreateMap<ProductsForOrder, ProductsForOrderDto>().ReverseMap();
            CreateMap<ProductsReserve, ProductsReserveDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Salary, SalaryDto>().ReverseMap();
            CreateMap<StoragePlace, StoragePlaceDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
