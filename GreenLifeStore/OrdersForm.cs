using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;
using GreenLifeStore.Sub_class;

namespace GreenLifeStore
{
    public partial class OrdersForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        private List<Orders> ordersList;
        private DataTable statusesTable;
        private Dictionary<int, string> statusDict; 

        public OrdersForm()
        {
            InitializeComponent();
            SetFixedSize();
            LoadOrderStatuses(); 
            LoadOrders();        
        }

        private void SetFixedSize()
        {
            this.Text = "Order Management";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1000, 600);
        }
        private void LoadOrderStatuses()
        {
            string query = "SELECT Id, Status_Name FROM order_status";
            statusesTable = db.ExecuteQuery(query);

            if (!statusesTable.Columns.Contains("Id") || !statusesTable.Columns.Contains("Status_Name"))
                throw new Exception("Statuses table missing expected columns.");

            statusDict = new Dictionary<int, string>();
            foreach (DataRow row in statusesTable.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string name = row["Status_Name"].ToString();
                statusDict[id] = name;
            }

            cmbStatus.DisplayMember = "Status_Name";
            cmbStatus.ValueMember = "Id";
            cmbStatus.DataSource = statusesTable;
            cmbStatus.SelectedIndex = -1;
        }

     
        private void LoadOrders()
        {
            ordersList = new List<Orders>();

            string query = @"
        SELECT Id, Customer_Id, Order_Status_Id, Product_Id, Quantity, Price, Order_Date
        FROM Orders
        ORDER BY Id DESC";

            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                ordersList.Add(new Orders
                {
                    Id = Convert.ToInt32(row["Id"]),
                    CustomerId = Convert.ToInt32(row["Customer_Id"]),
                    OrderStatusId = Convert.ToInt32(row["Order_Status_Id"]),
                    ProductId = Convert.ToInt32(row["Product_Id"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    Price = Convert.ToDecimal(row["Price"]),
                    OrderDate = Convert.ToDateTime(row["Order_Date"])
                });
            }

            BindOrdersToGrid();
        }

        private void BindOrdersToGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Order ID", typeof(int));
            dt.Columns.Add("Customer ID", typeof(int));
            dt.Columns.Add("Order Date", typeof(DateTime));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Status", typeof(string));

            foreach (var order in ordersList)
            {
                string statusName = GetStatusName(order.OrderStatusId);
                dt.Rows.Add(order.Id, order.CustomerId, order.OrderDate, order.TotalAmount, statusName);
            }

            dgvOrders.DataSource = dt;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.ClearSelection();
        }

        private string GetStatusName(int statusId)
        {
            return statusDict != null && statusDict.ContainsKey(statusId)
                ? statusDict[statusId]
                : "Unknown";
        }


        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtOrderId.Text = dgvOrders.Rows[e.RowIndex].Cells["Order ID"].Value.ToString();
            string statusText = dgvOrders.Rows[e.RowIndex].Cells["Status"].Value.ToString();

            
            cmbStatus.SelectedIndex = cmbStatus.FindStringExact(statusText);
        }


        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (!int.TryParse(txtOrderId.Text, out int orderId))
                {
                    MessageBox.Show("Select an order first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (cmbStatus.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a valid status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int statusId = Convert.ToInt32(cmbStatus.SelectedValue);

                string updateQuery = "UPDATE Orders SET Order_Status_Id=@StatusId, Updated_At=GETDATE() WHERE Id=@OrderId";

                db.ExecuteNonQuery(updateQuery, new SqlParameter[]
                {
            new SqlParameter("@StatusId", statusId),
            new SqlParameter("@OrderId", orderId)
                });

                MessageBox.Show("Order status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadOrders();
                ClearFields();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error while updating order: " + sqlEx.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating order: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtOrderId.Clear();
            cmbStatus.SelectedIndex = -1;
            dgvOrders.ClearSelection();
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadOrders();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtOrderId.Text, out int orderId))
            {
                MessageBox.Show("Select an order first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"
                SELECT o.id AS [Order ID],
                       p.Name AS [Product],
                       o.quantity AS [Quantity],
                       o.price AS [Price],
                       (o.quantity * o.price) AS [Subtotal]
                FROM Orders o
                INNER JOIN Product p ON o.product_id = p.Id
                WHERE o.id = @OrderId";

            DataTable dt = db.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@OrderId", orderId) });

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No products found for this order.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form detailsForm = new Form
            {
                Text = $"Order #{orderId} Details",
                Size = new System.Drawing.Size(600, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            DataGridView dgvDetails = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = dt,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            detailsForm.Controls.Add(dgvDetails);
            detailsForm.ShowDialog();
        }
    }
}