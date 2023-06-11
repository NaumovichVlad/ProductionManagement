using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class PurchaseService : Service<Purchase, PurchaseDto>, IPurchaseService
    {
        private readonly IRepository<MaterialPurchase> _materialsPurchaseRepository;
        private readonly IRepository<Counteragent> _counteragentRepository;
        public PurchaseService(IRepository<Purchase> employeeRepository, IMapper mapper,
            IRepository<MaterialPurchase> materialPurchaseRepository, IRepository<Counteragent> counteragentRepository) : base(employeeRepository, mapper)
        {
            _materialsPurchaseRepository = materialPurchaseRepository;
            _counteragentRepository = counteragentRepository;
        }

        public new List<PurchaseDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(Purchase);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<PurchaseDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<PurchaseDto>>(_repository.Get(null, null, "Counteragent")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }

        public List<PurchaseContainerDto> GetFullPurchases()
        {
            var materials = _materialsPurchaseRepository.Get(null, null, "Material").GroupBy(m => m.PurchaseId);
            var fullPurchases = new List<PurchaseContainerDto>();

            foreach (var material in materials)
            {
                var purchase = _repository.GetById(material.Key);
                var mat = material.Select(m => $"{m.Material.Name}: {m.Count}; ");
                var materialsString = string.Empty;

                foreach (var line in mat)
                {
                    materialsString += line;
                }

                var fullPurchase = new PurchaseContainerDto()
                {
                    OrderNumber = purchase.OrderNumber,
                    ManufactureCountry = purchase.ManufactureCountry,
                    ManufactureDate = purchase.ManufactureDate,
                    CounteragentName = _counteragentRepository.GetById(purchase.CounteragentId).Name,
                    Materials = materialsString
                };

                fullPurchases.Add(fullPurchase);
            }

            return fullPurchases;
        }

        public PurchaseDto GetByOrderNumber(string orderNumber)
        {
            return _mapper.Map<PurchaseDto>(_repository.Get(p => p.OrderNumber == orderNumber).First());
        }
    }
}
