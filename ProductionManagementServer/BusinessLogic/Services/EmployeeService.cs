using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmployeeService : Service<Employee, EmployeeDto>, IEmployeeService
    {
        public EmployeeService(IRepository<Employee> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
