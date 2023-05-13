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
    public class MaterialsForProductsService : Service<MaterialsForProducts, MaterialsForProductsDto>, IMaterialsForProductsService
    {
        public MaterialsForProductsService(IRepository<MaterialsForProducts> employeeRepository, IMapper mapper) 
            : base(employeeRepository, mapper)
        { }
        public new List<MaterialsForProductsDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialsForProducts);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<MaterialsForProductsDto>>(_repository.Get(null, null, "Material,Product")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<MaterialsForProductsDto>>(_repository.Get(null, null, "Material,Product")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
