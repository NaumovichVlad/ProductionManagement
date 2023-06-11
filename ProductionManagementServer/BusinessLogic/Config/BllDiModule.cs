using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.DI;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Config
{
    public static class BllDiModule
    {
        public static void ConfigurateBll(this IServiceCollection service, string connectionString)
        {
            service.ConfigurateDal(connectionString);
            service.AddScoped<ISalaryService, SalaryService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IEmployeeService, EmployeeService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<ICounteragentService, CounteragentService>();
            service.AddScoped<IFinishedProductSaleService, FinishedProductSaleService>();
            service.AddScoped<IFinishedProductService, FinishedProductService>();
            service.AddScoped<IPurchaseService, PurchaseService>();
            service.AddScoped<IMaterialService, MaterialService>();
            service.AddScoped<IMaterialsForFinishedProductsService, MaterialsForFinishedProductsService>();
            service.AddScoped<IMaterialsForProductsService, MaterialsForProductsService>();
            service.AddScoped<IMaterialsReserveService, MaterialsReserveService>();
            service.AddScoped<ISaleService, SaleService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IProductsReserveService, ProductsReserveService>();
            service.AddScoped<IStoragePlaceService, StoragePlaceService>();
            service.AddScoped<IMaterialsPurchasesService, MaterialsPurchasesService>();
        }
    }
}
