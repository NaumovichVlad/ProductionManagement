using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class SalariesRepository : Repository<Salary>
    {
        public SalariesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
