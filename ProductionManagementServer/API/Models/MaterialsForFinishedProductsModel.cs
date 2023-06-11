namespace API.Models
{
    public class MaterialsForFinishedProductsModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialReserveId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public string MaterialName { get; set; }
    }
}
