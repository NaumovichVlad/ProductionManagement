using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IProductsReserveService : IService<ProductsReserveDto>
    {
        List<ProductsReserveDto> GetStorageReserves(int storagePlaceId);
        List<FinishedProductDto> GetPendingReserves();
        List<AvailableProductDto> GetAvailableProducts();
    }
}
