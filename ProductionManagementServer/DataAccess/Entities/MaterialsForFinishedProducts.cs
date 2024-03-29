﻿using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class MaterialsForFinishedProducts : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MaterialReserveId { get; set; }
        public int Count { get; set; }
        public FinishedProduct Product { get; set; }
        public MaterialReserve Material { get; set; }
    }
}
