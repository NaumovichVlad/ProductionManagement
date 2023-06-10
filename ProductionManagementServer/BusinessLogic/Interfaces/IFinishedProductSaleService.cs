using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IFinishedProductSaleService : IService<FinishedProductsSaleDto>
    {
        bool InsertByName(string productName, int count, int saleId);
    }
}