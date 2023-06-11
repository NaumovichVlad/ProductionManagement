using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class ProductsRepository : Repository<Product>
    {
        public ProductsRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
