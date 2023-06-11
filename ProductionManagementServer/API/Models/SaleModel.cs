namespace API.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int CounteragentId { get; set; }
        public string CounteragentName { get; set; }
    }
}
