using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class FinishedProductsSalesRepository : Repository<FinishedProductSale>
    {
        public FinishedProductsSalesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
