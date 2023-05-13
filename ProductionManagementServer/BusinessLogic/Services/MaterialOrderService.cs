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
    public class MaterialOrderService : Service<MaterialOrder, MaterialOrderDto>, IMaterialOrderService
    {
        public MaterialOrderService(IRepository<MaterialOrder> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<MaterialOrderDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialOrder);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<MaterialOrderDto>>(_repository.Get(null, null, "Material, Counteragent")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<MaterialOrderDto>>(_repository.Get(null, null, "Material, Counteragent")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
