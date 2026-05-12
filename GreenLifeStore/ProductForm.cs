using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GreenLifeStore
{
    public partial class ProductForm : Form
    {
        private int productId = 0; // 0 = Add, >0 = Edit
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["GreenLifeStoreDb"].ConnectionString;
        private DataTable productTable;      // store products for filtering/search
        private DataTable supplierTable;     // store suppliers for ComboBox
        private DataTable productTypeTable;  // store product types for ComboBox

        public ProductForm()
        {
            InitializeComponent();
            SetFixedSize();
            LoadSuppliers();
            LoadProductTypes();
            LoadProducts();
        }

        public ProductForm(int id) : this()
        {
            productId = id;
            LoadProductData();
        }

        private void SetFixedSize()
        {
            this.Text = "Product Management";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1459, 710);
        }

        // ------------------------- VALIDATION -------------------------
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Product Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (comboProductType.SelectedValue == null)
            {
                MessageBox.Show("Please select a product type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboProductType.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Price must be a valid positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Stock must be a valid non-negative integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStock.Focus();
                return false;
            }

            if (comboSupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboSupplier.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDiscount.Text, out decimal discount) || discount < 0)
            {
                MessageBox.Show("Discount must be a valid positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscount.Focus();
                return false;
            }

            if (!float.TryParse(txtRating.Text, out float rating) || rating < 0 || rating > 5)
            {
                MessageBox.Show("Rating must be a number between 0 and 5.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRating.Focus();
                return false;
            }

            return true;
        }

        // ------------------------- LOAD COMBOBOX DATA -------------------------
        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT id, supplier_name FROM supplier", conn);
                    supplierTable = new DataTable();
                    da.Fill(supplierTable);
                    comboSupplier.DataSource = supplierTable;
                    comboSupplier.DisplayMember = "supplier_name";
                    comboSupplier.ValueMember = "id";
                    comboSupplier.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductTypes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT id, type_name FROM product_type", conn);
                    productTypeTable = new DataTable();
                    da.Fill(productTypeTable);
                    comboProductType.DataSource = productTypeTable;
                    comboProductType.DisplayMember = "type_name";
                    comboProductType.ValueMember = "id";
                    comboProductType.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product types: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------- LOAD PRODUCTS -------------------------
        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    p.id, 
                    p.name, 
                    pt.id AS ProductTypeId, 
                    pt.type_name AS ProductType,
                    p.price, 
                    p.discount, 
                    p.rating,
                    s.id AS SupplierId, 
                    s.supplier_name AS Supplier,
                    ISNULL(i.quantity_in_stock, 0) AS Stock,
                    p.image
                FROM product p
                JOIN supplier s ON p.supplier_id = s.id
                JOIN product_type pt ON p.product_type_id = pt.id
                LEFT JOIN inventory i ON p.id = i.product_id";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    productTable = new DataTable();
                    da.Fill(productTable);

                    dgvProducts.Columns.Clear();

                    DataTable dt = productTable.Copy();
                    dt.Columns.Add("ImageDisplay", typeof(Image));

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["image"] != DBNull.Value)
                        {
                            byte[] imgBytes = (byte[])row["image"];
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                row["ImageDisplay"] = Image.FromStream(ms);
                            }
                        }
                    }

                    dgvProducts.DataSource = dt;
                    dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvProducts.RowTemplate.Height = 80;

                    if (dgvProducts.Columns["ImageDisplay"] != null)
                    {
                        DataGridViewImageColumn imgCol =
                            (DataGridViewImageColumn)dgvProducts.Columns["ImageDisplay"];
                        imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        imgCol.HeaderText = "Image";
                    }

                    // Hide unnecessary columns
                    dgvProducts.Columns["id"].Visible = false;
                    dgvProducts.Columns["ProductTypeId"].Visible = false;
                    dgvProducts.Columns["SupplierId"].Visible = false;
                    dgvProducts.Columns["image"].Visible = false;

                    dgvProducts.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductData()
        {
            if (productId == 0) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM product WHERE id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", productId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtPrice.Text = dr["price"].ToString();
                        txtDiscount.Text = dr["discount"].ToString();
                        txtRating.Text = dr["rating"].ToString();
                        txtStock.Text = GetStock(productId).ToString();
                        comboSupplier.SelectedValue = Convert.ToInt32(dr["supplier_id"]);
                        comboProductType.SelectedValue = Convert.ToInt32(dr["product_type_id"]);

                        if (dr["image"] != DBNull.Value)
                        {
                            byte[] imgBytes = (byte[])dr["image"];
                            using (var ms = new MemoryStream(imgBytes))
                            {
                                pbProductImage.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            pbProductImage.Image = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetStock(int productId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT quantity_in_stock FROM inventory WHERE product_id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", productId);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch { return 0; }
        }

        // ------------------------- CLEAR FORM -------------------------
        private void ClearForm()
        {
            productId = 0;
            txtName.Clear();
            txtPrice.Clear();
            txtDiscount.Clear();
            txtRating.Clear();
            txtStock.Clear();
            comboSupplier.SelectedIndex = -1;
            comboProductType.SelectedIndex = -1;
            pbProductImage.Image = null;
        }

        // ------------------------- UPLOAD IMAGE -------------------------
        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbProductImage.Image = Image.FromFile(ofd.FileName);
                }
            }
        }
        // ------------------------- SAVE -------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();

                    byte[] imgBytes = null;
                    if (pbProductImage.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pbProductImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            imgBytes = ms.ToArray();
                        }
                    }

                    SqlCommand cmd = new SqlCommand(@"
            INSERT INTO product 
            (supplier_id, product_type_id, name, category, price, discount, rating, image, created_at, updated_at)
            VALUES 
            (@SupplierId, @ProductTypeId, @Name, @Category, @Price, @Discount, @Rating, @Image, @CreatedAt, @UpdatedAt);
            SELECT SCOPE_IDENTITY();", conn);

                    cmd.Parameters.AddWithValue("@SupplierId", Convert.ToInt32(comboSupplier.SelectedValue));
                    cmd.Parameters.AddWithValue("@ProductTypeId", Convert.ToInt32(comboProductType.SelectedValue));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Category", DBNull.Value); // change if needed
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(txtDiscount.Text));
                    cmd.Parameters.AddWithValue("@Rating", Convert.ToSingle(txtRating.Text));
                    cmd.Parameters.AddWithValue("@Image", (object)imgBytes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int newProductId = Convert.ToInt32(result);

                        SqlCommand invCmd = new SqlCommand(@"
                INSERT INTO inventory 
                (product_id, quantity_in_stock, reorder_level, stock_status, created_at, updated_at)
                VALUES 
                (@ProductId, @Quantity, 10, 'In Stock', @CreatedAt, @UpdatedAt)", conn);

                        invCmd.Parameters.AddWithValue("@ProductId", newProductId);
                        invCmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtStock.Text));
                        invCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        invCmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                        invCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Product saved successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------- VIEW -------------------------
        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProducts.SelectedRows[0];
                string details = $"Name: {row.Cells["name"].Value}\n" +
                                 $"Price: {row.Cells["price"].Value}\n" +
                                 $"Discount: {row.Cells["discount"].Value}\n" +
                                 $"Rating: {row.Cells["rating"].Value}\n" +
                                 $"Stock: {row.Cells["Stock"].Value}\n" +
                                 $"Supplier: {row.Cells["Supplier"].Value}\n" +
                                 $"Product Type: {row.Cells["ProductType"].Value}";
                MessageBox.Show(details, "Product Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a product first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ------------------------- UPDATE -------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            if (productId == 0)
            {
                MessageBox.Show("Please select a product to update.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        byte[] imgBytes = null;

                        if (pbProductImage.Image != null)
                        {
                            using (var ms = new MemoryStream())
                            {
                                pbProductImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                imgBytes = ms.ToArray();
                            }
                        }

                        // ================= UPDATE PRODUCT =================
                        SqlCommand cmd = new SqlCommand(@"
                    UPDATE product
                    SET name=@Name,
                        price=@Price,
                        discount=@Discount,
                        rating=@Rating,
                        supplier_id=@SupplierId,
                        product_type_id=@ProductTypeId,
                        image=@Image,
                        updated_at=@UpdatedAt
                    WHERE id=@Id", conn, transaction);

                        cmd.Parameters.AddWithValue("@Id", productId);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(txtDiscount.Text));
                        cmd.Parameters.AddWithValue("@Rating", Convert.ToSingle(txtRating.Text));
                        cmd.Parameters.AddWithValue("@SupplierId", Convert.ToInt32(comboSupplier.SelectedValue));
                        cmd.Parameters.AddWithValue("@ProductTypeId", Convert.ToInt32(comboProductType.SelectedValue));
                        cmd.Parameters.AddWithValue("@Image", (object)imgBytes ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                        cmd.ExecuteNonQuery();

                        // ================= SAFE INVENTORY UPDATE =================
                        SqlCommand invCmd = new SqlCommand(@"
                    MERGE inventory AS target
                    USING (SELECT @ProductId AS product_id) AS source
                    ON target.product_id = source.product_id
                    WHEN MATCHED THEN
                        UPDATE SET 
                            quantity_in_stock = @Quantity,
                            updated_at = @UpdatedAt
                    WHEN NOT MATCHED THEN
                        INSERT (product_id, quantity_in_stock, reorder_level, stock_status, created_at, updated_at)
                        VALUES (@ProductId, @Quantity, 10, 'In Stock', @UpdatedAt, @UpdatedAt);",
                            conn, transaction);

                        invCmd.Parameters.AddWithValue("@ProductId", productId);
                        invCmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtStock.Text));
                        invCmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                        invCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                MessageBox.Show("Product updated successfully!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------- DELETE -------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (productId == 0)
            {
                MessageBox.Show("Select a product to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        DELETE FROM discount WHERE product_id=@Id;
                        DELETE FROM review WHERE product_id=@Id;
                        DELETE FROM low_stock_notification WHERE product_id=@Id;
                        DELETE FROM inventory WHERE product_id=@Id;
                        DELETE FROM product WHERE id=@Id", conn);

                    cmd.Parameters.AddWithValue("@Id", productId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------- DATAGRIDVIEW CLICK -------------------------
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
            productId = Convert.ToInt32(row.Cells["id"].Value);
            txtName.Text = row.Cells["name"].Value.ToString();
            txtPrice.Text = row.Cells["price"].Value.ToString();
            txtDiscount.Text = row.Cells["discount"].Value.ToString();
            txtRating.Text = row.Cells["rating"].Value.ToString();
            txtStock.Text = row.Cells["Stock"].Value.ToString();
            comboSupplier.SelectedValue = Convert.ToInt32(row.Cells["SupplierId"].Value);
            comboProductType.SelectedValue = Convert.ToInt32(row.Cells["ProductTypeId"].Value);

            if (row.Cells["ImageDisplay"].Value != DBNull.Value)
            {
                pbProductImage.Image = row.Cells["ImageDisplay"].Value as Image;
            }
            else
            {
                pbProductImage.Image = null;
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (productTable == null) return;

            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadProducts();
                return;
            }

            DataTable resultTable = productTable.Copy();
            resultTable.Columns.Add("ImageDisplay", typeof(Image));

            foreach (DataRow row in resultTable.Rows)
            {
                if (row["image"] != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])row["image"];
                    using (MemoryStream ms = new MemoryStream(imgBytes))
                    {
                        row["ImageDisplay"] = Image.FromStream(ms);
                    }
                }
            }

            DataTable filteredTable = resultTable.Clone();

            foreach (DataRow row in resultTable.Rows)
            {
                string name = row["name"].ToString().ToLower();
                string supplier = row["Supplier"].ToString().ToLower();
                string productType = row["ProductType"].ToString().ToLower();

                if (name.Contains(searchText) || supplier.Contains(searchText) || productType.Contains(searchText))
                {
                    filteredTable.ImportRow(row);
                }
            }

            dgvProducts.DataSource = filteredTable;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}