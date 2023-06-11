namespace BusinessLogic.Dtos
{
    public class FinishedProductsSaleDto
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductsReserveId { get; set; }
        public int Count { get; set; }
        public SaleDto Sale { get; set; }
        public ProductsReserveDto ProductsReserve { get; set; }
    }
}
