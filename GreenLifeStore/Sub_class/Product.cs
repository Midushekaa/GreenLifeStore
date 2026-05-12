using System;

namespace GreenLifeStore.Sub_class
{
    public class Product
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}