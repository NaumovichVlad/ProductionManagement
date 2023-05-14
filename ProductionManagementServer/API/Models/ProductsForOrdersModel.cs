using BusinessLogic.Dtos;

namespace API.Models
{
    public class ProductsForOrdersModel
    {
        public int Id { get; set; }
        public int ProductOrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string ProductOrderNumber { get; set; }
        public string ProductName { get; set; }
    }
}
