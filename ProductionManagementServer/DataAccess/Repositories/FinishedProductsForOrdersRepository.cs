using DataAccess.Config;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class FinishedProductsForOrdersRepository : Repository<FinishedProductsForOrder>
    {
        public FinishedProductsForOrdersRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
