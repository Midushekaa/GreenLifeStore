using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore
{
    public partial class TrackOrdersForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        private int customerId;

        public TrackOrdersForm(int custId)
        {
            InitializeComponent();
            customerId = custId;

            
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1000, 600);


            this.Load += TrackOrdersForm_Load;
        }

        private void TrackOrdersForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                string query = @"
                SELECT 
                    o.id AS OrderID,
                    p.name AS [Product],
                    o.quantity AS [Quantity],
                    o.price AS [Price],
                    o.total_amount AS [Total],
                    os.status_name AS [Status],
                    o.order_date AS [Order Date]
                FROM orders o
                INNER JOIN product p ON o.product_id = p.id
                INNER JOIN order_status os ON o.order_status_id = os.id
                WHERE o.customer_id = @cid
                ORDER BY o.order_date DESC";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@cid", customerId)
                };

                DataTable dt = db.ExecuteQuery(query, parameters);

                dgvOrders.DataSource = dt;

                
                if (dgvOrders.Columns["OrderID"] != null)
                    dgvOrders.Columns["OrderID"].Visible = false;

                if (dgvOrders.Columns["Price"] != null)
                    dgvOrders.Columns["Price"].DefaultCellStyle.Format = "\"Rs\" #,##0.00";

                if (dgvOrders.Columns["Total"] != null)
                    dgvOrders.Columns["Total"].DefaultCellStyle.Format = "\"Rs\" #,##0.00";

              
                if (dgvOrders.Columns["Product"] != null)
                    dgvOrders.Columns["Product"].Width = 300;

                if (dgvOrders.Columns["Status"] != null)
                    dgvOrders.Columns["Status"].Width = 120;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No orders found for this customer.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}