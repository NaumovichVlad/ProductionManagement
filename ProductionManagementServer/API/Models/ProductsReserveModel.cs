namespace API.Models
{
    public class ProductsReserveModel
    {
        public int Id { get; set; }
        public int StoragePlaceId { get; set; }
        public int FinishedProductId { get; set; }
        public int Count { get; set; }
        public string StoragePlaceName { get; set; }
        public string FinishedProductName { get; set; }
    }
}
