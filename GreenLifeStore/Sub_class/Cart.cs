using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore.Sub_class
{
    public class Cart
    {
        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Cart(int id, int customerId, int productId, int quantity)
        {
            Id = id;
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 1) throw new ArgumentException("Quantity must be at least 1");
            Quantity = newQuantity;
            UpdatedAt = DateTime.Now;
        }

        public void IncrementQuantity(int amount = 1)
        {
            if (amount < 1) throw new ArgumentException("Increment amount must be at least 1");
            Quantity += amount;
            UpdatedAt = DateTime.Now;
        }

        public void DecrementQuantity(int amount = 1)
        {
            if (amount < 1 || Quantity - amount < 0) throw new ArgumentException("Invalid decrement amount");
            Quantity -= amount;
            UpdatedAt = DateTime.Now;
        }

        // Load all cart items for a customer
        public static List<Cart> LoadCartItems(int customerId, DatabaseHelper db)
        {
            List<Cart> items = new List<Cart>();
            string query = "SELECT id, product_id, quantity FROM cart WHERE customer_id=@cid";
            DataTable dt = db.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@cid", customerId) });

            foreach (DataRow row in dt.Rows)
            {
                items.Add(new Cart(
                    Convert.ToInt32(row["id"]),
                    customerId,
                    Convert.ToInt32(row["product_id"]),
                    Convert.ToInt32(row["quantity"])
                ));
            }

            return items;
        }

        // Add or update cart item
        public static void AddOrUpdateCartItem(int customerId, int productId, int quantity, DatabaseHelper db, List<Cart> cartItems)
        {
            Cart existing = cartItems.Find(c => c.ProductId == productId);

            if (existing != null)
            {
                existing.IncrementQuantity(quantity);
                string updateQuery = "UPDATE cart SET quantity=@qty, updated_at=GETDATE() WHERE id=@id";
                db.ExecuteNonQuery(updateQuery, new SqlParameter[]
                {
                    new SqlParameter("@qty", existing.Quantity),
                    new SqlParameter("@id", existing.Id)
                });
            }
            else
            {
                string insertQuery = "INSERT INTO cart(customer_id, product_id, quantity, created_at, updated_at) VALUES(@cid,@pid,@qty,GETDATE(),GETDATE()); SELECT SCOPE_IDENTITY();";
                int newId = Convert.ToInt32(db.ExecuteScalar(insertQuery, new SqlParameter[]
                {
                    new SqlParameter("@cid", customerId),
                    new SqlParameter("@pid", productId),
                    new SqlParameter("@qty", quantity)
                }));

                cartItems.Add(new Cart(newId, customerId, productId, quantity));
            }
        }

        // Remove a cart item
        public static void RemoveCartItem(int cartItemId, DatabaseHelper db, List<Cart> cartItems)
        {
            string deleteQuery = "DELETE FROM cart WHERE id=@id";
            db.ExecuteNonQuery(deleteQuery, new SqlParameter[] { new SqlParameter("@id", cartItemId) });
            cartItems.RemoveAll(c => c.Id == cartItemId);
        }

        // Place order for all cart items
        public static decimal PlaceOrder(int customerId, DatabaseHelper db, List<Cart> cartItems)
        {
            decimal totalAmount = 0;

            foreach (Cart item in cartItems)
            {
                decimal price = 100; // replace with actual price from DB
                decimal total = price * item.Quantity;

                string orderQuery = @"
                    INSERT INTO orders (customer_id, order_status_id, product_id, quantity, price, total_amount, created_at, updated_at)
                    VALUES (@cid, 1, @pid, @qty, @price, @total, GETDATE(), GETDATE()); SELECT SCOPE_IDENTITY();";

                int orderId = Convert.ToInt32(db.ExecuteScalar(orderQuery, new SqlParameter[]
                {
                    new SqlParameter("@cid", customerId),
                    new SqlParameter("@pid", item.ProductId),
                    new SqlParameter("@qty", item.Quantity),
                    new SqlParameter("@price", price),
                    new SqlParameter("@total", total)
                }));

                string paymentQuery = @"
                    INSERT INTO payment (order_id, payment_type_id, payment_status_id, amount, created_at, updated_at)
                    VALUES (@orderId, 1, 1, @amount, GETDATE(), GETDATE())";

                db.ExecuteNonQuery(paymentQuery, new SqlParameter[]
                {
                    new SqlParameter("@orderId", orderId),
                    new SqlParameter("@amount", total)
                });

                totalAmount += total;
            }

            // Clear cart
            string clearQuery = "DELETE FROM cart WHERE customer_id=@cid";
            db.ExecuteNonQuery(clearQuery, new SqlParameter[] { new SqlParameter("@cid", customerId) });
            cartItems.Clear();

            return totalAmount;
        }
    }
}