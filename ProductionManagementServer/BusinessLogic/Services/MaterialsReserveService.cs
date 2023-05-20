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
        private readonly IRepository<MaterialOrder> _materialOrderRepository;
        public MaterialsReserveService(IRepository<MaterialReserve> employeeRepository, IRepository<MaterialOrder> orderRepository, IMapper mapper) : base(employeeRepository, mapper)
        {
            _materialOrderRepository = orderRepository;
        }

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

        public List<MaterialReserveDto> GetStorageReserves(int storagePlaceId)
        {
            return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (r.StoragePlaceId == storagePlaceId) && (r.Count > 0), null, "MaterialOrder"));
        }

        public List<MaterialOrderDto> GetPendingReserves()
        {
            var reserves = _repository.Get().Select(r => r.MaterialOrderId);
            return _mapper.Map<List<MaterialOrderDto>>(_materialOrderRepository.Get(o => !reserves.Contains(o.Id)));
        }

        public List<MaterialReserveDto> GetAvailableReservesByMaterialId(int materialId)
        {
            var reserves = _materialOrderRepository.Get(o => o.MaterialId == materialId).Select(o => o.Id);
            return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (reserves.Contains(r.MaterialOrderId)) && (r.Count > 0), null, "MaterialOrder,StoragePlace"));
        }

        public List<MaterialReserveDto> GetConsumptionReservesByMaterialId(int materialId)
        {
            var reserves = _materialOrderRepository.Get(o => o.MaterialId == materialId).Select(o => o.Id);
            return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (reserves.Contains(r.MaterialOrderId)) && (r.Count == 0), null, "MaterialOrder,StoragePlace"));
        }
        
    }
}
