using DataAccess.Config;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductsReservesRepository : Repository<ProductsReserve>
    {
        public ProductsReservesRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
