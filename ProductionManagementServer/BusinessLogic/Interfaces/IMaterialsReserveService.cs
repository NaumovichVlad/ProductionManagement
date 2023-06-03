using BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMaterialsReserveService : IService<MaterialReserveDto>
    {
        List<MaterialReserveDto> GetStorageReserves(int storagePlaceId);
        List<MaterialPurchaseDto> GetPendingReserves();
        List<MaterialReserveDto> GetAvailableReservesByMaterialId(int materialId);
        List<MaterialReserveDto> GetConsumptionReservesByMaterialId(int materialId);
    }
}
