namespace BusinessLogic.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int CounteragentId { get; set; }
        public CounteragentDto Counteragent { get; set; }

    }
}
