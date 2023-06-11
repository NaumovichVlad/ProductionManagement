using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class EmployeesRepository : Repository<Employee>
    {
        public EmployeesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
