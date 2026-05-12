using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using GreenLifeStore.Sub_class;
using GreenLifeStore.DataLayer;
using System.Data.SqlClient;

namespace GreenLifeStore
{
    public partial class CustomerForm : Form
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private List<User> customers = new List<User>();
        private User selectedCustomer = null;

        public CustomerForm()
        {
            InitializeComponent();

            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            LoadCustomers();
        }

        // LOAD CUSTOMERS
        private void LoadCustomers()
        {
            customers.Clear();

            try
            {
                DataTable dt = dbHelper.ExecuteQuery("SELECT * FROM [User] WHERE Role='CUSTOMER'");

                foreach (DataRow row in dt.Rows)
                {
                    customers.Add(new User(
                        Convert.ToInt32(row["Id"]),
                        row["Full_Name"].ToString(),
                        row["Email"].ToString(),
                        row["Password"].ToString(),
                        "CUSTOMER",
                        row["Address"]?.ToString() ?? "",
                        row["Phone"]?.ToString() ?? "",
                        Convert.ToDateTime(row["Created_At"]),
                        Convert.ToDateTime(row["Updated_At"])
                    ));
                }

                dgvCustomers.DataSource = customers.Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Email,
                    c.Phone,
                    c.Address
                }).ToList();

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
        }

        // SELECT CUSTOMER FROM GRID
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int customerId = Convert.ToInt32(dgvCustomers.Rows[e.RowIndex].Cells["Id"].Value);

                selectedCustomer = customers.FirstOrDefault(c => c.Id == customerId);

                if (selectedCustomer != null)
                {
                    txtFullName.Text = selectedCustomer.FullName;
                    txtEmail.Text = selectedCustomer.Email;
                    txtPhone.Text = selectedCustomer.Phone;
                    txtAddress.Text = selectedCustomer.Address;
                }
            }
        }

        // CLEAR TEXTBOXES
        private void ClearFields()
        {
            selectedCustomer = null;

            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
        }

        // VIEW CUSTOMER
        private void btnView_Click(object sender, EventArgs e)
        {
            if (selectedCustomer != null)
            {
                string details =
                    $"Full Name: {selectedCustomer.FullName}\n" +
                    $"Email: {selectedCustomer.Email}\n" +
                    $"Phone: {selectedCustomer.Phone}\n" +
                    $"Address: {selectedCustomer.Address}";

                MessageBox.Show(details, "Customer Details",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a customer first.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // UPDATE CUSTOMER
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null)
            {
                MessageBox.Show("Select a customer first.");
                return;
            }

            try
            {
                string query = @"UPDATE [User] 
                                 SET Full_Name=@FullName,
                                     Email=@Email,
                                     Phone=@Phone,
                                     Address=@Address,
                                     Updated_At=GETDATE()
                                 WHERE Id=@Id AND Role='CUSTOMER'";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@FullName", txtFullName.Text.Trim()),
                    new SqlParameter("@Email", txtEmail.Text.Trim()),
                    new SqlParameter("@Phone", txtPhone.Text.Trim()),
                    new SqlParameter("@Address", txtAddress.Text.Trim()),
                    new SqlParameter("@Id", selectedCustomer.Id)
                };

                int rows = dbHelper.ExecuteNonQuery(query, parameters);

                if (rows > 0)
                {
                    MessageBox.Show("Customer updated successfully.");

                    selectedCustomer = new User(
                        selectedCustomer.Id,
                        txtFullName.Text.Trim(),
                        txtEmail.Text.Trim(),
                        selectedCustomer.PasswordHash,
                        "CUSTOMER",
                        txtAddress.Text.Trim(),
                        txtPhone.Text.Trim(),
                        selectedCustomer.CreatedAt,
                        DateTime.Now
                    );
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }

                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
            }
        }

        // CLEAR BUTTON
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}