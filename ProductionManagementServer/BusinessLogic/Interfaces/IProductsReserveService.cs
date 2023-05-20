using BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IProductsReserveService : IService<ProductsReserveDto>
    {
        List<ProductsReserveDto> GetStorageReserves(int storagePlaceId);
        List<FinishedProductDto> GetPendingReserves();
    }
}
