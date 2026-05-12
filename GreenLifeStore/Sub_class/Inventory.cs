using System;

namespace GreenLifeStore.Sub_class
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
        public int ReservedQuantity { get; set; }
        public int DamagedQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public int? ReorderQuantity { get; set; }  
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }  
        public string StockStatus { get; set; }
        public DateTime? LastStockInDate { get; set; }
        public DateTime? LastStockOutDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public DateTime UpdatedAt { get; set; } = DateTime.Now;  

        // Default constructor
        public Inventory() { }

        // Full constructor
        public Inventory(int id, int productId, int quantityInStock, int reservedQuantity, int damagedQuantity,
                         int reorderLevel, int? reorderQuantity, string batchNumber, DateTime? expiryDate,
                         string stockStatus, DateTime? lastStockInDate, DateTime? lastStockOutDate,
                         DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ProductId = productId;
            QuantityInStock = quantityInStock;
            ReservedQuantity = reservedQuantity;
            DamagedQuantity = damagedQuantity;
            ReorderLevel = reorderLevel;
            ReorderQuantity = reorderQuantity;
            BatchNumber = batchNumber;
            ExpiryDate = expiryDate;
            StockStatus = stockStatus;
            LastStockInDate = lastStockInDate;
            LastStockOutDate = lastStockOutDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Simple constructor for new stock entries
        public Inventory(int productId, int quantityInStock, string batchNumber, DateTime? expiryDate = null)
        {
            ProductId = productId;
            QuantityInStock = quantityInStock;
            BatchNumber = batchNumber;
            ExpiryDate = expiryDate;
            ReservedQuantity = 0;
            DamagedQuantity = 0;
            ReorderLevel = 0;
            StockStatus = "In Stock";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}