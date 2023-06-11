using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class StoragePlacesRepository : Repository<StoragePlace>
    {
        public StoragePlacesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
