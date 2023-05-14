using BusinessLogic.Dtos;

namespace API.Models
{
    public class MaterialsForFinishedProductsModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialReserveId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string OrderNumber { get; set; }
    }
}
