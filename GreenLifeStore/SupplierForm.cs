using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore
{
    public partial class SupplierForm : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public SupplierForm()
        {
            InitializeComponent();
            try
            {
                LoadSuppliers();
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.MinimizeBox = true;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Size = new System.Drawing.Size(1000, 600);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing Supplier Form: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                string query = "SELECT * FROM supplier";
                DataTable dt = dbHelper.ExecuteQuery(query);
                dgvSuppliers.DataSource = dt;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while loading suppliers: " + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading suppliers: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            try
            {
                txtName.Clear();
                txtContact.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
                cmbDistanceType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing fields: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add new supplier
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"INSERT INTO supplier 
                                (supplier_name, contact_person, phone, email, address, distance_type) 
                                VALUES (@name, @contact, @phone, @email, @address, @distance)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", txtName.Text),
                    new SqlParameter("@contact", txtContact.Text),
                    new SqlParameter("@phone", txtPhone.Text),
                    new SqlParameter("@email", txtEmail.Text),
                    new SqlParameter("@address", txtAddress.Text),
                    new SqlParameter("@distance", cmbDistanceType.SelectedItem?.ToString() ?? "")
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSuppliers();
                    ClearFields();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while adding supplier: " + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while adding supplier: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update selected supplier
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSuppliers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a supplier to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvSuppliers.SelectedRows[0].Cells["id"].Value);
                string query = @"UPDATE supplier SET 
                                supplier_name=@name, contact_person=@contact, phone=@phone, email=@email, 
                                address=@address, distance_type=@distance, updated_at=GETDATE()
                                WHERE id=@id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id", id),
                    new SqlParameter("@name", txtName.Text),
                    new SqlParameter("@contact", txtContact.Text),
                    new SqlParameter("@phone", txtPhone.Text),
                    new SqlParameter("@email", txtEmail.Text),
                    new SqlParameter("@address", txtAddress.Text),
                    new SqlParameter("@distance", cmbDistanceType.SelectedItem?.ToString() ?? "")
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSuppliers();
                    ClearFields();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while updating supplier: " + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating supplier: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete selected supplier
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSuppliers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a supplier to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvSuppliers.SelectedRows[0].Cells["id"].Value);
                string query = "DELETE FROM supplier WHERE id=@id";
                SqlParameter param = new SqlParameter("@id", id);

                int result = dbHelper.ExecuteNonQuery(query, new SqlParameter[] { param });
                if (result > 0)
                {
                    MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSuppliers();
                    ClearFields();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while deleting supplier: " + sqlEx.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while deleting supplier: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load selected row into input fields
        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    txtName.Text = dgvSuppliers.Rows[e.RowIndex].Cells["supplier_name"].Value.ToString();
                    txtContact.Text = dgvSuppliers.Rows[e.RowIndex].Cells["contact_person"].Value.ToString();
                    txtPhone.Text = dgvSuppliers.Rows[e.RowIndex].Cells["phone"].Value.ToString();
                    txtEmail.Text = dgvSuppliers.Rows[e.RowIndex].Cells["email"].Value.ToString();
                    txtAddress.Text = dgvSuppliers.Rows[e.RowIndex].Cells["address"].Value.ToString();
                    cmbDistanceType.SelectedItem = dgvSuppliers.Rows[e.RowIndex].Cells["distance_type"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting supplier row: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Clear fields button
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}