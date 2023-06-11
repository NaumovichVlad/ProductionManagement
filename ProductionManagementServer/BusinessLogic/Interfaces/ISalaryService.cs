using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface ISalaryService : IService<SalaryDto>
    {
        List<SalaryDto> GetByEmployee(int employeeId);
        List<SalaryDto> GetByYear(int year);
        List<SalaryDto> GetByEmployeeAndMonth(int employeeId, DateTime date);
    }
}
