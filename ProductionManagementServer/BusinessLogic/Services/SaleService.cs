using AutoMapper;
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
    public class SaleService : Service<Sale, SaleDto>, ISaleService
    {
        private readonly IRepository<FinishedProductSale> _finishedProductSaleRepository;
        private readonly IRepository<FinishedProduct> _finishedProductRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Counteragent> _counteragentRepository;
        public SaleService(IRepository<Sale> employeeRepository, IMapper mapper,
            IRepository<FinishedProductSale> finishedProductSaleRepository, 
            IRepository<FinishedProduct> finishedProductRepository, IRepository<Product> productRepository, 
            IRepository<Counteragent> counteragentRepository) 
            : base(employeeRepository, mapper)
        {
            _finishedProductSaleRepository = finishedProductSaleRepository;
            _finishedProductRepository = finishedProductRepository;
            _productRepository = productRepository;
            _counteragentRepository = counteragentRepository;
        }

        public new List<SaleDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(Sale);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<SaleDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<SaleDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }

        public List<SaleContainerDto> GetFullSales()
        {
            var materials = _finishedProductSaleRepository.Get(null, null, "ProductsReserve");

            foreach (var material in materials )
            {
                material.ProductsReserve.FinishedProduct = _finishedProductRepository.GetById(material.ProductsReserve.FinishedProductId);
                material.ProductsReserve.FinishedProduct.Product = _productRepository.GetById(material.ProductsReserve.FinishedProduct.ProductId);
            }

            var materialsFull = materials.GroupBy(m => m.SaleId);
            var fullPurchases = new List<SaleContainerDto>();

            foreach (var material in materialsFull)
            {
                var purchase = _repository.GetById(material.Key);
                var mat = material.Select(m => $"{m.ProductsReserve.FinishedProduct.Product.Name}: {m.Count}; ");
                var materialsString = string.Empty;

                foreach (var line in mat)
                {
                    materialsString += line;
                }

                var fullPurchase = new SaleContainerDto()
                {
                    OrderNumber = purchase.OrderNumber,
                    OrderDate = purchase.OrderDate,
                    CounteragentName = _counteragentRepository.GetById(purchase.CounteragentId).Name,
                    Products = materialsString
                };

                fullPurchases.Add(fullPurchase);
            }

            return fullPurchases;
        }

        public SaleDto GetByOrderNumber(string orderNumber)
        {
            return _mapper.Map<SaleDto>(_repository.Get(p => p.OrderNumber == int.Parse(orderNumber)).First());
        }
    }
}
