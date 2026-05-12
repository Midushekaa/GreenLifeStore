using System;

namespace GreenLifeStore.Sub_class
{
    public class LowStockNotification
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public string Message { get; set; }
        public DateTime? NotifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}