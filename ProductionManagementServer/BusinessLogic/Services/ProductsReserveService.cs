﻿using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ProductsReserveService : Service<ProductsReserve, ProductsReserveDto>, IProductsReserveService
    {
        private IRepository<FinishedProduct> _finishedProductRepository;
        private IRepository<Product> _productRepository;
        public ProductsReserveService(IRepository<ProductsReserve> employeeRepository, IMapper mapper, IRepository<FinishedProduct> finishedProductRepository, IRepository<Product> productRepository) : base(employeeRepository, mapper)
        {
            _finishedProductRepository = finishedProductRepository;
            _productRepository = productRepository;
        }

        public new List<ProductsReserveDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(ProductsReserve);
            var dtos = new List<ProductsReserveDto>();
            var sortParameterProperty = type.GetProperty(sortParameter);

            if (sortDirection == "asc")
            {
                dtos = _mapper.Map<List<ProductsReserveDto>>(_repository.Get(null, null, "FinishedProduct,StoragePlace")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                dtos = _mapper.Map<List<ProductsReserveDto>>(_repository.Get(null, null, "FinishedProduct,StoragePlace")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

            foreach (var dto in dtos)
            {
                dto.FinishedProduct.Product = _mapper.Map<ProductDto>(_productRepository.GetById(dto.FinishedProduct.ProductId));
            }

            return dtos;
        }

        public List<ProductsReserveDto> GetList()
        {
            var dtos = _mapper.Map<List<ProductsReserveDto>>(_repository.Get(null, null, "FinishedProduct")).ToList();

            foreach (var dto in dtos)
            {
                dto.FinishedProduct.Product = _mapper.Map<ProductDto>(_productRepository.GetById(dto.FinishedProduct.ProductId));
            }

            return dtos;
        }

        public List<ProductsReserveDto> GetStorageReserves(int storagePlaceId)
        {
            var reserves = _mapper.Map<List<ProductsReserveDto>>(_repository.Get(r => (r.StoragePlaceId == storagePlaceId) && (r.Count > 0), null, "FinishedProduct"));

            foreach (var reserve in reserves)
            {
                reserve.FinishedProduct.Product = _mapper.Map<ProductDto>(_productRepository.GetById(reserve.FinishedProduct.ProductId));
            }

            return reserves;
        }

        public List<FinishedProductDto> GetPendingReserves()
        {
            return _mapper.Map<List<FinishedProductDto>>(_finishedProductRepository.Get(o => !o.IsApproved, null, "Product"));
        }

        public List<AvailableProductDto> GetAvailableProducts()
        {
            var reserves = _mapper.Map<List<ProductsReserveDto>>(_repository.Get(r => (r.Count > 0), null, "FinishedProduct")).GroupBy(r => r.FinishedProduct.ProductId);
            var materialas = new List<AvailableProductDto>();

            foreach (var reserve in reserves)
            {
                var finishedProduct = _mapper.Map<FinishedProductDto>(_finishedProductRepository.GetById(reserve.Key));

                materialas.Add(new AvailableProductDto
                {
                    Name = _mapper.Map<ProductDto>(_productRepository.GetById(finishedProduct.ProductId)).Name,
                    Count = reserve.Sum(r => r.Count),
                });
            }

            return materialas;
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
