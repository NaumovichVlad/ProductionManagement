using DataAccess.Entities;

namespace API.Models
{
    public class FinishedProductsForOrderModel
    {
        public int Id { get; set; }
        public int ProductsForOrderId { get; set; }
        public int ProductsReserveId { get; set; }
        public int Count { get; set; }
        public string ProductsForOrderName { get; set; }
        public string ProductsReserveName { get; set; }
    }
}
