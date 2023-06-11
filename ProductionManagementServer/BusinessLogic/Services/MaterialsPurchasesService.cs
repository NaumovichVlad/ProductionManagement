using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class MaterialsPurchasesService : Service<MaterialPurchase, MaterialPurchaseDto>, IMaterialsPurchasesService
    {
        public MaterialsPurchasesService(IRepository<MaterialPurchase> employeeRepository, IMapper mapper)
            : base(employeeRepository, mapper)
        { }

        public new List<MaterialPurchaseDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(MaterialPurchase);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<MaterialPurchaseDto>>(_repository.Get(null, null, "Purchase,Material")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<MaterialPurchaseDto>>(_repository.Get(null, null, "Purchase,Material")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }

        public List<MaterialPurchaseDto> GetNotAccepted()
        {
            return _mapper.Map<List<MaterialPurchaseDto>>(_repository.Get(m => m.IsAccepted == false, null, "Purchase,Material"));
        }
    }
}
