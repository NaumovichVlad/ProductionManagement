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
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductsReserve> _productsReserveRepository;
        public FinishedProductSaleService(IRepository<FinishedProductSale> employeeRepository, IMapper mapper, 
            IRepository<FinishedProduct> repository, IRepository<Product> productRepository, IRepository<ProductsReserve> productReserveRepository)
            : base(employeeRepository, mapper)
        {
            _productsReserveRepository = productReserveRepository;
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

        public bool InsertByName(string productName, int count, int saleId)
        {
            var reserves = _productsReserveRepository.Get(r => r.Count > 0, null, "FinishedProduct").ToList();

            for (var i = 0; i < reserves.Count; i++)
            {
                if (_productRepository.GetById(reserves[i].FinishedProduct.ProductId).Name != productName)
                {
                    reserves.RemoveAt(i);
                    i--;
                }
            }

            if (reserves.Any())
            {
                return InsertAndRemoveFromReserves(reserves, count, saleId);
            }
            else
            {
                return false;
            }
        }

        private bool InsertAndRemoveFromReserves(List<ProductsReserve> reserves, int count, int saleId)
        {
            if (reserves.Sum(r => r.Count) >= count)
            {
                for (var i = 0; i < reserves.Count; i++)
                {
                    if (reserves[i].Count - count >= 0)
                    {
                        reserves[i].Count -= count;

                        _productsReserveRepository.Update(reserves[i]);

                        _repository.Insert(new FinishedProductSale()
                        {
                            SaleId = saleId,
                            Count = count,
                            ProductsReserveId = reserves[i].Id
                        });

                        break;
                    }
                    else
                    {
                        count -= reserves[i].Count;

                        _repository.Insert(new FinishedProductSale()
                        {
                            SaleId = saleId,
                            Count = reserves[i].Count,
                            ProductsReserveId = reserves[i].Id
                        });

                        reserves[i].Count = 0;

                        _productsReserveRepository.Update(reserves[i]);
                    }
                }

                return true;
            }
            else
            {
                return false;
            }    
        }
    }
}
