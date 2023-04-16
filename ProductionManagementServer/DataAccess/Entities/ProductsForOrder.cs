using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class ProductsForOrder : IEntity
    {
        public int Id { get; set; }
        public int ProductOrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public ProductOrder ProductOrder { get; set; }
        public Product Product { get; set; }
    }
}
