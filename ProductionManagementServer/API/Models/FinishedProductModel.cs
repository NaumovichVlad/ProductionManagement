using DataAccess.Entities;

namespace API.Models
{
    public class FinishedProductModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ProductName { get; set; }
    }
}
