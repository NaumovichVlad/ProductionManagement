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
    public class MaterialsReserveService : Service<MaterialReserve, MaterialReserveDto>, IMaterialsReserveService
    {
        public MaterialsReserveService(IRepository<MaterialReserve> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<MaterialReserveDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialReserve);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(null, null, "MaterialOrder,StoragePlace")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(null, null, "MaterialOrder,StoragePlace")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }
    }
}
