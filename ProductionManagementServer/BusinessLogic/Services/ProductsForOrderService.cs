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
    public class ProductsForOrderService : Service<ProductsForOrder, ProductsForOrderDto>, IProductsForOrderService
    {
        public ProductsForOrderService(IRepository<ProductsForOrder> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<ProductsForOrderDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(ProductsForOrder);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<ProductsForOrderDto>>(_repository.Get(null, null, "Product,ProductOrder")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<ProductsForOrderDto>>(_repository.Get(null, null, "Product,ProductOrder")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
