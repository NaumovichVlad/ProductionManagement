namespace BusinessLogic.Dtos
{
    public class MaterialsForProductsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialId { get; set; }
        public int Count { get; set; }
        public ProductDto Product { get; set; }
        public MaterialDto Material { get; set; }
    }
}
