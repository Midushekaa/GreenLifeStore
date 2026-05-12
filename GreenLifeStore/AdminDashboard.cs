using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GreenLifeStore.Sub_class;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore
{
    public partial class AdminDashboard : Form
    {
        private User currentAdmin;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        private ProductForm productForm;
        private CustomerForm customerForm;
        private OrdersForm ordersForm;
        private ReportsForm reportsForm;
        private DiscountForm discountForm;
        private LowStockNotificationForm lowStockForm;
        private SupplierForm supplierForm;

        public AdminDashboard(User admin)
        {
            InitializeComponent();

            currentAdmin = admin;

            panelMain.AutoScroll = true;

            lblTitle.Text = $"Welcome, {currentAdmin.FullName}";
            CenterHeaderTitle();

            panelHeader.Resize += (s, e) => CenterHeaderTitle();

            StyleSidebarButtons();

            LoadDashboard();
        }


        // ===============================
        // Sidebar Styling
        // ===============================
        private void StyleSidebarButtons()
        {
            Button[] buttons =
            {
                btnDashboard,
                btnCustomers,
                btnProducts,
                btnOrders,
                btnReports,
                btnDiscounts,
                btnSupplier,
                btnLowStock
            };

            foreach (Button btn in buttons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = Color.White;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(15, 0, 0, 0);
                btn.Cursor = Cursors.Hand;

                btn.MouseEnter += (s, e) => btn.BackColor = Color.SeaGreen;
                btn.MouseLeave += (s, e) => btn.BackColor = panelSidebar.BackColor;
            }
        }

        // ===============================
        // Center Header Title
        // ===============================
        private void CenterHeaderTitle()
        {
            lblTitle.Left = (panelHeader.Width - lblTitle.Width) / 2;
        }

        // ===============================
        // Dashboard Loader
        // ===============================
        private void LoadDashboard()
        {
            panelMain.Controls.Clear();

            int cardWidth = 220;
            int cardHeight = 100;
            int spacing = 30;
            int top = 20;
            int leftStart = 40;

            AddCard("Products", GetCount("Product").ToString(),
                Color.SeaGreen, leftStart, top, Properties.Resources.icons8_product_32);

            AddCard("Customers", GetCount("Customer").ToString(),
                Color.RoyalBlue, leftStart + (cardWidth + spacing), top, Properties.Resources.icons8_customer_32);

            AddCard("Orders", GetCount("Orders").ToString(),
                Color.DarkOrange, leftStart + 2 * (cardWidth + spacing), top, Properties.Resources.icons8_order_321);

            Image revenueIcon = Properties.Resources.ResourceManager.GetObject("revenue_icon") as Image;

            AddCard("Revenue", "Rs " + GetRevenue().ToString("N2"),
                Color.IndianRed, leftStart + 3 * (cardWidth + spacing), top, revenueIcon);

            int chartTop = top + cardHeight + 40;
            int chartWidth = panelMain.Width / 2 - 60;
            int chartHeight = 300;
            int chartSpacing = 20;

            LoadBarChart(new Point(leftStart, chartTop), new Size(chartWidth, chartHeight));

            LoadPieChart(new Point(leftStart + chartWidth + chartSpacing, chartTop),
                new Size(chartWidth, chartHeight));

            LoadMonthlyRevenueChart(new Point(leftStart, chartTop + chartHeight + chartSpacing),
                new Size(chartWidth, chartHeight));

            LoadTopProductsChart(new Point(leftStart + chartWidth + chartSpacing,
                chartTop + chartHeight + chartSpacing), new Size(chartWidth, chartHeight));
        }

        // ===============================
        // Embed Form into panelMain
        // ===============================
        private void OpenFormInPanel<T>(ref T form, Func<T> factory) where T : Form
        {
            // Dispose previous form
            if (form != null)
                form.Dispose();

            form = factory();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);

            form.Show();
        }

        // ===============================
        // Card Creator
        // ===============================
        private Panel AddCard(string title, string value, Color color, int left, int top, Image icon)
        {
            Panel panel = new Panel
            {
                Size = new Size(220, 100),
                Location = new Point(left, top),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox pic = new PictureBox
            {
                Image = icon,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(160, 25),
                Size = new Size(50, 50)
            };

            Label lblTitle = new Label
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(15, 15),
                AutoSize = true
            };

            Label lblValue = new Label
            {
                Text = value,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(15, 45),
                AutoSize = true
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblValue);
            panel.Controls.Add(pic);

            panelMain.Controls.Add(panel);

            return panel;
        }

        // ===============================
        // Chart methods (same as before)
        // ===============================
        private Chart CreateChart(Point location, Size size, string title)
        {
            Chart chart = new Chart
            {
                Location = location,
                Size = size,
                BackColor = Color.WhiteSmoke
            };

            chart.ChartAreas.Add(new ChartArea("ChartArea1"));

            chart.Titles.Add(new Title
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            });

            return chart;
        }
        private void OpenFormModal<T>(ref T form, Func<T> factory) where T : Form
        {
            if (form == null || form.IsDisposed)
                form = factory();

            form.ShowDialog();
        }

        private void LoadBarChart(Point location, Size size)
        {
            Chart chart = CreateChart(location, size, "System Overview");

            Series series = new Series("Totals") { ChartType = SeriesChartType.Column };

            series.Points.AddXY("Products", GetCount("Product"));
            series.Points.AddXY("Customers", GetCount("Customer"));
            series.Points.AddXY("Orders", GetCount("Orders"));
            series.Points.AddXY("Discounts", GetCount("Discount"));

            chart.Series.Add(series);

            panelMain.Controls.Add(chart);
        }

        private void LoadPieChart(Point location, Size size)
        {
            Chart chart = CreateChart(location, size, "Order Status Distribution");

            Series series = new Series("OrderStatus") { ChartType = SeriesChartType.Pie };

            DataTable dt = dbHelper.ExecuteQuery(@"
                SELECT os.Status_Name, COUNT(*) AS Total
                FROM Orders o
                JOIN Order_Status os ON o.Order_Status_Id = os.Id
                GROUP BY os.Status_Name");

            foreach (DataRow row in dt.Rows)
                series.Points.AddXY(row["Status_Name"], row["Total"]);

            chart.Series.Add(series);
            panelMain.Controls.Add(chart);
        }

        private void LoadMonthlyRevenueChart(Point location, Size size)
        {
            Chart chart = CreateChart(location, size, "Monthly Revenue");

            Series series = new Series("Revenue") { ChartType = SeriesChartType.Column, BorderWidth = 3 };

            DataTable dt = dbHelper.ExecuteQuery(@"
                SELECT DATENAME(MONTH, Order_Date) AS MonthName,
                       ISNULL(SUM(Total_Amount),0) AS Total
                FROM Orders
                GROUP BY DATENAME(MONTH, Order_Date), MONTH(Order_Date)
                ORDER BY MONTH(Order_Date)");

            foreach (DataRow row in dt.Rows)
                series.Points.AddXY(row["MonthName"], row["Total"]);

            chart.Series.Add(series);
            chart.ChartAreas[0].AxisY.Title = "Revenue (LKR)";
            chart.ChartAreas[0].AxisX.Title = "Month";

            panelMain.Controls.Add(chart);
        }

        private void LoadTopProductsChart(Point location, Size size)
        {
            Chart chart = CreateChart(location, size, "Top Products");

            Series series = new Series("Sales") { ChartType = SeriesChartType.Bar };

            DataTable dt = dbHelper.ExecuteQuery(@"
                SELECT TOP 5 p.Name, SUM(o.Quantity) AS TotalSold
                FROM Orders o
                JOIN Product p ON o.Product_Id = p.Id
                GROUP BY p.Name
                ORDER BY TotalSold DESC");

            foreach (DataRow row in dt.Rows)
                series.Points.AddXY(row["Name"], row["TotalSold"]);

            chart.Series.Add(series);

            panelMain.Controls.Add(chart);
        }

        // ===============================
        // Database Methods
        // ===============================
        private int GetCount(string table)
        {
            string query;

            if (table == "Customer")
                query = "SELECT COUNT(*) FROM [User] WHERE Role='CUSTOMER'";
            else if (table == "Admin")
                query = "SELECT COUNT(*) FROM [User] WHERE Role='ADMIN'";
            else
                query = "SELECT COUNT(*) FROM " + table;

            return Convert.ToInt32(dbHelper.ExecuteScalar(query));
        }

        private decimal GetRevenue()
        {
            return Convert.ToDecimal(
                dbHelper.ExecuteScalar("SELECT ISNULL(SUM(Total_Amount),0) FROM Orders"));
        }

        // ===============================
        // Button Events
        // ===============================
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void btnCustomers_Click_1(object sender, EventArgs e)
        {
            OpenFormInPanel(ref customerForm, () => new CustomerForm());
        }

        private void btnProducts_Click_1(object sender, EventArgs e)
        {
            OpenFormInPanel(ref productForm, () => new ProductForm());
        }

        private void btnOrders_Click_1(object sender, EventArgs e)
        {
            OpenFormInPanel(ref ordersForm, () => new OrdersForm());
        }

        private void btnReports_Click_1(object sender, EventArgs e)
        {
            OpenFormInPanel(ref reportsForm, () => new ReportsForm());
        }

        private void btnDiscounts_Click_1(object sender, EventArgs e)
        {
            OpenFormInPanel(ref discountForm, () => new DiscountForm());
        }

        private void btnLowStock_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(ref lowStockForm, () => new LowStockNotificationForm());
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(ref supplierForm, () => new SupplierForm());
        }

        

        private void btnLog_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void close_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}