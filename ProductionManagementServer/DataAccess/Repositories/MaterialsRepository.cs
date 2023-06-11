using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class MaterialsRepository : Repository<Material>
    {
        public MaterialsRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
