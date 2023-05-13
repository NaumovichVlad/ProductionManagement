using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.DI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            service.AddScoped<IFinishedProductForOrderService, FinishedProductForOrderService>();
            service.AddScoped<IFinishedProductService, FinishedProductService>();
            service.AddScoped<IMaterialOrderService, MaterialOrderService>();
            service.AddScoped<IMaterialService, MaterialService>();
            service.AddScoped<IMaterialsForFinishedProductsService, MaterialsForFinishedProductsService>();
            service.AddScoped<IMaterialsForProductsService, MaterialsForProductsService>();
            service.AddScoped<IMaterialsReserveService, MaterialsReserveService>();
            service.AddScoped<IProductOrderService, ProductOrderService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IProductsForOrderService, ProductsForOrderService>();
            service.AddScoped<IProductsReserveService, ProductsReserveService>();
            service.AddScoped<IStoragePlaceService, StoragePlaceService>();
        }
    }
}
