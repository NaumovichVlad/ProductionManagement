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
        private readonly IRepository<MaterialPurchase> _materialPurchaseRepository;
        private readonly IRepository<Material> _materialRepository;
        public MaterialsReserveService(IRepository<MaterialReserve> employeeRepository, IRepository<Purchase> orderRepository,  
            IMapper mapper, IRepository<Material> materialRepository, IRepository<MaterialPurchase> materialPurchaseRepository) 
            : base(employeeRepository, mapper)
        {
            _purchaseRepository = orderRepository;
            _materialRepository = materialRepository;
            _materialPurchaseRepository = materialPurchaseRepository;
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

        public List<MaterialReserveDto> GetStorageReserves(int storagePlaceId)
        {
            var reserves = _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (r.StoragePlaceId == storagePlaceId) && (r.Count > 0), null, "MaterialPurchase"));
            
            foreach (var reserve in reserves) 
            { 
                reserve.MaterialPurchase.Purchase = _mapper.Map<PurchaseDto>(_purchaseRepository.GetById(reserve.MaterialPurchase.PurchaseId));
                reserve.MaterialPurchase.Material = _mapper.Map<MaterialDto>(_materialRepository.GetById(reserve.MaterialPurchase.MaterialId));
            }

            return reserves;
        }

        public List<MaterialPurchaseDto> GetPendingReserves()
        {
            return _mapper.Map<List<MaterialPurchaseDto>>(_materialPurchaseRepository.Get(m => !m.IsAccepted, null, "Purchase,Material"));
        }

        public List<MaterialReserveDto> GetAvailableReservesByMaterialId(int materialId)
        {
            var reserves = _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (r.Count > 0), null, "MaterialPurchase,StoragePlace"));
            reserves = reserves.Where(r => r.MaterialPurchase.MaterialId == materialId).ToList();

            foreach (var reserve in reserves)
            {
                reserve.MaterialPurchase.Purchase = _mapper.Map<PurchaseDto>(_purchaseRepository.GetById(reserve.MaterialPurchase.PurchaseId));
                reserve.MaterialPurchase.Material = _mapper.Map<MaterialDto>(_materialRepository.GetById(reserve.MaterialPurchase.MaterialId));
            }

            return reserves;
        }

        public List<MaterialReserveDto> GetConsumptionReservesByMaterialId(int materialId)
        {
            var reserves = _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (r.Count == 0), null, "MaterialPurchase,StoragePlace"));
            
            reserves = reserves.Where(r => r.MaterialPurchase.MaterialId == materialId).ToList();

            foreach (var reserve in reserves)
            {
                reserve.MaterialPurchase.Purchase = _mapper.Map<PurchaseDto>(_purchaseRepository.GetById(reserve.MaterialPurchase.PurchaseId));
                reserve.MaterialPurchase.Material = _mapper.Map<MaterialDto>(_materialRepository.GetById(reserve.MaterialPurchase.MaterialId));
            }

            return reserves;
        }
        
    }
}
