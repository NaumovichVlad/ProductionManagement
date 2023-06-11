using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class CounteragentsRepository : Repository<Counteragent>
    {
        public CounteragentsRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
