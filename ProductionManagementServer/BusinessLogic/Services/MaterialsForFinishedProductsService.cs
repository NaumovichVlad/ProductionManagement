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
    public class MaterialsForFinishedProductsService : Service<MaterialsForFinishedProducts, MaterialsForFinishedProductsDto>, IMaterialsForFinishedProductsService
    {
        public MaterialsForFinishedProductsService(IRepository<MaterialsForFinishedProducts> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<MaterialsForFinishedProductsDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialsForFinishedProducts);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<MaterialsForFinishedProductsDto>>(_repository.Get(null, null, "MaterialReserve,FinishedProduct")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<MaterialsForFinishedProductsDto>>(_repository.Get(null, null, "MaterialReserve,FinishedProduct")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
