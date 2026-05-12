namespace GreenLifeStore
{
    partial class PaymentForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpPaymentMethod;
        private System.Windows.Forms.RadioButton rdoCash;
        private System.Windows.Forms.RadioButton rdoCard;

        private System.Windows.Forms.GroupBox grpCardDetails;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.Label lblCardHolder;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Label lblCVV;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtCardHolder;
        private System.Windows.Forms.TextBox txtExpiry;
        private System.Windows.Forms.TextBox txtCVV;
        private System.Windows.Forms.TextBox txtAmount;

        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpPaymentMethod = new System.Windows.Forms.GroupBox();
            this.rdoCash = new System.Windows.Forms.RadioButton();
            this.rdoCard = new System.Windows.Forms.RadioButton();
            this.grpCardDetails = new System.Windows.Forms.GroupBox();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.lblCardHolder = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.lblCVV = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.txtCardHolder = new System.Windows.Forms.TextBox();
            this.txtExpiry = new System.Windows.Forms.TextBox();
            this.txtCVV = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnPay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpPaymentMethod.SuspendLayout();
            this.grpCardDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPaymentMethod
            // 
            this.grpPaymentMethod.Controls.Add(this.rdoCash);
            this.grpPaymentMethod.Controls.Add(this.rdoCard);
            this.grpPaymentMethod.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPaymentMethod.Location = new System.Drawing.Point(97, 65);
            this.grpPaymentMethod.Name = "grpPaymentMethod";
            this.grpPaymentMethod.Size = new System.Drawing.Size(535, 108);
            this.grpPaymentMethod.TabIndex = 0;
            this.grpPaymentMethod.TabStop = false;
            this.grpPaymentMethod.Text = "Select Payment Method";
            // 
            // rdoCash
            // 
            this.rdoCash.Location = new System.Drawing.Point(20, 30);
            this.rdoCash.Name = "rdoCash";
            this.rdoCash.Size = new System.Drawing.Size(84, 38);
            this.rdoCash.TabIndex = 0;
            this.rdoCash.Text = "Cash";
            this.rdoCash.CheckedChanged += new System.EventHandler(this.rdoCash_CheckedChanged);
            // 
            // rdoCard
            // 
            this.rdoCard.Location = new System.Drawing.Point(279, 30);
            this.rdoCard.Name = "rdoCard";
            this.rdoCard.Size = new System.Drawing.Size(220, 52);
            this.rdoCard.TabIndex = 1;
            this.rdoCard.Text = "Credit / Debit Card";
            this.rdoCard.CheckedChanged += new System.EventHandler(this.rdoCard_CheckedChanged);
            // 
            // grpCardDetails
            // 
            this.grpCardDetails.Controls.Add(this.lblCardNumber);
            this.grpCardDetails.Controls.Add(this.lblCardHolder);
            this.grpCardDetails.Controls.Add(this.lblExpiry);
            this.grpCardDetails.Controls.Add(this.lblCVV);
            this.grpCardDetails.Controls.Add(this.lblAmount);
            this.grpCardDetails.Controls.Add(this.txtCardNumber);
            this.grpCardDetails.Controls.Add(this.txtCardHolder);
            this.grpCardDetails.Controls.Add(this.txtExpiry);
            this.grpCardDetails.Controls.Add(this.txtCVV);
            this.grpCardDetails.Controls.Add(this.txtAmount);
            this.grpCardDetails.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCardDetails.Location = new System.Drawing.Point(97, 179);
            this.grpCardDetails.Name = "grpCardDetails";
            this.grpCardDetails.Size = new System.Drawing.Size(535, 240);
            this.grpCardDetails.TabIndex = 1;
            this.grpCardDetails.TabStop = false;
            this.grpCardDetails.Text = "Card Details";
            this.grpCardDetails.Visible = false;
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.Location = new System.Drawing.Point(20, 30);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(179, 31);
            this.lblCardNumber.TabIndex = 0;
            this.lblCardNumber.Text = "Card Number:";
            // 
            // lblCardHolder
            // 
            this.lblCardHolder.Location = new System.Drawing.Point(20, 60);
            this.lblCardHolder.Name = "lblCardHolder";
            this.lblCardHolder.Size = new System.Drawing.Size(137, 31);
            this.lblCardHolder.TabIndex = 1;
            this.lblCardHolder.Text = "Card Holder:";
            // 
            // lblExpiry
            // 
            this.lblExpiry.Location = new System.Drawing.Point(20, 90);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(164, 31);
            this.lblExpiry.TabIndex = 2;
            this.lblExpiry.Text = "Expiry (MM/YY):";
            // 
            // lblCVV
            // 
            this.lblCVV.Location = new System.Drawing.Point(20, 120);
            this.lblCVV.Name = "lblCVV";
            this.lblCVV.Size = new System.Drawing.Size(100, 23);
            this.lblCVV.TabIndex = 3;
            this.lblCVV.Text = "CVV:";
            // 
            // lblAmount
            // 
            this.lblAmount.Location = new System.Drawing.Point(20, 150);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(100, 23);
            this.lblAmount.TabIndex = 4;
            this.lblAmount.Text = "Amount:";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(205, 30);
            this.txtCardNumber.Multiline = true;
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(209, 22);
            this.txtCardNumber.TabIndex = 5;
            // 
            // txtCardHolder
            // 
            this.txtCardHolder.Location = new System.Drawing.Point(205, 60);
            this.txtCardHolder.Name = "txtCardHolder";
            this.txtCardHolder.Size = new System.Drawing.Size(200, 31);
            this.txtCardHolder.TabIndex = 6;
            // 
            // txtExpiry
            // 
            this.txtExpiry.Location = new System.Drawing.Point(205, 90);
            this.txtExpiry.Name = "txtExpiry";
            this.txtExpiry.Size = new System.Drawing.Size(100, 31);
            this.txtExpiry.TabIndex = 7;
            // 
            // txtCVV
            // 
            this.txtCVV.Location = new System.Drawing.Point(205, 127);
            this.txtCVV.MaxLength = 3;
            this.txtCVV.Name = "txtCVV";
            this.txtCVV.PasswordChar = '*';
            this.txtCVV.Size = new System.Drawing.Size(80, 31);
            this.txtCVV.TabIndex = 8;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(205, 164);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(120, 31);
            this.txtAmount.TabIndex = 9;
            // 
            // btnPay
            // 
            this.btnPay.BackColor = System.Drawing.Color.Blue;
            this.btnPay.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.ForeColor = System.Drawing.Color.White;
            this.btnPay.Location = new System.Drawing.Point(182, 442);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(152, 44);
            this.btnPay.TabIndex = 2;
            this.btnPay.Text = "Pay Now";
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(376, 442);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 44);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // PaymentForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(680, 552);
            this.Controls.Add(this.grpPaymentMethod);
            this.Controls.Add(this.grpCardDetails);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnCancel);
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Gateway";
            this.grpPaymentMethod.ResumeLayout(false);
            this.grpCardDetails.ResumeLayout(false);
            this.grpCardDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TextBox txtCardNumber;
    }
}