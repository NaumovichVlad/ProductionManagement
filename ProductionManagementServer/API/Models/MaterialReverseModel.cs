using BusinessLogic.Dtos;

namespace API.Models
{
    public class MaterialReserveModel
    {
        public int Id { get; set; }
        public int StoragePlaceId { get; set; }
        public int MaterialPurchaseId { get; set; }
        public int Count { get; set; }
        public string StoragePlaceName { get; set; }
        public string MaterialOrderNumber { get; set; }
        public string MaterialName { get; set; }
    }
}
