using DataAccess.Config;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DI
{
    public static class DalDiModule
    {
        public static void ConfigurateDal(this IServiceCollection builder, string connectionString)
        {
            builder.AddDbContext<ProductionManagementDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 25))));

            builder.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.AddScoped<DbContext, ProductionManagementDbContext>();
        }
    }
}
