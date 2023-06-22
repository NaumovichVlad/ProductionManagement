using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class MaterialsForFinishedProductsService : Service<MaterialsForFinishedProducts, MaterialsForFinishedProductsDto>, IMaterialsForFinishedProductsService
    {
        private readonly IRepository<MaterialReserve> _materialReserveRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<MaterialPurchase> _materialPurchaseRepository;
        private readonly IRepository<Product> _productRepository;
        public MaterialsForFinishedProductsService(IRepository<MaterialsForFinishedProducts> employeeRepository, IRepository<Material> materialRepository, 
            IMapper mapper, IRepository<MaterialReserve> materialReserveRepository, 
            IRepository<MaterialPurchase> materialPurchaseRepository, IRepository<Product> productRepository) : base(employeeRepository, mapper)
        {
            _materialRepository = materialRepository;
            _materialReserveRepository = materialReserveRepository;
            _materialPurchaseRepository = materialPurchaseRepository;
            _productRepository = productRepository;
        }

        public new List<MaterialsForFinishedProductsDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialsForFinishedProducts);
            var dtos = new List<MaterialsForFinishedProductsDto>();
            var sortParameterProperty = type.GetProperty(sortParameter);

            if (sortDirection == "asc")
            {
                dtos = _mapper.Map<List<MaterialsForFinishedProductsDto>>(_repository.Get(null, null, "Product")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                dtos = _mapper.Map<List<MaterialsForFinishedProductsDto>>(_repository.Get(null, null, "Product")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

            foreach (var dto in dtos)
            {
                dto.MaterialReserve = _mapper.Map<MaterialReserveDto>(_materialReserveRepository.GetById(dto.MaterialReserveId));
                dto.MaterialReserve.MaterialPurchase = _mapper.Map<MaterialPurchaseDto>(_materialPurchaseRepository.GetById(dto.MaterialReserve.MaterialPurchaseId));
                dto.MaterialReserve.MaterialPurchase.Material = _mapper.Map<MaterialDto>(_materialRepository.GetById(dto.MaterialReserve.MaterialPurchase.MaterialId));
                dto.Product.Product = _mapper.Map<ProductDto>(_productRepository.GetById(dto.Product.ProductId));
            }

            return dtos;
        }
    }
}
