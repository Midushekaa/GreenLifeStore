using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GreenLifeStore.Sub_class
{
    public class User
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; } // ADMIN or CUSTOMER
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

      
        public User(int id, string fullName, string email, string passwordHash, string role = "CUSTOMER",
                    string address = "", string phone = "", DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Address = address;
            Phone = phone;
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt ?? DateTime.Now;
        }

     
        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }

     
        public static User GetUserByEmail(string email, SqlConnection conn)
        {
            string query = "SELECT * FROM [User] WHERE Email=@Email";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            reader.GetInt32(reader.GetOrdinal("Id")),
                            reader.GetString(reader.GetOrdinal("Full_Name")),
                            reader.GetString(reader.GetOrdinal("Email")),
                            reader.GetString(reader.GetOrdinal("Password")),
                            reader.GetString(reader.GetOrdinal("Role")),
                            reader["Address"]?.ToString() ?? "",
                            reader["Phone"]?.ToString() ?? "",
                            reader.GetDateTime(reader.GetOrdinal("Created_At")),
                            reader.GetDateTime(reader.GetOrdinal("Updated_At"))
                        );
                    }
                }
            }
            return null; 
        }

        public void UpdatePassword(string newPassword, SqlConnection conn)
        {
            string hashed = HashPassword(newPassword);
            string query = "UPDATE [User] SET password=@Password, updated_at=GETDATE() WHERE Id=@Id";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Password", hashed);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    PasswordHash = hashed;
                    UpdatedAt = DateTime.Now;
                }
                else
                {
                    throw new Exception("Password update failed.");
                }
            }
        }


        public static User Login(string username, string role, string password, SqlConnection conn)
        {
            string query = "SELECT * FROM [User] WHERE User_Name=@UserName AND UPPER(Role)=@Role";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Role", role.ToUpper());

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedHash = reader.GetString(reader.GetOrdinal("Password"));
                        if (HashPassword(password) == storedHash)
                        {
                            return new User(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Full_Name")),
                                reader.GetString(reader.GetOrdinal("Email")),
                                storedHash,
                                reader.GetString(reader.GetOrdinal("Role")),
                                reader["Address"]?.ToString() ?? "",
                                reader["Phone"]?.ToString() ?? "",
                                reader.GetDateTime(reader.GetOrdinal("Created_At")),
                                reader.GetDateTime(reader.GetOrdinal("Updated_At"))
                            );
                        }
                    }
                }
            }
            return null; // Login failed
        }
    }
}