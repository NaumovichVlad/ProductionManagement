using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class FinishedProductsForOrderDto
    {
        public int Id { get; set; }
        public int ProductsForOrderId { get; set; }
        public int ProductsReserveId { get; set; }
        public int Count { get; set; }
        public ProductsForOrderDto ProductsForOrder { get; set; }
        public ProductsReserveDto ProductsReserve { get; set; }
    }
}
