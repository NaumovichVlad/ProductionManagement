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
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IRepository<Material> _materialRepository;
        public MaterialsReserveService(IRepository<MaterialReserve> employeeRepository, IRepository<Purchase> orderRepository,  IMapper mapper, IRepository<Material> materialRepository) : base(employeeRepository, mapper)
        {
            _purchaseRepository = orderRepository;
            _materialRepository = materialRepository;
        }

        public new List<MaterialReserveDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialReserve);
            var dtos = new List<MaterialReserveDto>();
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                dtos = _mapper.Map<List<MaterialReserveDto>>(_repository.Get(null, null, "MaterialPurchase,StoragePlace")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size));
            }
            else
            {
                dtos =  _mapper.Map<List<MaterialReserveDto>>(_repository.Get(null, null, "MaterialPurchase,StoragePlace")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

            foreach (var dto in dtos)
            {
                dto.MaterialPurchase.Purchase = _mapper.Map<PurchaseDto>(_purchaseRepository.GetById(dto.MaterialPurchase.PurchaseId));
                dto.MaterialPurchase.Material = _mapper.Map<MaterialDto>(_materialRepository.GetById(dto.MaterialPurchase.MaterialId));
            }

            return dtos;
        }

        /*public List<MaterialReserveDto> GetStorageReserves(int storagePlaceId)
        {
            return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (r.StoragePlaceId == storagePlaceId) && (r.Count > 0), null, "MaterialPurchase"));
        }

        public List<PurchaseDto> GetPendingReserves()
        {
            var reserves = _repository.Get().Select(r => r.MaterialOrderId);
            return _mapper.Map<List<PurchaseDto>>(_materialOrderRepository.Get(o => !reserves.Contains(o.Id)));
        }

        public List<MaterialReserveDto> GetAvailableReservesByMaterialId(int materialId)
        {
            var reserves = _materialOrderRepository.Get(o => o. == materialId).Select(o => o.Id);
            return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (reserves.Contains(r.MaterialOrderId)) && (r.Count > 0), null, "MaterialPurchase,StoragePlace"));
        }

        public List<MaterialReserveDto> GetConsumptionReservesByMaterialId(int materialId)
        {
            var reserves = _materialOrderRepository.Get(o => o.MaterialId == materialId).Select(o => o.Id);
            return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (reserves.Contains(r.MaterialOrderId)) && (r.Count == 0), null, "MaterialPurchase,StoragePlace"));
        }*/
        
    }
}
