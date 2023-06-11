using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class MaterialsForProductsRepository : Repository<MaterialsForProducts>
    {
        public MaterialsForProductsRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
