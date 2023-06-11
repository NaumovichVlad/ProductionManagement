using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class RoleService : Service<Role, RoleDto>, IRoleService
    {
        public RoleService(IRepository<Role> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
