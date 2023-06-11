using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class MaterialsPurchasesRepository : Repository<MaterialPurchase>
    {
        public MaterialsPurchasesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
