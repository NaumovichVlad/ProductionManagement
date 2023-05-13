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
        }
    }
}
