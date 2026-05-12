using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;
using GreenLifeStore.Sub_class;

namespace GreenLifeStore
{
    public partial class DiscountForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        public DiscountForm()
        {
            InitializeComponent();
            SetFixedSize();
        }

        private void DiscountForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadDiscounts();
        }

        private void SetFixedSize()
        {
            this.Text = "Discount Management";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 600);
        }

        private void LoadProducts()
        {
            string query = "SELECT Id, Name FROM Product";
            DataTable dt = db.ExecuteQuery(query);

            cmbProducts.DisplayMember = "Name";
            cmbProducts.ValueMember = "Id";
            cmbProducts.DataSource = dt;
        }

        private void LoadDiscounts()
        {
            DataTable dt = Discount.LoadDiscounts(db);
            dgvDiscounts.DataSource = dt;
            dgvDiscounts.Columns["Id"].Visible = false;
            dgvDiscounts.Columns["Product"].Width = 200;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedIndex == -1)
            {
                MessageBox.Show("Select a product!");
                return;
            }

            int productId = Convert.ToInt32(cmbProducts.SelectedValue);
            decimal discountPercentage = nudDiscount.Value;
            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date;

            if (endDate < startDate)
            {
                MessageBox.Show("End date cannot be earlier than start date!");
                return;
            }

            try
            {
                Discount newDiscount = new Discount
                {
                    ProductId = productId,
                    DiscountPercentage = discountPercentage,
                    StartDate = startDate,
                    EndDate = endDate
                };

                newDiscount.ApplyDiscount(db);

                MessageBox.Show("Discount applied successfully!");
                LoadDiscounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying discount: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDiscounts.CurrentRow == null) return;

            int discountId = Convert.ToInt32(dgvDiscounts.CurrentRow.Cells["Id"].Value);
            Discount.DeleteDiscount(discountId, db);
            LoadDiscounts();
            MessageBox.Show("Discount deleted!");
        }
    }
}
