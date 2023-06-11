namespace BusinessLogic.Dtos
{
    public class MaterialsForFinishedProductsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialReserveId { get; set; }
        public int Count { get; set; }
        public FinishedProductDto Product { get; set; }
        public MaterialReserveDto MaterialReserve { get; set; }
    }
}
