using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class MaterialsForProducts : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialId { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
        public Material Material { get; set; }
    }
}
