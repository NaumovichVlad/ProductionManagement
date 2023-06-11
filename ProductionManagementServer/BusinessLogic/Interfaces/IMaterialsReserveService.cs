using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IMaterialsReserveService : IService<MaterialReserveDto>
    {
        List<MaterialReserveDto> GetStorageReserves(int storagePlaceId);
        List<MaterialPurchaseDto> GetPendingReserves();
        List<MaterialReserveDto> GetAvailableReservesByMaterialId(int materialId);
        List<MaterialReserveDto> GetConsumptionReservesByMaterialId(int materialId);
        List<AvailableMaterialDto> GetAvailableMaterials();
    }
}
