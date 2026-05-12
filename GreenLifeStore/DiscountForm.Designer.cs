using System.Windows.Forms;

namespace GreenLifeStore
{
    partial class DiscountForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbProducts;
        private NumericUpDown nudDiscount;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Button btnApply;
        private DataGridView dgvDiscounts;
        private Label lblProduct, lblDiscount, lblStart, lblEnd;
        private System.Windows.Forms.Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()

        {
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.nudDiscount = new System.Windows.Forms.NumericUpDown();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnApply = new System.Windows.Forms.Button();
            this.dgvDiscounts = new System.Windows.Forms.DataGridView();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscounts)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(540, 20);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "Delete Discount";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbProducts
            // 
            this.cmbProducts.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProducts.Location = new System.Drawing.Point(469, 103);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(200, 31);
            this.cmbProducts.TabIndex = 1;
            // 
            // nudDiscount
            // 
            this.nudDiscount.DecimalPlaces = 2;
            this.nudDiscount.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDiscount.Location = new System.Drawing.Point(469, 143);
            this.nudDiscount.Name = "nudDiscount";
            this.nudDiscount.Size = new System.Drawing.Size(200, 31);
            this.nudDiscount.TabIndex = 3;
            // 
            // dtpStart
            // 
            this.dtpStart.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStart.Location = new System.Drawing.Point(469, 183);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(441, 31);
            this.dtpStart.TabIndex = 5;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnd.Location = new System.Drawing.Point(469, 223);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(445, 31);
            this.dtpEnd.TabIndex = 7;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.ForestGreen;
            this.btnApply.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(358, 292);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(156, 48);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply Discount";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dgvDiscounts
            // 
            this.dgvDiscounts.AllowUserToAddRows = false;
            this.dgvDiscounts.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiscounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiscounts.Location = new System.Drawing.Point(184, 376);
            this.dgvDiscounts.Name = "dgvDiscounts";
            this.dgvDiscounts.ReadOnly = true;
            this.dgvDiscounts.RowHeadersWidth = 51;
            this.dgvDiscounts.Size = new System.Drawing.Size(700, 234);
            this.dgvDiscounts.TabIndex = 9;
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(219, 103);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(170, 35);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product:";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.Location = new System.Drawing.Point(219, 143);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(170, 34);
            this.lblDiscount.TabIndex = 2;
            this.lblDiscount.Text = "Discount (%):";
            // 
            // lblStart
            // 
            this.lblStart.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.Location = new System.Drawing.Point(219, 183);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(170, 34);
            this.lblStart.TabIndex = 4;
            this.lblStart.Text = "Start Date:";
            // 
            // lblEnd
            // 
            this.lblEnd.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.Location = new System.Drawing.Point(219, 223);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(170, 34);
            this.lblEnd.TabIndex = 6;
            this.lblEnd.Text = "End Date:";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Gray;
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(982, 60);
            this.panelHeader.TabIndex = 10;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(422, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(308, 37);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Discount Management";
            // 
            // DiscountForm
            // 
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.cmbProducts);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.nudDiscount);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.dgvDiscounts);
            this.Name = "DiscountForm";
            this.Text = "Manage Discounts";
            this.Load += new System.EventHandler(this.DiscountForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscounts)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private Panel panelHeader;
        private Label lblHeader;
    }
}
