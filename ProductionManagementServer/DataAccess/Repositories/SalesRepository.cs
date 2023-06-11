using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class SalesRepository : Repository<Product>
    {
        public SalesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
