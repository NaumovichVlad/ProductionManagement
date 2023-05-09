using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetList();
        List<EmployeeDto> GetSelection(int start, int size, string sortDirection, string sortParameter);
    }
}