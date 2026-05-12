using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GreenLifeStore
{
    partial class CustomerDashboard
    {
        private IContainer components = null;

        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelMain;

        private Label lblTitle;

        private PictureBox pictureLogo;
        private PictureBox pictureSearchIcon;
        private PictureBox pictureCartIcon;
        private PictureBox pictureOrdersIcon;
        private PictureBox pictureProfileIcon;
        private PictureBox pictureHomeIcon;



        private Button btnHome;
        private Button btnSearchProducts;
        private Button btnCart;
        private Button btnTrackOrders;
        private Button btnProfile;
        private System.Windows.Forms.Button btnNotifications;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnReview = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.pictureSearchIcon = new System.Windows.Forms.PictureBox();
            this.btnSearchProducts = new System.Windows.Forms.Button();
            this.pictureCartIcon = new System.Windows.Forms.PictureBox();
            this.btnCart = new System.Windows.Forms.Button();
            this.pictureOrdersIcon = new System.Windows.Forms.PictureBox();
            this.btnTrackOrders = new System.Windows.Forms.Button();
            this.pictureProfileIcon = new System.Windows.Forms.PictureBox();
            this.btnProfile = new System.Windows.Forms.Button();
            this.pictureHomeIcon = new System.Windows.Forms.PictureBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.close = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNotifications = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearchIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCartIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOrdersIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProfileIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHomeIcon)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.DarkGreen;
            this.panelSidebar.Controls.Add(this.pictureBox1);
            this.panelSidebar.Controls.Add(this.btnReview);
            this.panelSidebar.Controls.Add(this.pictureLogo);
            this.panelSidebar.Controls.Add(this.pictureSearchIcon);
            this.panelSidebar.Controls.Add(this.btnSearchProducts);
            this.panelSidebar.Controls.Add(this.pictureCartIcon);
            this.panelSidebar.Controls.Add(this.btnCart);
            this.panelSidebar.Controls.Add(this.pictureOrdersIcon);
            this.panelSidebar.Controls.Add(this.btnTrackOrders);
            this.panelSidebar.Controls.Add(this.pictureProfileIcon);
            this.panelSidebar.Controls.Add(this.btnProfile);
            this.panelSidebar.Controls.Add(this.pictureHomeIcon);
            this.panelSidebar.Controls.Add(this.btnHome);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(301, 719);
            this.panelSidebar.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GreenLifeStore.Properties.Resources.icons8_review_40;
            this.pictureBox1.Location = new System.Drawing.Point(25, 449);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btnReview
            // 
            this.btnReview.BackColor = System.Drawing.Color.DarkGreen;
            this.btnReview.FlatAppearance.BorderSize = 0;
            this.btnReview.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReview.ForeColor = System.Drawing.Color.Black;
            this.btnReview.Location = new System.Drawing.Point(75, 449);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(214, 57);
            this.btnReview.TabIndex = 9;
            this.btnReview.Text = "Review";
            this.btnReview.UseVisualStyleBackColor = false;
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
            // 
            // pictureLogo
            // 
            this.pictureLogo.BackColor = System.Drawing.Color.White;
            this.pictureLogo.Image = global::GreenLifeStore.Properties.Resources.greenlife_logo11;
            this.pictureLogo.Location = new System.Drawing.Point(69, 28);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(98, 59);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 0;
            this.pictureLogo.TabStop = false;
            // 
            // pictureSearchIcon
            // 
            this.pictureSearchIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_window_search_50;
            this.pictureSearchIcon.Location = new System.Drawing.Point(21, 195);
            this.pictureSearchIcon.Name = "pictureSearchIcon";
            this.pictureSearchIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureSearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureSearchIcon.TabIndex = 1;
            this.pictureSearchIcon.TabStop = false;
            // 
            // btnSearchProducts
            // 
            this.btnSearchProducts.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSearchProducts.FlatAppearance.BorderSize = 0;
            this.btnSearchProducts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchProducts.ForeColor = System.Drawing.Color.Black;
            this.btnSearchProducts.Location = new System.Drawing.Point(71, 195);
            this.btnSearchProducts.Name = "btnSearchProducts";
            this.btnSearchProducts.Size = new System.Drawing.Size(218, 51);
            this.btnSearchProducts.TabIndex = 2;
            this.btnSearchProducts.Text = "Search Products";
            this.btnSearchProducts.UseVisualStyleBackColor = false;
            this.btnSearchProducts.Click += new System.EventHandler(this.btnSearchProducts_Click);
            // 
            // pictureCartIcon
            // 
            this.pictureCartIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_cart_40;
            this.pictureCartIcon.Location = new System.Drawing.Point(21, 273);
            this.pictureCartIcon.Name = "pictureCartIcon";
            this.pictureCartIcon.Size = new System.Drawing.Size(44, 48);
            this.pictureCartIcon.TabIndex = 3;
            this.pictureCartIcon.TabStop = false;
            // 
            // btnCart
            // 
            this.btnCart.BackColor = System.Drawing.Color.DarkGreen;
            this.btnCart.FlatAppearance.BorderSize = 0;
            this.btnCart.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCart.ForeColor = System.Drawing.Color.Black;
            this.btnCart.Location = new System.Drawing.Point(71, 273);
            this.btnCart.Name = "btnCart";
            this.btnCart.Size = new System.Drawing.Size(218, 58);
            this.btnCart.TabIndex = 4;
            this.btnCart.Text = "Cart";
            this.btnCart.UseVisualStyleBackColor = false;
            this.btnCart.Click += new System.EventHandler(this.btnCart_Click);
            // 
            // pictureOrdersIcon
            // 
            this.pictureOrdersIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_track_order_40;
            this.pictureOrdersIcon.Location = new System.Drawing.Point(21, 361);
            this.pictureOrdersIcon.Name = "pictureOrdersIcon";
            this.pictureOrdersIcon.Size = new System.Drawing.Size(44, 43);
            this.pictureOrdersIcon.TabIndex = 5;
            this.pictureOrdersIcon.TabStop = false;
            // 
            // btnTrackOrders
            // 
            this.btnTrackOrders.BackColor = System.Drawing.Color.DarkGreen;
            this.btnTrackOrders.FlatAppearance.BorderSize = 0;
            this.btnTrackOrders.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrackOrders.ForeColor = System.Drawing.Color.Black;
            this.btnTrackOrders.Location = new System.Drawing.Point(71, 361);
            this.btnTrackOrders.Name = "btnTrackOrders";
            this.btnTrackOrders.Size = new System.Drawing.Size(218, 58);
            this.btnTrackOrders.TabIndex = 6;
            this.btnTrackOrders.Text = "Track Orders";
            this.btnTrackOrders.UseVisualStyleBackColor = false;
            this.btnTrackOrders.Click += new System.EventHandler(this.btnTrackOrders_Click);
            // 
            // pictureProfileIcon
            // 
            this.pictureProfileIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_test_account_50;
            this.pictureProfileIcon.Location = new System.Drawing.Point(25, 532);
            this.pictureProfileIcon.Name = "pictureProfileIcon";
            this.pictureProfileIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureProfileIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureProfileIcon.TabIndex = 7;
            this.pictureProfileIcon.TabStop = false;
            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = System.Drawing.Color.DarkGreen;
            this.btnProfile.FlatAppearance.BorderSize = 0;
            this.btnProfile.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.ForeColor = System.Drawing.Color.Black;
            this.btnProfile.Location = new System.Drawing.Point(75, 532);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(214, 55);
            this.btnProfile.TabIndex = 8;
            this.btnProfile.Text = "Profile";
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // pictureHomeIcon
            // 
            this.pictureHomeIcon.Image = global::GreenLifeStore.Properties.Resources.icons8_home_32;
            this.pictureHomeIcon.Location = new System.Drawing.Point(21, 115);
            this.pictureHomeIcon.Name = "pictureHomeIcon";
            this.pictureHomeIcon.Size = new System.Drawing.Size(40, 40);
            this.pictureHomeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureHomeIcon.TabIndex = 11;
            this.pictureHomeIcon.TabStop = false;
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.DarkGreen;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F);
            this.btnHome.ForeColor = System.Drawing.Color.Black;
            this.btnHome.Location = new System.Drawing.Point(71, 115);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(218, 51);
            this.btnHome.TabIndex = 12;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.close);
            this.panelHeader.Controls.Add(this.btnLog);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.btnNotifications);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(301, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1110, 60);
            this.panelHeader.TabIndex = 1;
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.close.ForeColor = System.Drawing.Color.Black;
            this.close.Location = new System.Drawing.Point(1194, 14);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(25, 28);
            this.close.TabIndex = 12;
            this.close.Text = "X";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.Color.Red;
            this.btnLog.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLog.ForeColor = System.Drawing.Color.White;
            this.btnLog.Location = new System.Drawing.Point(1049, 11);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(120, 37);
            this.btnLog.TabIndex = 3;
            this.btnLog.Text = "Logout";
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTitle.Location = new System.Drawing.Point(350, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(274, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome, Customer";
            // 
            // btnNotifications
            // 
            this.btnNotifications.BackColor = System.Drawing.Color.Orange;
            this.btnNotifications.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotifications.ForeColor = System.Drawing.Color.White;
            this.btnNotifications.Location = new System.Drawing.Point(828, 12);
            this.btnNotifications.Name = "btnNotifications";
            this.btnNotifications.Size = new System.Drawing.Size(215, 38);
            this.btnNotifications.TabIndex = 1;
            this.btnNotifications.Text = "Notifications";
            this.btnNotifications.UseVisualStyleBackColor = false;
            this.btnNotifications.Click += new System.EventHandler(this.btnNotifications_Click);
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelMain.Location = new System.Drawing.Point(301, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1107, 659);
            this.panelMain.TabIndex = 0;
            // 
            // CustomerDashboard
            // 
            this.ClientSize = new System.Drawing.Size(1411, 719);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSearchIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCartIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOrdersIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProfileIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHomeIcon)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private Button btnReview;
        private PictureBox pictureBox1;
        private Button btnLog;
        private Label close;
    }
}
