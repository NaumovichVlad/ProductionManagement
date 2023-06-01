using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class FinishedProductSale : IEntity
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductsReserveId { get; set; }
        public int Count { get; set; }
        public Sale Sale { get; set; }
        public ProductsReserve ProductsReserve { get; set; }
    }
}
