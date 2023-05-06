using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        List<RoleDto> GetList();
        RoleDto GetRoleById(int id);
    }
}