using DataAccess.Entities;

namespace API.Models
{
    public class FinishedProductSaleModel
    {
        public int Id { get; set; }
        public int ProductsForOrderId { get; set; }
        public int ProductsReserveId { get; set; }
        public int Count { get; set; }
        public string SaleNumber { get; set; }
        public string ProductName { get; set; }
    }
}
