using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class RolesRepository : Repository<Role>
    {
        public RolesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
