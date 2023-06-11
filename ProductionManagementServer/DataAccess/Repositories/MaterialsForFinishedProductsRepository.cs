using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class MaterialsForFinishedProductsRepository : Repository<MaterialsForFinishedProducts>
    {
        public MaterialsForFinishedProductsRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
