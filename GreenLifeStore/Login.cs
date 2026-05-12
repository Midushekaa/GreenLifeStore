using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using GreenLifeStore.Sub_class;
using GreenLifeStore.UI;
using System.Configuration;

namespace GreenLifeStore
{
    public partial class LoginForm : Form
    {
        private readonly string _connStr = ConfigurationManager.ConnectionStrings["GreenLifeStoreDb"].ConnectionString;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set role options
            comboRole.Items.Clear();
            comboRole.Items.Add("ADMIN");
            comboRole.Items.Add("CUSTOMER");
            comboRole.SelectedIndex = 0; 
        }

        // Show/hide password
        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        // Close the app
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Open registration form
        private void login_registerBtn_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        // Forgot password
        private void forgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.Show();
            this.Hide();
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            string username = login_username.Text.Trim();
            string password = login_password.Text.Trim();
            string role = comboRole.SelectedItem?.ToString().Trim().ToUpper();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    conn.Open();

                    // Query user by username and role (case-insensitive)
                    string query = @"SELECT Id, Full_Name, Email, Password, User_Name, Role 
                                     FROM [User] 
                                     WHERE User_Name=@UserName AND UPPER(Role)=@Role";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Role", role);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        string fullName = reader.GetString(1);
                        string email = reader.GetString(2);
                        string storedHash = reader.GetString(3);
                        string userNameDB = reader.GetString(4);

                        if (VerifyHashedPassword(storedHash, password))
                        {
                            if (role == "ADMIN")
                            {
                                User admin = new User(userId, fullName, email, storedHash, "ADMIN");
                                AdminDashboard adminDashboard = new AdminDashboard(admin);
                                adminDashboard.Show();
                                this.Hide();
                            }
                            else 
                            {
                                
                                User customer = new User(userId, fullName, email, storedHash, "CUSTOMER");
                                CustomerDashboard customerDashboard = new CustomerDashboard(customer);
                                customerDashboard.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found with this username and role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // SHA-256 password verification
        private bool VerifyHashedPassword(string storedHash, string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha.ComputeHash(bytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "");

                return string.Equals(hashString, storedHash, StringComparison.OrdinalIgnoreCase);
            }
        }
      
    }
}