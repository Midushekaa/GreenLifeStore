using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GreenLifeStore.DataLayer;
using GreenLifeStore.Sub_class;

namespace GreenLifeStore
{
    public partial class LowStockNotificationForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();
        private List<LowStockNotification> notifications;

        public LowStockNotificationForm()
        {
            InitializeComponent();
            SetFixedSize();
            CheckAndInsertLowStockNotifications();
            LoadNotifications();
        }

        private void SetFixedSize()
        {
            this.Text = "Low Stock Notifications";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1459, 710);
        }

        private void CheckAndInsertLowStockNotifications()
        {
            string query = @"
                -- Update stock_status
                UPDATE inventory
                SET stock_status = 
                    CASE 
                        WHEN quantity_in_stock = 0 THEN 'OUT OF STOCK'
                        WHEN quantity_in_stock <= reorder_level THEN 'LOW STOCK'
                        ELSE 'IN STOCK'
                    END;

                -- Insert notifications for low stock products
                INSERT INTO low_stock_notification (product_id, message, notified_at, created_at, updated_at)
                SELECT i.product_id,
                       'Stock is below reorder level',
                       GETDATE(),
                       GETDATE(),
                       GETDATE()
                FROM inventory i
                WHERE i.quantity_in_stock <= i.reorder_level
                AND NOT EXISTS (
                    SELECT 1 
                    FROM low_stock_notification l
                    WHERE l.product_id = i.product_id
                );";

            db.ExecuteNonQuery(query);
        }

        private void LoadNotifications()
        {
            notifications = new List<LowStockNotification>();
            string query = @"
                SELECT l.id, l.product_id, p.name AS product_name, l.message, l.notified_at, l.created_at, l.updated_at
                FROM low_stock_notification l
                INNER JOIN product p ON l.product_id = p.id
                ORDER BY l.notified_at DESC";

            DataTable dt = db.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                notifications.Add(new LowStockNotification
                {
                    Id = Convert.ToInt32(row["id"]),
                    ProductId = Convert.ToInt32(row["product_id"]),
                    ProductName = row["product_name"].ToString(), 
                    Message = row["message"].ToString(),
                    NotifiedAt = row["notified_at"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["notified_at"]),
                    CreatedAt = Convert.ToDateTime(row["created_at"]),
                    UpdatedAt = Convert.ToDateTime(row["updated_at"])
                });
            }
            BindNotificationsToGrid();
        }
        private void BindNotificationsToGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Message", typeof(string));
            dt.Columns.Add("Notified At", typeof(DateTime));

            foreach (var n in notifications)
            {
                dt.Rows.Add(n.Id, n.ProductName, n.Message, n.NotifiedAt);
            }

            dgvNotifications.DataSource = dt;
            dgvNotifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvNotifications.Columns.Contains("ID"))
                dgvNotifications.Columns["ID"].Visible = false;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CheckAndInsertLowStockNotifications();
            LoadNotifications();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}