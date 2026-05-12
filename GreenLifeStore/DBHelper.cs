using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GreenLifeStore.DataLayer
{
    public class DatabaseHelper
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["GreenLifeStoreDb"].ConnectionString;

        // Public property to expose connection string
        public string ConnectionString => connStr;

        // Execute SELECT query
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // Execute INSERT/UPDATE/DELETE
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Execute scalar query (COUNT, SUM, SCOPE_IDENTITY, etc.)
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        // Hash password using SHA256
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
        public DataTable ExecuteDataTable(string query, SqlParameter[] parameters = null)
        {
            return ExecuteQuery(query, parameters);
        }

        // Verify hashed password
        public bool VerifyHashedPassword(string storedHash, string passwordToCheck)
        {
            string hashOfInput = HashPassword(passwordToCheck);
            return string.Equals(storedHash, hashOfInput, StringComparison.Ordinal);
        }
    }
}