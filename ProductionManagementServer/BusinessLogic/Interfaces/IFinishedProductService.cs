using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IFinishedProductService : IService<FinishedProductDto>
    {
        List<FinishedProductDto> GetNotAccepted();
        bool CreateFinishedProducts(int productId, int productCount);
    }
}
