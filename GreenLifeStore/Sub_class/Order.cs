using System;

namespace GreenLifeStore.Sub_class
{
    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int OrderStatusId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount => Quantity * Price;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Orders() { }


        public Orders(int customerId, int orderStatusId, int productId, int quantity, decimal price)
        {
            CustomerId = customerId;
            OrderStatusId = orderStatusId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            OrderDate = DateTime.Now;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        // Full constructor including ID and timestamps
        public Orders(int id, int customerId, int orderStatusId, int productId, int quantity, decimal price, DateTime orderDate, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            CustomerId = customerId;
            OrderStatusId = orderStatusId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            OrderDate = orderDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        // Update order quantity and refresh UpdatedAt
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 1) throw new ArgumentException("Quantity must be at least 1.");
            Quantity = newQuantity;
            UpdatedAt = DateTime.Now;
        }

        // Update price per unit and refresh UpdatedAt
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0) throw new ArgumentException("Price cannot be negative.");
            Price = newPrice;
            UpdatedAt = DateTime.Now;
        }
    }
}