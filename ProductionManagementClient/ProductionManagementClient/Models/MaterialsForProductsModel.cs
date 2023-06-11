namespace ProductionManagementClient.Models
{
    public class MaterialsForProductsModel : ModelBase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public string MaterialName { get; set; }
    }
}
