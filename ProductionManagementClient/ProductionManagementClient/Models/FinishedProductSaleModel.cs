namespace ProductionManagementClient.Models
{
    public class FinishedProductSaleModel : ModelBase
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductsReserveId { get; set; }
        public int Count { get; set; }
        public string SaleNumber { get; set; }
        public string ProductName { get; set; }
    }
}
