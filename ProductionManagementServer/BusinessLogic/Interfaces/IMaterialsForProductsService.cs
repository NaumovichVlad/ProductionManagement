using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IMaterialsForProductsService : IService<MaterialsForProductsDto>
    {
        List<MaterialsForProductsDto> GetMaterialsByProductId(int productId);
    }
}
