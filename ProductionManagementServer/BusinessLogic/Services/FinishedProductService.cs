using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class FinishedProductService : Service<FinishedProduct, FinishedProductDto>, IFinishedProductService
    {
        private readonly IRepository<MaterialsForProducts> _materialsForProductsRepository;
        private readonly IRepository<MaterialReserve> _materialsReserveRepository;
        private readonly IRepository<MaterialsForFinishedProducts> _materialsFinishedProductsRepository;
        public FinishedProductService(IRepository<FinishedProduct> employeeRepository, IMapper mapper,
            IRepository<MaterialsForProducts> materialsForProductsRepository, IRepository<MaterialReserve> materialsReserveRepository,
            IRepository<MaterialsForFinishedProducts> materialsForFinishedProductsRepository)
            : base(employeeRepository, mapper)
        {
            _materialsForProductsRepository = materialsForProductsRepository;
            _materialsReserveRepository = materialsReserveRepository;
            _materialsFinishedProductsRepository = materialsForFinishedProductsRepository;
        }

        public new List<FinishedProductDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(FinishedProduct);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<FinishedProductDto>>(_repository.Get(null, null, "Product")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<FinishedProductDto>>(_repository.Get(null, null, "Product")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }

        public List<FinishedProductDto> GetNotAccepted()
        {
            return _mapper.Map<List<FinishedProductDto>>(_repository.Get(m => m.IsApproved == false, null, "Product"));
        }

        public bool CreateFinishedProducts(int productId, int productCount)
        {
            var materialsForProducts =
                _mapper.Map<List<MaterialsForProductsDto>>(_materialsForProductsRepository.Get(m => m.ProductId == productId, null, "Material,Product"));

            if (CheckAvailableMaterials(materialsForProducts, productCount))
            {
                var date = DateTime.Now;

                _repository.Insert(new FinishedProduct()
                {
                    ProductId = productId,
                    Count = productCount,
                    IsApproved = false,
                    ManufactureDate = date,
                });

                var finishedProduct = _repository.Get(p => (p.ProductId == productId) && p.ManufactureDate == date).First();

                foreach (var material in materialsForProducts)
                {
                    var reserves = _materialsReserveRepository.Get(m => m.Count > 0, null, "MaterialPurchase").Where(m => m.MaterialPurchase.MaterialId == material.Id);
                    var count = material.Count * productCount;

                    foreach (var reserve in reserves)
                    {
                        if (reserve.Count >= count)
                        {
                            _materialsFinishedProductsRepository.Insert(new MaterialsForFinishedProducts()
                            {
                                MaterialReserveId = reserve.Id,
                                ProductId = finishedProduct.Id,
                                Count = count
                            });

                            reserve.Count -= count;

                            _materialsReserveRepository.Update(reserve);

                            break;
                        }
                        else
                        {
                            _materialsFinishedProductsRepository.Insert(new MaterialsForFinishedProducts()
                            {
                                MaterialReserveId = reserve.Id,
                                ProductId = finishedProduct.Id,
                                Count = reserve.Count
                            });

                            count -= reserve.Count;
                            reserve.Count = 0;

                            _materialsReserveRepository.Update(reserve);
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckAvailableMaterials(List<MaterialsForProductsDto> materialsForProducts, int productCount)
        {
            foreach (var material in materialsForProducts)
            {
                var count = _materialsReserveRepository.Get(m => m.Count > 0, null, "MaterialPurchase")
                    .Where(m => m.MaterialPurchase.MaterialId == material.Id).Sum(m => m.Count);

                if (count < material.Count * productCount)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
