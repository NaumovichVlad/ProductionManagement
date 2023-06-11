using AutoMapper;
using BusinessLogic.Dtos;
using DataAccess.Entities;

namespace BusinessLogic.Mapper
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            CreateMap<Counteragent, CounteragentDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<FinishedProduct, FinishedProductDto>().ReverseMap();
            CreateMap<FinishedProductSale, FinishedProductsSaleDto>().ReverseMap();
            CreateMap<Material, MaterialDto>().ReverseMap();
            CreateMap<Purchase, PurchaseDto>().ReverseMap();
            CreateMap<MaterialReserve, MaterialReserveDto>().ReverseMap();
            CreateMap<MaterialsForFinishedProducts, MaterialsForFinishedProductsDto>().ReverseMap();
            CreateMap<MaterialsForProducts, MaterialsForProductsDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<ProductsReserve, ProductsReserveDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Salary, SalaryDto>().ReverseMap();
            CreateMap<StoragePlace, StoragePlaceDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<MaterialPurchase, MaterialPurchaseDto>().ReverseMap();
        }
    }
}
