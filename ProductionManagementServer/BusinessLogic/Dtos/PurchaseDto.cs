namespace BusinessLogic.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ManufactureCountry { get; set; }
        public int CounteragentId { get; set; }
        public CounteragentDto Counteragent { get; set; }
    }
}
