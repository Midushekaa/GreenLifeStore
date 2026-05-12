using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;
using GreenLifeStore.Sub_class;

namespace GreenLifeStore
{
    public partial class ReviewForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        private int customerId;

        public ReviewForm(int loggedInCustomerId)
        {
            InitializeComponent();
            customerId = loggedInCustomerId;

            // Fixed Form Size
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1000, 600);
        }

        private void ReviewForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

     
        private void LoadProducts()
        {
            try
            {
                string query = "SELECT Id, Name FROM Product";
                DataTable dt = db.ExecuteQuery(query);

                cmbProducts.DisplayMember = "Name";
                cmbProducts.ValueMember = "Id";
                cmbProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }


        private void LoadReviews(int productId)
        {
            try
            {
                string query = @"
                    SELECT r.Id,
                           p.Name AS ProductName,
                           c.Full_Name AS CustomerName,
                           r.Rating,
                           r.Comment,
                           r.Created_At
                    FROM Review r
                    JOIN [User] c ON r.Customer_Id = c.Id
                    JOIN Product p ON r.Product_Id = p.Id
                    WHERE r.Product_Id = @pid
                    ORDER BY r.Created_At DESC";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@pid", productId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                dgvReviews.DataSource = dt;
                dgvReviews.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message);
            }
        }

        private void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedIndex != -1)
            {
                int productId =
                    Convert.ToInt32(cmbProducts.SelectedValue);

                LoadReviews(productId);
            }
        }

   
        private void btnSubmitReview_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                MessageBox.Show("Please enter a comment.");
                return;
            }

            try
            {
                Review review = new Review
                {
                    ProductId = Convert.ToInt32(cmbProducts.SelectedValue),
                    CustomerId = customerId,
                    Rating = (int)nudRating.Value,
                    Comment = txtComment.Text.Trim()
                };

                string query = @"
                    INSERT INTO Review 
                    (Product_Id, Customer_Id, Rating, Comment, Created_At)
                    VALUES (@pid, @cid, @rating, @comment, GETDATE())";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@pid", review.ProductId),
                    new SqlParameter("@cid", review.CustomerId),
                    new SqlParameter("@rating", review.Rating),
                    new SqlParameter("@comment", review.Comment)
                };

                db.ExecuteNonQuery(query, parameters);

                MessageBox.Show("Review submitted successfully!");

                txtComment.Clear();
                nudRating.Value = 1;

                LoadReviews(review.ProductId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting review: " + ex.Message);
            }
        }


        private void btnDeleteReview_Click(object sender, EventArgs e)
        {
            if (dgvReviews.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review to delete.");
                return;
            }

            int reviewId =
                Convert.ToInt32(dgvReviews.SelectedRows[0].Cells["Id"].Value);

            int productId =
                Convert.ToInt32(cmbProducts.SelectedValue);

            DialogResult dr = MessageBox.Show(
                "Are you sure you want to delete this review?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    string query =
                        "DELETE FROM Review WHERE Id=@id AND Customer_Id=@cid";

                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@id", reviewId),
                        new SqlParameter("@cid", customerId)
                    };

                    int rows = db.ExecuteNonQuery(query, parameters);

                    if (rows > 0)
                    {
                        MessageBox.Show("Review deleted successfully!");
                        LoadReviews(productId);
                    }
                    else
                    {
                        MessageBox.Show("You can only delete your own review.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting review: " + ex.Message);
                }
            }
        }
    }
}