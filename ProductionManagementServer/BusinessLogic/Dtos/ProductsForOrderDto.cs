using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class ProductsForOrderDto
    {
        public int Id { get; set; }
        public int ProductOrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public ProductOrderDto ProductOrder { get; set; }
        public ProductDto Product { get; set; }
    }
}
