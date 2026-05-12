using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using GreenLifeStore.Sub_class;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore.UI
{
    public partial class RegisterForm : Form
    {
        private DatabaseHelper db = new DatabaseHelper();

        public RegisterForm()
        {
            InitializeComponent();
        }

        
        private void register_btn_Click(object sender, EventArgs e)
        {
            string fullName = register_fullName.Text.Trim();
            string email = register_email.Text.Trim();
            string password = register_password.Text.Trim();
            string cPassword = register_cPassword.Text.Trim();

            
            if (string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cPassword))
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != cPassword)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                
                string checkQuery = "SELECT COUNT(*) FROM [User] WHERE Email=@Email OR Full_Name=@FullName";
                int exists = Convert.ToInt32(db.ExecuteScalar(checkQuery, new SqlParameter[]
                {
                    new SqlParameter("@Email", email),
                    new SqlParameter("@FullName", fullName)
                }));

                if (exists > 0)
                {
                    MessageBox.Show("Full Name or Email already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Generate a unique username (from full name + random number)
                string userName;
                Random rnd = new Random();
                do
                {
                    userName = fullName.Replace(" ", "").ToLower() + rnd.Next(100, 999);
                    string checkUserNameQuery = "SELECT COUNT(*) FROM [User] WHERE User_Name=@UserName";
                    exists = Convert.ToInt32(db.ExecuteScalar(checkUserNameQuery, new SqlParameter[]
                    {
                        new SqlParameter("@UserName", userName)
                    }));
                } while (exists > 0);

                
                string hashedPassword = db.HashPassword(password);

                
                string insertQuery = @"INSERT INTO [User] 
                                       (Full_Name, User_Name, Email, Password, Role, Created_At, Updated_At)
                                       VALUES (@FullName, @UserName, @Email, @Password, @Role, @CreatedAt, @UpdatedAt)";

                db.ExecuteNonQuery(insertQuery, new SqlParameter[]
                {
                    new SqlParameter("@FullName", fullName),
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Password", hashedPassword), 
                    new SqlParameter("@Role", "CUSTOMER"),
                    new SqlParameter("@CreatedAt", DateTime.Now),
                    new SqlParameter("@UpdatedAt", DateTime.Now)
                });

                MessageBox.Show($"Registration successful!\nYour username: {userName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Login form
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Show/Hide Password
        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            register_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
            register_cPassword.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        // Close form
        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Open Login form
        private void register_loginBtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}