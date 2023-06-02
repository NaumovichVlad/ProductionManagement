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
        private readonly IRepository<MaterialReserve> _materialReserveRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<MaterialPurchase> _materialPurchaseRepository;
        public MaterialsForFinishedProductsService(IRepository<MaterialsForFinishedProducts> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public new List<MaterialsForFinishedProductsDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialsForFinishedProducts);
            var dtos = new List<MaterialsForFinishedProductsDto>();
            var sortParameterProperty = type.GetProperty(sortParameter);

            if (sortDirection == "asc")
            {
                dtos = _mapper.Map<List<MaterialsForFinishedProductsDto>>(_repository.Get(null, null, "Material,Product")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                dtos =  _mapper.Map<List<MaterialsForFinishedProductsDto>>(_repository.Get(null, null, "Material,Product")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

            foreach (var dto in dtos)
            {
                dto.MaterialReserve = _mapper.Map<MaterialReserveDto>(_materialReserveRepository.GetById(dto.MaterialReserveId));
                dto.MaterialReserve.MaterialPurchase = _mapper.Map<MaterialPurchaseDto>(_materialPurchaseRepository.GetById(dto.MaterialReserve.MaterialPurchaseId));
                dto.MaterialReserve.MaterialPurchase.Material = _mapper.Map<MaterialDto>(_materialRepository.GetById(dto.MaterialReserve.MaterialPurchase.MaterialId));
            }

            return dtos;
        }
    }
}
