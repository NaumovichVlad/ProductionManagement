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
    public class FinishedProductService : Service<FinishedProduct, FinishedProductDto>, IFinishedProductService
    {
        public FinishedProductService(IRepository<FinishedProduct> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<FinishedProductDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(FinishedProduct);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<FinishedProductDto>>(_repository.Get(null, null, "Product")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<FinishedProductDto>>(_repository.Get(null, null, "Product")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
