using BusinessLogic.Dtos;

namespace API.Models
{
    public class MaterialPurchaseModel
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int PurchaseId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsAccepted { get; set; }
        public string MaterialName { get; set; }
        public string PurchaseNumber { get; set; }
    }
}
