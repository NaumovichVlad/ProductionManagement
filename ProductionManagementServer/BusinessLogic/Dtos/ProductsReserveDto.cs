using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class ProductsReserveDto
    {
        public int Id { get; set; }
        public int StoragePlaceId { get; set; }
        public int FinishedProductId { get; set; }
        public int Count { get; set; }
        public StoragePlaceDto StoragePlace { get; set; }
        public FinishedProductDto FinishedProduct { get; set; }
    }
}
