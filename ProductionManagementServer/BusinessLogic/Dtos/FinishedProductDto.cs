namespace BusinessLogic.Dtos
{
    public class FinishedProductDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime ManufactureDate { get; set; }
        public bool IsApproved { get; set; }
        public ProductDto Product { get; set; }
    }
}
