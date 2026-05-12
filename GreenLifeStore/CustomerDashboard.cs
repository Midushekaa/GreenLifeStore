using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GreenLifeStore.Sub_class;
using GreenLifeStore.DataLayer;
using System.Linq;
using System.Collections.Generic;

namespace GreenLifeStore
{
    public partial class CustomerDashboard : Form
    {
        private User currentCustomer;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        // Child forms
        private SearchProductsForm searchProductsForm;
        private CartForm cartForm;
        private TrackOrdersForm trackOrdersForm;
        private ProfileForm profileForm;
        private ReviewForm reviewForm;

        public CustomerDashboard(User customer)
        {
            InitializeComponent();
            currentCustomer = customer;

            // Make form full screen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            // Make panelMain fill the form
            panelMain.Dock = DockStyle.Fill;
            panelMain.AutoScroll = false; // remove scroll bar

            lblTitle.Text = $"Hello, {currentCustomer.FullName}";
            CenterHeaderTitle();
            panelHeader.Resize += (s, e) => CenterHeaderTitle();

            StyleSidebarButtons();
            LoadDashboard();
        }


        private void StyleSidebarButtons()
        {
            Button[] buttons = {
                btnHome, btnSearchProducts, btnCart, btnTrackOrders, btnProfile, btnReview
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
        // Load Customer Dashboard Cards + Charts
        // ===============================
        private void LoadDashboard()
        {
            panelMain.Controls.Clear();
            panelMain.AutoScroll = true;

            int totalOrders = Convert.ToInt32(dbHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM orders WHERE customer_id=@CustomerId",
                new SqlParameter[] { new SqlParameter("@CustomerId", currentCustomer.Id) }));

            int shippedOrders = Convert.ToInt32(dbHelper.ExecuteScalar(
                @"SELECT COUNT(*) FROM orders o
          JOIN order_status os ON o.order_status_id=os.id
          WHERE o.customer_id=@CustomerId AND os.status_name='Completed'",
                new SqlParameter[] { new SqlParameter("@CustomerId", currentCustomer.Id) }));

            int pendingOrders = totalOrders - shippedOrders;

            decimal totalSpent = Convert.ToDecimal(dbHelper.ExecuteScalar(
                "SELECT ISNULL(SUM(total_amount),0) FROM orders WHERE customer_id=@CustomerId",
                new SqlParameter[] { new SqlParameter("@CustomerId", currentCustomer.Id) }));


            int cardWidth = 220, cardHeight = 100, spacing = 30, top = 20, leftStart = 40;

            AddCard("Total Orders", totalOrders.ToString(), Color.SeaGreen, leftStart, top, Properties.Resources.icons8_order_32);
            AddCard("Shipped Orders", shippedOrders.ToString(), Color.RoyalBlue, leftStart + (cardWidth + spacing), top, Properties.Resources.icons8_shipped_40);
            AddCard("Pending Orders", pendingOrders.ToString(), Color.DarkOrange, leftStart + 2 * (cardWidth + spacing), top, Properties.Resources.icons8_data_pending_40);
            Image revenueIcon = Properties.Resources.ResourceManager.GetObject("revenue_icon") as Image;
            AddCard("Total Spent", $"Rs {totalSpent:F2}", Color.IndianRed, leftStart + 3 * (cardWidth + spacing), top, revenueIcon);


            int chartTop = top + cardHeight + 30;
            int chartWidth = panelMain.Width / 2 - 50;
            int chartHeight = 300;

            // Load monthly spending line chart
            int lineChartTop = chartTop + 320;
            int lineChartWidth = panelMain.Width - 80;
            int lineChartHeight = 300;

            LoadMonthlySpendingChart(new Point(40, lineChartTop), new Size(lineChartWidth, lineChartHeight));

            LoadBarChart(new Point(leftStart, chartTop), new Size(chartWidth, chartHeight), totalOrders, shippedOrders, pendingOrders);
            LoadPieChart(new Point(leftStart + chartWidth + spacing, chartTop), new Size(chartWidth, chartHeight), shippedOrders, pendingOrders);
        }

        private void LoadMonthlySpendingChart(Point location, Size size)
        {
            // Check if chart already exists in the panel
            Chart chart;
            if (panelMain.Controls.OfType<Chart>().Any(c => c.Name == "chartMonthlySpending"))
            {
                chart = panelMain.Controls.OfType<Chart>().First(c => c.Name == "chartMonthlySpending");
                chart.Series.Clear(); // clear old data
            }
            else
            {
                chart = new Chart
                {
                    Name = "chartMonthlySpending",
                    Location = location,
                    Size = size,
                    BackColor = Color.White
                };
                panelMain.Controls.Add(chart);

                // Add chart area
                chart.ChartAreas.Add(new ChartArea("SpendingArea"));

                // Add title
                chart.Titles.Add(new Title
                {
                    Text = "Monthly Spending (Last 6 Months)",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Docking = Docking.Top,
                    Alignment = ContentAlignment.MiddleCenter
                });

                // Configure axes
                ChartArea ca = chart.ChartAreas[0];
                ca.AxisY.LabelStyle.Format = "Rs #,##0";
                ca.AxisX.Interval = 1;
                ca.AxisX.IsLabelAutoFit = true;
                ca.AxisX.MajorGrid.Enabled = false;
            }

            // Create series
            Series series = new Series("MonthlySpending")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.MediumPurple,
                IsValueShownAsLabel = true,
                ChartArea = "SpendingArea"
            };

            // --- Sample Data (instead of SQL) ---
            var sampleData = new Dictionary<string, decimal>()
    {
        { "Oct", 1500 },
        { "Nov", 3200 },
        { "Dec", 2100 },
        { "Jan", 4500 },
        { "Feb", 3800 },
        { "Mar", 5000 }
    };

            foreach (var item in sampleData)
            {
                series.Points.AddXY(item.Key, item.Value);
            }

            // Add series to chart
            chart.Series.Add(series);
        }

        private int GetShippedOrdersCount()
        {
            string query = @"
        SELECT COUNT(*) 
        FROM orders o
        JOIN order_status os ON o.order_status_id = os.id
        WHERE os.status_name = @StatusName
          AND o.customer_id = @CustomerId";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@StatusName", "Completed"),
        new SqlParameter("@CustomerId", currentCustomer.Id)
            };

            return Convert.ToInt32(dbHelper.ExecuteScalar(query, parameters));
        }

        // ===============================
        // Add Card
        // ===============================
        private void AddCard(string title, string value, Color color, int x, int y, Image icon)
        {
            Panel card = new Panel
            {
                Width = 220,
                Height = 100,
                BackColor = color,
                Location = new Point(x, y),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, 45),
                AutoSize = true
            };

            PictureBox pic = null;
            if (icon != null)
            {
                pic = new PictureBox
                {
                    Image = icon,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(40, 40),
                    Location = new Point(card.Width - 50, 10),
                    BackColor = Color.Transparent
                };
                card.Controls.Add(pic);
            }

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            panelMain.Controls.Add(card);
        }

        // ===============================
        // Load Charts
        // ===============================
        private void LoadBarChart(Point location, Size size, int totalOrders, int shippedOrders, int pendingOrders)
        {
            Chart chart = new Chart { Location = location, Size = size, BackColor = Color.White };
            chart.ChartAreas.Add(new ChartArea("OrdersArea"));
            Series series = new Series("Orders") { ChartType = SeriesChartType.Column };
            series.Points.AddXY("Total", totalOrders);
            series.Points.AddXY("Shipped", shippedOrders);
            series.Points.AddXY("Pending", pendingOrders);
            chart.Series.Add(series);

            chart.Titles.Add(new Title
            {
                Text = "Orders Overview",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Docking = Docking.Top,
                Alignment = ContentAlignment.MiddleCenter
            });

            panelMain.Controls.Add(chart);
        }

        private void LoadPieChart(Point location, Size size, int shippedOrders, int pendingOrders)
        {
            Chart chart = new Chart { Location = location, Size = size, BackColor = Color.White };
            chart.ChartAreas.Add(new ChartArea("PieArea"));

            Series series = new Series("OrderStatus") { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };
            series.Points.Add(new DataPoint { YValues = new double[] { shippedOrders }, LegendText = "Shipped", Label = $"Shipped: {shippedOrders}" });
            series.Points.Add(new DataPoint { YValues = new double[] { pendingOrders }, LegendText = "Pending", Label = $"Pending: {pendingOrders}" });

            chart.Series.Add(series);
            chart.Legends.Add(new Legend());

            chart.Titles.Add(new Title
            {
                Text = "Order Status Distribution",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Docking = Docking.Top,
                Alignment = ContentAlignment.MiddleCenter
            });

            panelMain.Controls.Add(chart);
        }

        // ===============================
        // Open Forms inside panelMain (Admin Dashboard Style)
        // ===============================
        private void OpenFormInPanel(Form form)
        {
            panelMain.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelMain.Controls.Add(form);
            panelMain.Tag = form;

            form.Show();
        }
        // ===============================
        // Button Events
        // ===============================
        private void btnHome_Click(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void btnSearchProducts_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new SearchProductsForm(currentCustomer.Id));
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new CartForm(currentCustomer.Id));
        }

        private void btnTrackOrders_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new TrackOrdersForm(currentCustomer.Id));
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new ProfileForm(currentCustomer.Id));
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            OpenFormInPanel(new ReviewForm(currentCustomer.Id));
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnNotifications_Click(object sender, EventArgs e)
        {
            int count = GetShippedOrdersCount();

            if (count > 0)
            {
                MessageBox.Show($"📦 You have {count} shipped order(s)!",
                    "Order Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No shipped orders yet.",
                    "Notifications",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}