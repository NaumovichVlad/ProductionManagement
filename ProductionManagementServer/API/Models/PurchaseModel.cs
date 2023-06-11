namespace API.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ManufactureCountry { get; set; }
        public int CounteragentId { get; set; }
        public string CounteragentName { get; set; }
    }
}
