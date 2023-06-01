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
    public class SaleService : Service<Sale, SaleDto>, ISaleService
    {
        public SaleService(IRepository<Sale> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<SaleDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(Sale);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<SaleDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<SaleDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
