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
    public class SalaryService : ISalaryService
    {
        private readonly IRepository<Salary> _salaryRepository;
        private readonly IMapper _mapper;

        public SalaryService(IRepository<Salary> salaryRepository, IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
        }

        public List<SalaryDto> GetList()
        {
            return _mapper.Map<List<SalaryDto>>(_salaryRepository.GetWithInclude(s => s.Employee).ToList());
        }

        public List<SalaryDto> GetByEmployee(int employeeId)
        {
            return _mapper.Map<List<SalaryDto>>(_salaryRepository.Get(s => s.EmployeeId == employeeId));
        }

        public List<SalaryDto> GetByDate(DateTime startDate, DateTime endDate, Employee employee = null)
        {
            if (employee != null)
            {
                return _mapper.Map<List<SalaryDto>>(_salaryRepository.Get(
                    s => s.EmployeeId == employee.Id && s.PaymentDate >= startDate && s.PaymentDate <= endDate));
            }
            else
            {
                return _mapper.Map<List<SalaryDto>>(_salaryRepository.Get(
                    s => s.PaymentDate >= startDate && s.PaymentDate <= endDate));
            }
        }
    }
}
