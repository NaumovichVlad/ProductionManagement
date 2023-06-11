using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class EmployeeService : Service<Employee, EmployeeDto>, IEmployeeService
    {
        public EmployeeService(IRepository<Employee> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
