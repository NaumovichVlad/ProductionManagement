using DataAccess.Config;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class UsersRepository : Repository<User>
    {
        public UsersRepository(ProductionManagementDbContext context) : base(context)
        { }
    }
}
