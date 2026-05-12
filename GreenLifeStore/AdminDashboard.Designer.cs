using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GreenLifeStore
{
    partial class AdminDashboard
    {
        private IContainer components = null;

        // Panels
        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelMain;
        private System.Windows.Forms.DataVisualization.Charting.Chart salesChart;

        // Labels
        private Label lblTitle;
        private Label close;





        // PictureBoxes
        private PictureBox pictureLogo;
        private PictureBox pictureDashboardIcon;
        private PictureBox pictureCustomersIcon;
        private PictureBox pictureProductsIcon;
        private PictureBox pictureOrdersIcon;
        private PictureBox pictureReportsIcon;
        private PictureBox pictureDiscountIcon;
        private PictureBox pictureLowStockIcon;

        // Buttons
        private Button btnDashboard;
        private Button btnCustomers;
        private Button btnProducts;
        private Button btnOrders;
        private Button btnReports;
        private Button btnDiscounts;
        private Button btnLowStock;
        private Button btnLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSupplier = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.pictureDashboardIcon = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pictureCustomersIcon = new System.Windows.Forms.PictureBox();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.pictureProductsIcon = new System.Windows.Forms.PictureBox();
            this.btnProducts = new System.Windows.Forms.Button();
            this.pictureOrdersIcon = new System.Windows.Forms.PictureBox();
            this.btnOrders = new System.Windows.Forms.Button();
            this.pictureReportsIcon = new System.Windows.Forms.PictureBox();
            this.btnReports = new System.Windows.Forms.Button();
            this.pictureDiscountIcon = new System.Windows.Forms.PictureBox();
            this.btnDiscounts = new System.Windows.Forms.Button();
            this.pictureLowStockIcon = new System.Windows.Forms.PictureBox();
            this.btnLowStock = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDashboardIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCustomersIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProductsIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOrdersIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReportsIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDiscountIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLowStockIcon)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.DarkGreen;
            this.panelSidebar.Controls.Add(this.pictureBox1);
            this.panelSidebar.Controls.Add(this.btnSupplier);
            this.panelSidebar.Controls.Add(this.pictureLogo);
            this.panelSidebar.Controls.Add(this.pictureDashboardIcon);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.pictureCustomersIcon);
            this.panelSidebar.Controls.Add(this.btnCustomers);
            this.panelSidebar.Controls.Add(this.pictureProductsIcon);
            this.panelSidebar.Controls.Add(this.btnProducts);
            this.panelSidebar.Controls.Add(this.pictureOrdersIcon);
            this.panelSidebar.Controls.Add(this.btnOrders);
            this.panelSidebar.Controls.Add(this.pictureReportsIcon);
            this.panelSidebar.Controls.Add(this.btnReports);
            this.panelSidebar.Controls.Add(this.pictureDiscountIcon);
            this.panelSidebar.Controls.Add(this.btnDiscounts);
            this.panelSidebar.Controls.Add(this.pictureLowStockIcon);
            this.panelSidebar.Controls.Add(this.btnLowStock);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(308, 719);
            this.panelSidebar.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureBox1.Image = global::GreenLifeStore.Properties.Resources.icons8_supplier_40;
            this.pictureBox1.Location = new System.Drawing.Point(50, 571);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // btnSupplier
            // 
            this.btnSupplier.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSupplier.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnSupplier.Location = new System.Drawing.Point(100, 571);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Size = new System.Drawing.Size(194, 54);
            this.btnSupplier.TabIndex = 15;
            this.btnSupplier.Text = "Suppliers";
            this.btnSupplier.UseVisualStyleBackColor = false;
            this.btnSupplier.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // pictureLogo
            // 
            this.pictureLogo.BackColor = System.Drawing.Color.White;
            this.pictureLogo.Image = global::GreenLifeStore.Properties.Resources.greenlife_logo11;
            this.pictureLogo.Location = new System.Drawing.Point(90, 20);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(98, 59);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 0;
            this.pictureLogo.TabStop = false;
            // 
            // pictureDashboardIcon
            // 
            this.pictureDashboardIcon.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureDashboardIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_home_32;
            this.pictureDashboardIcon.Location = new System.Drawing.Point(50, 150);
            this.pictureDashboardIcon.Name = "pictureDashboardIcon";
            this.pictureDashboardIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureDashboardIcon.TabIndex = 1;
            this.pictureDashboardIcon.TabStop = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.DarkGreen;
            this.btnDashboard.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnDashboard.Location = new System.Drawing.Point(100, 150);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(194, 51);
            this.btnDashboard.TabIndex = 2;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click_1);
            // 
            // pictureCustomersIcon
            // 
            this.pictureCustomersIcon.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureCustomersIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_customer_32;
            this.pictureCustomersIcon.Location = new System.Drawing.Point(50, 220);
            this.pictureCustomersIcon.Name = "pictureCustomersIcon";
            this.pictureCustomersIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureCustomersIcon.TabIndex = 3;
            this.pictureCustomersIcon.TabStop = false;
            // 
            // btnCustomers
            // 
            this.btnCustomers.BackColor = System.Drawing.Color.DarkGreen;
            this.btnCustomers.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnCustomers.Location = new System.Drawing.Point(100, 220);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(194, 52);
            this.btnCustomers.TabIndex = 4;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click_1);
            // 
            // pictureProductsIcon
            // 
            this.pictureProductsIcon.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureProductsIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_product_32;
            this.pictureProductsIcon.Location = new System.Drawing.Point(50, 290);
            this.pictureProductsIcon.Name = "pictureProductsIcon";
            this.pictureProductsIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureProductsIcon.TabIndex = 5;
            this.pictureProductsIcon.TabStop = false;
            // 
            // btnProducts
            // 
            this.btnProducts.BackColor = System.Drawing.Color.DarkGreen;
            this.btnProducts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnProducts.Location = new System.Drawing.Point(100, 290);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(194, 50);
            this.btnProducts.TabIndex = 6;
            this.btnProducts.Text = "Products";
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click_1);
            // 
            // pictureOrdersIcon
            // 
            this.pictureOrdersIcon.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureOrdersIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_order_32;
            this.pictureOrdersIcon.Location = new System.Drawing.Point(50, 360);
            this.pictureOrdersIcon.Name = "pictureOrdersIcon";
            this.pictureOrdersIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureOrdersIcon.TabIndex = 7;
            this.pictureOrdersIcon.TabStop = false;
            // 
            // btnOrders
            // 
            this.btnOrders.BackColor = System.Drawing.Color.DarkGreen;
            this.btnOrders.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnOrders.Location = new System.Drawing.Point(100, 360);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(194, 50);
            this.btnOrders.TabIndex = 8;
            this.btnOrders.Text = "Orders";
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click_1);
            // 
            // pictureReportsIcon
            // 
            this.pictureReportsIcon.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureReportsIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_report_32;
            this.pictureReportsIcon.Location = new System.Drawing.Point(50, 430);
            this.pictureReportsIcon.Name = "pictureReportsIcon";
            this.pictureReportsIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureReportsIcon.TabIndex = 9;
            this.pictureReportsIcon.TabStop = false;
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.DarkGreen;
            this.btnReports.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnReports.Location = new System.Drawing.Point(100, 430);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(194, 50);
            this.btnReports.TabIndex = 10;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click_1);
            // 
            // pictureDiscountIcon
            // 
            this.pictureDiscountIcon.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureDiscountIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_discount_40;
            this.pictureDiscountIcon.Location = new System.Drawing.Point(50, 500);
            this.pictureDiscountIcon.Name = "pictureDiscountIcon";
            this.pictureDiscountIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureDiscountIcon.TabIndex = 11;
            this.pictureDiscountIcon.TabStop = false;
            // 
            // btnDiscounts
            // 
            this.btnDiscounts.BackColor = System.Drawing.Color.DarkGreen;
            this.btnDiscounts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnDiscounts.Location = new System.Drawing.Point(100, 500);
            this.btnDiscounts.Name = "btnDiscounts";
            this.btnDiscounts.Size = new System.Drawing.Size(194, 54);
            this.btnDiscounts.TabIndex = 12;
            this.btnDiscounts.Text = "Discounts";
            this.btnDiscounts.UseVisualStyleBackColor = false;
            this.btnDiscounts.Click += new System.EventHandler(this.btnDiscounts_Click_1);
            // 
            // pictureLowStockIcon
            // 
            this.pictureLowStockIcon.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureLowStockIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_notification_40;
            this.pictureLowStockIcon.Location = new System.Drawing.Point(50, 654);
            this.pictureLowStockIcon.Name = "pictureLowStockIcon";
            this.pictureLowStockIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureLowStockIcon.TabIndex = 13;
            this.pictureLowStockIcon.TabStop = false;
            // 
            // btnLowStock
            // 
            this.btnLowStock.BackColor = System.Drawing.Color.DarkGreen;
            this.btnLowStock.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnLowStock.Location = new System.Drawing.Point(100, 642);
            this.btnLowStock.Name = "btnLowStock";
            this.btnLowStock.Size = new System.Drawing.Size(194, 65);
            this.btnLowStock.TabIndex = 14;
            this.btnLowStock.Text = "Low Stock Notifications";
            this.btnLowStock.UseVisualStyleBackColor = false;
            this.btnLowStock.Click += new System.EventHandler(this.btnLowStock_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.btnLog);
            this.panelHeader.Controls.Add(this.close);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(308, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1103, 60);
            this.panelHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTitle.Location = new System.Drawing.Point(400, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(401, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome, Admin";
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.Color.Red;
            this.btnLog.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F);
            this.btnLog.ForeColor = System.Drawing.Color.White;
            this.btnLog.Location = new System.Drawing.Point(1038, 12);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(120, 37);
            this.btnLog.TabIndex = 1;
            this.btnLog.Text = "Logout";
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click_1);
            // 
            // close
            // 
            this.close.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.close.ForeColor = System.Drawing.Color.Black;
            this.close.Location = new System.Drawing.Point(1174, 15);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(100, 23);
            this.close.TabIndex = 2;
            this.close.Text = "X";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(308, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1103, 659);
            this.panelMain.TabIndex = 0;
            // 
            // AdminDashboard
            // 
            this.ClientSize = new System.Drawing.Size(1411, 719);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDashboardIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCustomersIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProductsIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOrdersIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureReportsIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDiscountIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLowStockIcon)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Button btnSupplier;
        private PictureBox pictureBox1;
    }
}