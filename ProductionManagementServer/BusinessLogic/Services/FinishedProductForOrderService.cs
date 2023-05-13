using AutoMapper;
using BusinessLogic.Dtos;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class FinishedProductForOrderService : Service<FinishedProductsForOrder, FinishedProductsForOrderDto>, IFinishedProductForOrderService
    {
        public FinishedProductForOrderService(IRepository<FinishedProductsForOrder> employeeRepository, IMapper mapper)
            : base(employeeRepository, mapper)
        { }

        public new List<FinishedProductsForOrderDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(FinishedProductsForOrder);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<FinishedProductsForOrderDto>>(_repository.Get(null, null, "ProductsForOrder,ProductsReserve")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<FinishedProductsForOrderDto>>(_repository.Get(null, null, "ProductsForOrder,ProductsReserve")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
