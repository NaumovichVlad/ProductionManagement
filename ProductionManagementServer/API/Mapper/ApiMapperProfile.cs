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
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.Accrued, dto => dto.MapFrom(s => s.Accrued))
                .ForMember(model => model.Paid, dto => dto.MapFrom(s => s.Paid))
                .ForMember(model => model.PaymentDate, dto => dto.MapFrom(s => s.PaymentDate))
                .ForMember(model => model.ToBePaid, dto => dto.MapFrom(s => s.ToBePaid))
                .ForMember(model => model.EmployeeSurname, dto => dto.MapFrom(s => s.Employee.Surname))
                .ForMember(model => model.EmployeeName, dto => dto.MapFrom(s => s.Employee.Name))
                .ForMember(model => model.EmployeeMiddleName, dto => dto.MapFrom(s => s.Employee.MiddleName));

            CreateMap<SalaryModel, SalaryDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.Accrued, model => model.MapFrom(s => s.Accrued))
                .ForMember(dto => dto.Paid, model => model.MapFrom(s => s.Paid))
                .ForMember(dto => dto.ToBePaid, model => model.MapFrom(s => s.ToBePaid))
                .ForMember(dto => dto.PaymentDate, model => model.MapFrom(s => s.PaymentDate));

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

            CreateMap<MaterialOrderDto, MaterialOrderModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.OrderNumber, dto => dto.MapFrom(s => s.OrderNumber))
                .ForMember(model => model.MaterialId, dto => dto.MapFrom(s => s.MaterialId))
                .ForMember(model => model.MaterialName, dto => dto.MapFrom(s => s.Material.Name))
                .ForMember(model => model.Price, dto => dto.MapFrom(s => s.Price))
                .ForMember(model => model.ManufactureDate, dto => dto.MapFrom(s => s.ManufactureDate))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count))
                .ForMember(model => model.CounteragentId, dto => dto.MapFrom(s => s.CounteragentId))
                .ForMember(model => model.CounteragentName, dto => dto.MapFrom(s => s.Counteragent.Name))
                .ForMember(model => model.ManufactureCountry, dto => dto.MapFrom(s => s.ManufactureCountry));

            CreateMap<MaterialOrderModel, MaterialOrderDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.OrderNumber, model => model.MapFrom(s => s.OrderNumber))
                .ForMember(dto => dto.MaterialId, model => model.MapFrom(s => s.MaterialId))
                .ForMember(dto => dto.Price, model => model.MapFrom(s => s.Price))
                .ForMember(dto => dto.ManufactureDate, model => model.MapFrom(s => s.ManufactureDate))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count))
                .ForMember(dto => dto.CounteragentId, model => model.MapFrom(s => s.CounteragentId))
                .ForMember(dto => dto.ManufactureCountry, model => model.MapFrom(s => s.ManufactureCountry));

            CreateMap<MaterialReserveModel, MaterialReserveDto>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.MaterialOrderId, dto => dto.MapFrom(s => s.MaterialOrderId))
                .ForMember(model => model.StoragePlaceId, dto => dto.MapFrom(s => s.StoragePlaceId))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count));


            CreateMap<MaterialReserveDto, MaterialReserveModel>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.MaterialOrderId, model => model.MapFrom(s => s.MaterialOrderId))
                .ForMember(dto => dto.StoragePlaceId, model => model.MapFrom(s => s.StoragePlaceId))
                .ForMember(dto => dto.StoragePlaceName, model => model.MapFrom(s => s.StoragePlace.Name))
                .ForMember(dto => dto.MaterialOrderNumber, model => model.MapFrom(s => s.MaterialOrder.OrderNumber))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count));

            CreateMap<MaterialsForFinishedProductsDto, MaterialsForFinishedProductsModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.ProductId, dto => dto.MapFrom(s => s.ProductId))
                .ForMember(model => model.MaterialReserveId, dto => dto.MapFrom(s => s.MaterialReserveId))
                .ForMember(model => model.ProductName, dto => dto.MapFrom(s => s.Product.Name))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count))
                .ForMember(model => model.OrderNumber, dto => dto.MapFrom(s => s.MaterialReserve.MaterialOrder.OrderNumber));

            CreateMap<MaterialsForFinishedProductsModel, MaterialsForFinishedProductsDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.ProductId, model => model.MapFrom(s => s.ProductId))
                .ForMember(dto => dto.MaterialReserveId, model => model.MapFrom(s => s.MaterialReserveId))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count));

            CreateMap<MaterialsForProductsDto, MaterialsForProductsModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.ProductId, dto => dto.MapFrom(s => s.ProductId))
                .ForMember(model => model.MaterialId, dto => dto.MapFrom(s => s.MaterialId))
                .ForMember(model => model.ProductName, dto => dto.MapFrom(s => s.Product.Name))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count))
                .ForMember(model => model.MaterialName, dto => dto.MapFrom(s => s.Material.Name));

            CreateMap<MaterialsForProductsModel, MaterialsForProductsDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.ProductId, model => model.MapFrom(s => s.ProductId))
                .ForMember(dto => dto.MaterialId, model => model.MapFrom(s => s.MaterialId))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count));

            CreateMap<ProductOrderDto, ProductOrderModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.OrderNumber, dto => dto.MapFrom(s => s.OrderNumber))
                .ForMember(model => model.OrderDate, dto => dto.MapFrom(s => s.OrderDate))
                .ForMember(model => model.CounteragentId, dto => dto.MapFrom(s => s.CounteragentId))
                .ForMember(model => model.CounteragentName, dto => dto.MapFrom(s => s.Counteragent.Name));

            CreateMap<ProductOrderModel, ProductOrderDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.OrderNumber, model => model.MapFrom(s => s.OrderNumber))
                .ForMember(dto => dto.CounteragentId, model => model.MapFrom(s => s.CounteragentId))
                .ForMember(dto => dto.OrderDate, model => model.MapFrom(s => s.OrderDate));

            CreateMap<ProductsForOrderDto, ProductsForOrdersModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.ProductId, dto => dto.MapFrom(s => s.ProductId))
                .ForMember(model => model.ProductOrderId, dto => dto.MapFrom(s => s.ProductOrderId))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count))
                .ForMember(model => model.ProductOrderNumber, dto => dto.MapFrom(s => s.ProductOrder.OrderNumber))
                .ForMember(model => model.ProductName, dto => dto.MapFrom(s => s.Product.Name));

            CreateMap<ProductsForOrdersModel, ProductsForOrderDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.ProductId, model => model.MapFrom(s => s.ProductId))
                .ForMember(dto => dto.ProductOrderId, model => model.MapFrom(s => s.ProductOrderId))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count));

            CreateMap<ProductsReserveDto, ProductsReserveModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.FinishedProductId, dto => dto.MapFrom(s => s.FinishedProductId))
                .ForMember(model => model.StoragePlaceId, dto => dto.MapFrom(s => s.StoragePlaceId))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count))
                .ForMember(model => model.StoragePlaceName, dto => dto.MapFrom(s => s.StoragePlace.Name))
                .ForMember(model => model.FinishedProductName, dto => dto.MapFrom(s => s.FinishedProduct.Product.Name));

            CreateMap<ProductsReserveModel, ProductsReserveDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.FinishedProductId, model => model.MapFrom(s => s.FinishedProductId))
                .ForMember(dto => dto.StoragePlaceId, model => model.MapFrom(s => s.StoragePlaceId))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count));

            CreateMap<FinishedProductDto, FinishedProductModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.ProductId, dto => dto.MapFrom(s => s.ProductId))
                .ForMember(model => model.ManufactureDate, dto => dto.MapFrom(s => s.ManufactureDate))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count))
                .ForMember(model => model.ProductName, dto => dto.MapFrom(s => s.Product.Name));

            CreateMap<FinishedProductModel, FinishedProductDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.ProductId, model => model.MapFrom(s => s.ProductId))
                .ForMember(dto => dto.ManufactureDate, model => model.MapFrom(s => s.ManufactureDate))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count));

            CreateMap<FinishedProductsForOrderDto, FinishedProductsForOrderModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(s => s.Id))
                .ForMember(model => model.ProductsReserveId, dto => dto.MapFrom(s => s.ProductsReserveId))
                .ForMember(model => model.ProductsForOrderId, dto => dto.MapFrom(s => s.ProductsForOrderId))
                .ForMember(model => model.Count, dto => dto.MapFrom(s => s.Count))
                .ForMember(model => model.ProductsForOrderName, dto => dto.MapFrom(s => s.ProductsForOrder.ProductOrder.OrderNumber))
                .ForMember(model => model.ProductsReserveName, dto => dto.MapFrom(s => s.ProductsForOrder.Product.Name));

            CreateMap<FinishedProductsForOrderModel, FinishedProductsForOrderDto>()
                .ForMember(dto => dto.Id, model => model.MapFrom(s => s.Id))
                .ForMember(dto => dto.ProductsReserveId, model => model.MapFrom(s => s.ProductsReserveId))
                .ForMember(dto => dto.ProductsForOrderId, model => model.MapFrom(s => s.ProductsForOrderId))
                .ForMember(dto => dto.Count, model => model.MapFrom(s => s.Count));

            CreateMap<RoleDto, RoleModel>().ReverseMap();
            CreateMap<EmployeeDto, EmployeeModel>().ReverseMap();
            CreateMap<CounteragentDto, CounteragentModel>().ReverseMap();
            CreateMap<MaterialDto, MaterialModel>().ReverseMap();
            CreateMap<ProductDto, ProductModel>().ReverseMap();
            CreateMap<StoragePlaceDto, StoragePlaceModel>().ReverseMap();
        }
    }
}
