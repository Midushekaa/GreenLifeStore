using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using GreenLifeStore.Sub_class;

namespace GreenLifeStore
{
    public partial class ForgotPasswordForm : Form
    {
        private readonly string _connStr =
            ConfigurationManager.ConnectionStrings["GreenLifeStoreDb"].ConnectionString;

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        // RESET PASSWORD
        private void btnReset_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // INPUT VALIDATION
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill all fields.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();

                    // CHECK EMAIL
                    string checkQuery = "SELECT COUNT(*) FROM [user] WHERE email=@Email";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", email);

                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists == 0)
                        {
                            MessageBox.Show("Email not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // HASH PASSWORD
                    string hashedPassword = HashPassword(newPassword);

                    // UPDATE PASSWORD
                    string updateQuery =
                        "UPDATE [user] SET password=@Password WHERE email=@Email";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@Email", email);
                        updateCmd.Parameters.AddWithValue("@Password", hashedPassword);

                        int rows = updateCmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            // CALL HELPER CLASS
                            EmailHelper.SendPasswordResetConfirmation(email);

                            this.Close();
                            new LoginForm().Show();
                        }
                        else
                        {
                            MessageBox.Show("Password reset failed.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);

                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;

            txtNewPassword.PasswordChar = show ? '\0' : '*';
            txtConfirmPassword.PasswordChar = show ? '\0' : '*';
        }

        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm().Show();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}