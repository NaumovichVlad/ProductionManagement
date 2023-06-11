using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IPurchaseService : IService<PurchaseDto>
    {
        List<PurchaseContainerDto> GetFullPurchases();
        PurchaseDto GetByOrderNumber(string orderNumber);
    }
}
