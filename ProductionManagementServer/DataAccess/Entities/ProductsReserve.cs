using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class ProductsReserve : IEntity
    {
        public int Id { get; set; }
        public int StoragePlaceId { get; set; }
        public int FinishedProductId { get; set; }
        public int Count { get; set; }
        public StoragePlace StoragePlace { get; set; }
        public FinishedProduct FinishedProduct { get; set; }
    }
}
