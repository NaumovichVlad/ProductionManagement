using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IMaterialsPurchasesService : IService<MaterialPurchaseDto>
    {
        List<MaterialPurchaseDto> GetNotAccepted();
    }
}
