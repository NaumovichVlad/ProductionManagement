using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class MaterialReservesRepository : Repository<MaterialReserve>
    {
        public MaterialReservesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
