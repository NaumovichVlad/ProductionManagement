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
    public class ProductsReserveService : Service<ProductsReserve, ProductsReserveDto>, IProductsReserveService
    {
        public ProductsReserveService(IRepository<ProductsReserve> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<UserDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(User);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<UserDto>>(_repository.Get(null, null, "FinishedProduct,StoragePlace")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<UserDto>>(_repository.Get(null, null, "FinishedProduct,StoragePlace")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

        }
    }
}
