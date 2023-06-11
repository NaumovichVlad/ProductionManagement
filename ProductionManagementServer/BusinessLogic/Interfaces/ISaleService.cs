using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface ISaleService : IService<SaleDto>
    {
        List<SaleContainerDto> GetFullSales();
        SaleDto GetByOrderNumber(string orderNumber);
    }
}
