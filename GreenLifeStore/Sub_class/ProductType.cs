using System;

namespace GreenLifeStore.Sub_class
{
    public class ProductType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        // Default constructor
        public ProductType() { }

        // Constructor with parameters
        public ProductType(int id, string typeName, string description)
        {
            Id = id;
            TypeName = typeName;
            Description = description;
        }
    }
}