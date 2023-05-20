﻿using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductsReserveService : Service<ProductsReserve, ProductsReserveDto>, IProductsReserveService
    {
        private IRepository<FinishedProduct> _finishedProductRepository;
        public ProductsReserveService(IRepository<ProductsReserve> employeeRepository, IMapper mapper, IRepository<FinishedProduct> finishedProductRepository) : base(employeeRepository, mapper)
        {
            _finishedProductRepository = finishedProductRepository;
        }

        public new List<ProductsReserveDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(ProductsReserve);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<ProductsReserveDto>>(_repository.Get(null, null, "FinishedProduct,StoragePlace")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<ProductsReserveDto>>(_repository.Get(null, null, "FinishedProduct,StoragePlace")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

        }

        public List<ProductsReserveDto> GetStorageReserves(int storagePlaceId)
        {
            return _mapper.Map<List<ProductsReserveDto>>(_repository.Get(r => (r.StoragePlaceId == storagePlaceId) && (r.Count > 0), null, "FinishedProduct"));
        }

        public List<FinishedProductDto> GetPendingReserves()
        {
            var reserves = _repository.Get().Select(r => r.FinishedProductId);
            return _mapper.Map<List<FinishedProductDto>>(_finishedProductRepository.Get(o => !reserves.Contains(o.Id)));
        }

        /*public List<ProductsReserveDto> GetAvailableReservesByMaterialId(int productId)
        {
            var reserves = _finishedProductRepository.Get(o => o.ProductId == productId).Select(o => o.Id);
            return _mapper.Map<List<ProductsReserveDto>>(_repository.Get(r => (reserves.Contains(r.MaterialOrderId)) && (r.Count > 0), null, "MaterialOrder,StoragePlace"));
        }

        public List<MaterialReserveDto> GetConsumptionReservesByMaterialId(int materialId)
        {
            var reserves = _materialOrderRepository.Get(o => o.MaterialId == materialId).Select(o => o.Id);
            return _mapper.Map<List<MaterialReserveDto>>(_repository.Get(r => (reserves.Contains(r.MaterialOrderId)) && (r.Count == 0), null, "MaterialOrder,StoragePlace"));
        }*/
    }
}
