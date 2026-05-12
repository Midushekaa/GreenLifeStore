using System;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore
{
    public partial class ProfileForm : Form
    {
        private DatabaseHelper db = new DatabaseHelper();
        private int customerId;

        public ProfileForm(int custId)
        {
            InitializeComponent();
            customerId = custId;
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            var dt = db.ExecuteQuery("SELECT * FROM [User] WHERE Id=@id",
                new System.Data.SqlClient.SqlParameter[] {
                    new System.Data.SqlClient.SqlParameter("@id", customerId)
                });

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtName.Text = row["Full_Name"].ToString();
                txtUsername.Text = row["User_Name"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPassword.Text = row["Password"].ToString();
                txtAddress.Text = row["Address"].ToString();
                txtPhone.Text = row["Phone"].ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields (Name, Username, Email, Password).",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

      
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"UPDATE [User]
                     SET Full_Name=@n,
                         User_Name=@u,
                         Email=@e,
                         Password=@pw,
                         Address=@a,
                         Phone=@p,
                         Updated_At=GETDATE()
                     WHERE Id=@id";

            try
            {
                db.ExecuteNonQuery(query, new System.Data.SqlClient.SqlParameter[]
                {
            new System.Data.SqlClient.SqlParameter("@n", txtName.Text),
            new System.Data.SqlClient.SqlParameter("@u", txtUsername.Text),
            new System.Data.SqlClient.SqlParameter("@e", txtEmail.Text),
            new System.Data.SqlClient.SqlParameter("@pw", txtPassword.Text),
            new System.Data.SqlClient.SqlParameter("@a", txtAddress.Text ?? string.Empty),
            new System.Data.SqlClient.SqlParameter("@p", txtPhone.Text ?? string.Empty),
            new System.Data.SqlClient.SqlParameter("@id", customerId)
                });

                MessageBox.Show("Profile Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}