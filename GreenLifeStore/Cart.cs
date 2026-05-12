using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;
using GreenLifeStore.Sub_class;

namespace GreenLifeStore
{
    public partial class CartForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        private readonly int customerId;
        private List<Cart> cartItems = new List<Cart>();

        public CartForm(int custId)
        {
            InitializeComponent();
            customerId = custId;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1000, 600);

            this.Load += CartForm_Load;
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            LoadCart();
        }

      
        private void LoadCart()
        {
            string query = @"
                SELECT 
                    c.id AS CartItemId,
                    c.product_id AS ProductId,
                    p.Name AS ProductName,
                    p.Price,
                    c.quantity,
                    ISNULL(d.discount_percentage, 0) AS DiscountPercentage
                FROM cart c
                INNER JOIN product p ON c.product_id = p.id
                LEFT JOIN discount d 
                    ON c.product_id = d.product_id 
                    AND GETDATE() BETWEEN d.start_date AND d.end_date
                WHERE c.customer_id=@cid";

            DataTable dt = db.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@cid", customerId) });

            cartItems.Clear();
            foreach (DataRow row in dt.Rows)
            {
                cartItems.Add(new Cart(
                    Convert.ToInt32(row["CartItemId"]),
                    customerId,
                    Convert.ToInt32(row["ProductId"]),
                    Convert.ToInt32(row["quantity"])
                ));
            }

            // Add Total column if not exists
            if (!dt.Columns.Contains("Total"))
                dt.Columns.Add("Total", typeof(decimal));

            // Calculate discounted price and total
            foreach (DataRow row in dt.Rows)
            {
                decimal price = Convert.ToDecimal(row["Price"]);
                decimal discountPercent = Convert.ToDecimal(row["DiscountPercentage"]);
                int qty = Convert.ToInt32(row["quantity"]);

                if (discountPercent > 0)
                    price -= price * discountPercent / 100;

                row["Price"] = price;
                row["Total"] = price * qty;
            }

            dgvCart.DataSource = dt;

            // Hide CartItemId column
            if (dgvCart.Columns["CartItemId"] != null)
                dgvCart.Columns["CartItemId"].Visible = false;

            if (dgvCart.Columns["ProductName"] != null)
                dgvCart.Columns["ProductName"].Width = 250;

            if (dgvCart.Columns["Price"] != null)
                dgvCart.Columns["Price"].DefaultCellStyle.Format = "\"Rs\" #,##0.00";

            if (dgvCart.Columns["Total"] != null)
                dgvCart.Columns["Total"].DefaultCellStyle.Format = "\"Rs\" #,##0.00";
        }


        public void AddToCart(int productId, int quantity = 1)
        {
            Cart.AddOrUpdateCartItem(customerId, productId, quantity, db, cartItems);
            LoadCart();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an item to remove!");
                return;
            }

            int cartItemId = Convert.ToInt32(dgvCart.SelectedRows[0].Cells["CartItemId"].Value);
            Cart.RemoveCartItem(cartItemId, db, cartItems);
            LoadCart();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("Your cart is empty!");
                return;
            }

            decimal grandTotal = 0;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;

                int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                int quantity = Convert.ToInt32(row.Cells["quantity"].Value);
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                decimal totalAmount = price * quantity;

                grandTotal += totalAmount;

                string insertOrderQuery = @"
                    INSERT INTO orders (customer_id, order_status_id, product_id, quantity, price, total_amount)
                    VALUES (@customer_id, @status_id, @product_id, @quantity, @price, @total_amount);
                    SELECT SCOPE_IDENTITY();";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@customer_id", customerId),
                    new SqlParameter("@status_id", 1), // Pending
                    new SqlParameter("@product_id", productId),
                    new SqlParameter("@quantity", quantity),
                    new SqlParameter("@price", price),
                    new SqlParameter("@total_amount", totalAmount)
                };

                db.ExecuteScalar(insertOrderQuery, parameters);
            }

            // Show payment form
            PaymentForm paymentForm = new PaymentForm(grandTotal);
            paymentForm.ShowDialog();

            // Clear cart
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.IsNewRow) continue;
                int cartItemId = Convert.ToInt32(row.Cells["CartItemId"].Value);
                Cart.RemoveCartItem(cartItemId, db, cartItems);
            }

            LoadCart();

            MessageBox.Show("Order placed successfully! Total Amount: Rs " + grandTotal.ToString("N2"),
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}