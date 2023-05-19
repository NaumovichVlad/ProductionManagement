using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SalaryService : Service<Salary, SalaryDto>, ISalaryService
    {
        public SalaryService(IRepository<Salary> employeeRepository, IMapper mapper) 
            : base(employeeRepository, mapper)
        { }

        public new List<SalaryDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(Salary);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<SalaryDto>>(_repository.Get(null, l => l.OrderBy(r => sortParameterProperty.GetValue(r)), "Employee")
                    .Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<SalaryDto>>(_repository.Get(null, l => l.OrderByDescending(r => sortParameterProperty.GetValue(r)), "Employee")
                    .Skip(start).Take(size).ToList());
            }

        }

        public List<SalaryDto> GetByEmployee(int employeeId) 
        {
            return _mapper.Map<List<SalaryDto>>(
                _repository.Get(s => s.EmployeeId == employeeId, l => l.OrderByDescending(s => s.PaymentDate), "Employee"));
        }

        public List<SalaryDto> GetByYear(int year)
        {
            return _mapper.Map<List<SalaryDto>>(
                _repository.Get(s => s.PaymentDate.Year == year, l => l.OrderBy(s => s.EmployeeId), "Employee"));
        }

        public List<SalaryDto> GetByEmployeeAndMonth(int employeeId, DateTime date)
        {
            return _mapper.Map<List<SalaryDto>>(
                _repository.Get(s => (s.EmployeeId == employeeId) && (s.PaymentDate.Month == date.Month) && (s.PaymentDate.Year == date.Year), 
                l => l.OrderByDescending(s => s.PaymentDate), "Employee"));
        }
    }
}
