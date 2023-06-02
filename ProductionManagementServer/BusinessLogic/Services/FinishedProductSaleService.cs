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
    public class FinishedProductSaleService : Service<FinishedProductSale, FinishedProductsSaleDto>, IFinishedProductSaleService
    {
        private readonly IRepository<FinishedProduct> _finishedProductRepository;
        private IRepository<Product> _productRepository;
        public FinishedProductSaleService(IRepository<FinishedProductSale> employeeRepository, IMapper mapper, 
            IRepository<FinishedProduct> repository, IRepository<Product> productRepository)
            : base(employeeRepository, mapper)
        {
            _finishedProductRepository = repository;
            _productRepository = productRepository;
        }

        public new List<FinishedProductsSaleDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(FinishedProductSale);
            var dtos = new List<FinishedProductsSaleDto>();
            var sortParameterProperty = type.GetProperty(sortParameter);

            if (sortDirection == "asc")
            {
                dtos = _mapper.Map<List<FinishedProductsSaleDto>>(_repository.Get(null, null, "Sale,ProductsReserve")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                dtos = _mapper.Map<List<FinishedProductsSaleDto>>(_repository.Get(null, null, "Sale,ProductsReserve")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

            foreach (var dto in dtos)
            {
                dto.ProductsReserve.FinishedProduct = _mapper.Map<FinishedProductDto>(_finishedProductRepository.GetById(dto.ProductsReserve.FinishedProductId));
                dto.ProductsReserve.FinishedProduct.Product = _mapper.Map<ProductDto>(_productRepository.GetById(dto.ProductsReserve.FinishedProduct.ProductId));
            }

            return dtos;
        }
    }
}
