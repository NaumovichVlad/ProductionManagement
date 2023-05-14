using BusinessLogic.Dtos;

namespace API.Models
{
    public class MaterialOrderModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int MaterialId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ManufactureCountry { get; set; }
        public int CounteragentId { get; set; }
        public string MaterialName { get; set; }
        public string CounteragentName { get; set; }
    }
}
