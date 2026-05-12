namespace GreenLifeStore
{
    partial class ReviewForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.NumericUpDown nudRating;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnSubmitReview;
        private System.Windows.Forms.DataGridView dgvReviews;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblComment;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.nudRating = new System.Windows.Forms.NumericUpDown();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnSubmitReview = new System.Windows.Forms.Button();
            this.dgvReviews = new System.Windows.Forms.DataGridView();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnDeleteReview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbProducts
            // 
            this.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProducts.Location = new System.Drawing.Point(445, 82);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(250, 35);
            this.cmbProducts.TabIndex = 1;
            this.cmbProducts.SelectedIndexChanged += new System.EventHandler(this.cmbProducts_SelectedIndexChanged);
            // 
            // nudRating
            // 
            this.nudRating.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRating.Location = new System.Drawing.Point(445, 135);
            this.nudRating.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRating.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRating.Name = "nudRating";
            this.nudRating.Size = new System.Drawing.Size(50, 34);
            this.nudRating.TabIndex = 3;
            this.nudRating.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtComment
            // 
            this.txtComment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComment.Location = new System.Drawing.Point(445, 194);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(250, 60);
            this.txtComment.TabIndex = 5;
            // 
            // btnSubmitReview
            // 
            this.btnSubmitReview.BackColor = System.Drawing.Color.Blue;
            this.btnSubmitReview.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitReview.ForeColor = System.Drawing.Color.White;
            this.btnSubmitReview.Location = new System.Drawing.Point(228, 279);
            this.btnSubmitReview.Name = "btnSubmitReview";
            this.btnSubmitReview.Size = new System.Drawing.Size(256, 44);
            this.btnSubmitReview.TabIndex = 6;
            this.btnSubmitReview.Text = "Submit Review";
            this.btnSubmitReview.UseVisualStyleBackColor = false;
            this.btnSubmitReview.Click += new System.EventHandler(this.btnSubmitReview_Click);
            // 
            // dgvReviews
            // 
            this.dgvReviews.AllowUserToAddRows = false;
            this.dgvReviews.AllowUserToDeleteRows = false;
            this.dgvReviews.BackgroundColor = System.Drawing.Color.White;
            this.dgvReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReviews.Location = new System.Drawing.Point(124, 345);
            this.dgvReviews.Name = "dgvReviews";
            this.dgvReviews.ReadOnly = true;
            this.dgvReviews.RowHeadersWidth = 51;
            this.dgvReviews.Size = new System.Drawing.Size(689, 190);
            this.dgvReviews.TabIndex = 0;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(244, 90);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(94, 23);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product:";
            // 
            // lblRating
            // 
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRating.Location = new System.Drawing.Point(244, 143);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(79, 23);
            this.lblRating.TabIndex = 2;
            this.lblRating.Text = "Rating:";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.Location = new System.Drawing.Point(244, 202);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(110, 23);
            this.lblComment.TabIndex = 4;
            this.lblComment.Text = "Comment:";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Gray;
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1337, 60);
            this.panelHeader.TabIndex = 8;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(494, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(327, 51);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Review Management";
            // 
            // btnDeleteReview
            // 
            this.btnDeleteReview.BackColor = System.Drawing.Color.Red;
            this.btnDeleteReview.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteReview.ForeColor = System.Drawing.Color.White;
            this.btnDeleteReview.Location = new System.Drawing.Point(501, 279);
            this.btnDeleteReview.Name = "btnDeleteReview";
            this.btnDeleteReview.Size = new System.Drawing.Size(256, 44);
            this.btnDeleteReview.TabIndex = 9;
            this.btnDeleteReview.Text = "Delete Review";
            this.btnDeleteReview.UseVisualStyleBackColor = false;
            this.btnDeleteReview.Click += new System.EventHandler(this.btnDeleteReview_Click);
            // 
            // ReviewForm
            // 
            this.ClientSize = new System.Drawing.Size(1337, 653);
            this.Controls.Add(this.btnDeleteReview);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.cmbProducts);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.nudRating);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnSubmitReview);
            this.Controls.Add(this.dgvReviews);
            this.Name = "ReviewForm";
            this.Text = "Product Reviews";
            this.Load += new System.EventHandler(this.ReviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReviews)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnDeleteReview;
    }
}
