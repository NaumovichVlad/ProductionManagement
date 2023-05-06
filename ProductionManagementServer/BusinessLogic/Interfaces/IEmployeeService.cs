using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetList();
    }
}