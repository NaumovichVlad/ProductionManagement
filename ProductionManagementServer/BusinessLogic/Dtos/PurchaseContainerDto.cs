namespace BusinessLogic.Dtos
{
    public class PurchaseContainerDto
    {
        public string OrderNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ManufactureCountry { get; set; }
        public string CounteragentName { get; set; }
        public string Materials { get; set; }
    }
}
