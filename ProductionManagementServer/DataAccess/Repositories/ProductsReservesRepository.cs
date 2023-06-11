using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class ProductsReservesRepository : Repository<ProductsReserve>
    {
        public ProductsReservesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
