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
    public class FinishedProductSaleService : Service<FinishedProductSale, FinishedProductsSaleDto>, IFinishedProductSaleService
    {
        public FinishedProductSaleService(IRepository<FinishedProductSale> employeeRepository, IMapper mapper)
            : base(employeeRepository, mapper)
        { }

        public new List<FinishedProductsSaleDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(FinishedProductSale);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<FinishedProductsSaleDto>>(_repository.Get(null, null, "Sale,ProductsReserve")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<FinishedProductsSaleDto>>(_repository.Get(null, null, "Sale,ProductsReserve")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
