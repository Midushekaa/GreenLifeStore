using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GreenLifeStore
{
    public partial class SearchProductsForm : Form
    {
        private readonly string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["GreenLifeStoreDb"].ConnectionString;
        private int customerId;

        public SearchProductsForm(int custId)
        {
            InitializeComponent();
            customerId = custId;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);

            this.Load += SearchProductsForm_Load;
        }

        private void SearchProductsForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
        }

        // Load categories into ComboBox
        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All");

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = "SELECT DISTINCT Category FROM Product WHERE Category IS NOT NULL";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["Category"].ToString();
                                if (!string.IsNullOrWhiteSpace(category))
                                    cmbCategory.Items.Add(category);
                            }
                        }
                    }
                }
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        // Load all products initially
        private void LoadProducts()
        {
            string query = "SELECT Id, Name, Category, Price, Image FROM Product";
            DataTable dt = GetProductsFromDatabase(query);
            BindProductsToGrid(dt);
        }

        // Search button click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT Id, Name, Category, Price, Image FROM Product WHERE 1=1";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;

                        if (!string.IsNullOrWhiteSpace(txtName.Text))
                        {
                            query += " AND Name LIKE @Name";
                            cmd.Parameters.AddWithValue("@Name", "%" + txtName.Text + "%");
                        }

                        if (cmbCategory.Text != "All")
                        {
                            query += " AND Category=@Category";
                            cmd.Parameters.AddWithValue("@Category", cmbCategory.Text);
                        }

                        if (decimal.TryParse(txtMin.Text, out decimal min))
                        {
                            query += " AND Price >= @Min";
                            cmd.Parameters.AddWithValue("@Min", min);
                        }

                        if (decimal.TryParse(txtMax.Text, out decimal max))
                        {
                            query += " AND Price <= @Max";
                            cmd.Parameters.AddWithValue("@Max", max);
                        }

                        cmd.CommandText = query;
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            da.Fill(dt);

                        if (dt.Rows.Count == 0)
                            MessageBox.Show("No products found matching the search criteria.");

                        BindProductsToGrid(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching products: " + ex.Message);
                }
            }
        }

        // Get products with dynamic discount
        private DataTable GetProductsFromDatabase(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                // Apply discounts dynamically
                foreach (DataRow row in dt.Rows)
                {
                    int productId = Convert.ToInt32(row["Id"]);
                    decimal price = Convert.ToDecimal(row["Price"]);

                    string discountQuery = @"
                        SELECT TOP 1 discount_percentage 
                        FROM discount 
                        WHERE product_id=@pid AND GETDATE() BETWEEN start_date AND end_date";
                    using (SqlConnection con = new SqlConnection(connStr))
                    using (SqlCommand cmd = new SqlCommand(discountQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@pid", productId);
                        con.Open();
                        object discountObj = cmd.ExecuteScalar();
                        con.Close();

                        if (discountObj != null)
                        {
                            decimal discountPercent = Convert.ToDecimal(discountObj);
                            price -= price * discountPercent / 100;
                            row["Price"] = price;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
            return dt;
        }

        // Bind products to DataGridView
        private void BindProductsToGrid(DataTable dt)
        {
            try
            {
                if (!dt.Columns.Contains("ProductImage"))
                    dt.Columns.Add("ProductImage", typeof(Image));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Image"] != DBNull.Value)
                    {
                        byte[] imgBytes = (byte[])row["Image"];
                        using (var ms = new System.IO.MemoryStream(imgBytes))
                        {
                            row["ProductImage"] = Image.FromStream(ms);
                        }
                    }
                }

                dgvProducts.DataSource = null;
                dgvProducts.Columns.Clear();
                dgvProducts.AutoGenerateColumns = false;

                dgvProducts.Columns.Add("Id", "Id");
                dgvProducts.Columns["Id"].DataPropertyName = "Id";

                dgvProducts.Columns.Add("Name", "Name");
                dgvProducts.Columns["Name"].DataPropertyName = "Name";

                dgvProducts.Columns.Add("Category", "Category");
                dgvProducts.Columns["Category"].DataPropertyName = "Category";

                dgvProducts.Columns.Add("Price", "Price");
                dgvProducts.Columns["Price"].DataPropertyName = "Price";
                dgvProducts.Columns["Price"].DefaultCellStyle.Format = "\"Rs\" #,##0.00";

                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "Image";
                imgCol.HeaderText = "Product Image";
                imgCol.DataPropertyName = "ProductImage";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgvProducts.Columns.Add(imgCol);

                dgvProducts.DataSource = dt;
                dgvProducts.RowTemplate.Height = 80;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error binding products: " + ex.Message);
            }
        }

        // Add selected product to cart
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            int productId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["Id"].Value);

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM cart WHERE customer_id=@custId AND product_id=@productId";
                    using (SqlCommand cmd = new SqlCommand(checkQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@custId", customerId);
                        cmd.Parameters.AddWithValue("@productId", productId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            string updateQuery = @"UPDATE cart 
                                                   SET quantity = quantity + 1, updated_at=GETDATE()
                                                   WHERE customer_id=@custId AND product_id=@productId";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                            {
                                updateCmd.Parameters.AddWithValue("@custId", customerId);
                                updateCmd.Parameters.AddWithValue("@productId", productId);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = @"INSERT INTO cart 
                                                   (customer_id, product_id, quantity, created_at, updated_at)
                                                   VALUES (@custId, @productId, 1, GETDATE(), GETDATE())";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@custId", customerId);
                                insertCmd.Parameters.AddWithValue("@productId", productId);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Product added to cart successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product to cart: " + ex.Message);
            }
        }

        // Dummy CellContentClick to avoid designer errors
        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void panelHeader_Paint(object sender, PaintEventArgs e) { }
    }
}