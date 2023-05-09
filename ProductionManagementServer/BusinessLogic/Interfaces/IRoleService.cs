using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        List<RoleDto> GetList();
        RoleDto GetRoleById(int id);
        List<RoleDto> GetSelection(int start, int size, string sortDirection, string sortParameter);
    }
}