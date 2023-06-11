using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class FinishedProductsRepository : Repository<FinishedProduct>
    {
        public FinishedProductsRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
