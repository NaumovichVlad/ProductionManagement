using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class MaterialReserveDto
    {
        public int Id { get; set; }
        public int StoragePlaceId { get; set; }
        public int MaterialOrderId { get; set; }
        public int Count { get; set; }
        public StoragePlaceDto StoragePlace { get; set; }
        public MaterialOrderDto MaterialOrder { get; set; }
    }
}
