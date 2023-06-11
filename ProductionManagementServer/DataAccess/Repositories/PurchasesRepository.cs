using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class PurchasesRepository : Repository<Purchase>
    {
        public PurchasesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
