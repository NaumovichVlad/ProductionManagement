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
    public class PurchaseService : Service<Purchase, PurchaseDto>, IPurchaseService
    {
        public PurchaseService(IRepository<Purchase> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<PurchaseDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(Purchase);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<PurchaseDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<PurchaseDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
