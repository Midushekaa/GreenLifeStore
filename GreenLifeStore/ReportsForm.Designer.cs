using System;
using System.Drawing;
using System.Windows.Forms;

namespace GreenLifeStore
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvReport;
        private ComboBox cmbReportType;
        private ComboBox cmbCustomer;
        private Button btnLoadReport;
        private Button btnPrintBill;
        private Button btnExportExcel;
        private Label lblReportType;
        private Label lblCustomer;
        private Label lblTotalRevenue;
        private Panel panelHeader;
        private Label lblHeader;
        private Button btnSaveBillPDF;

        private void InitializeComponent()
        {
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.btnLoadReport = new System.Windows.Forms.Button();
            this.btnPrintBill = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.lblReportType = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.btnSaveBillPDF = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReport
            // 
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(126, 284);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.Size = new System.Drawing.Size(700, 324);
            this.dgvReport.TabIndex = 0;
            // 
            // cmbReportType
            // 
            this.cmbReportType.Location = new System.Drawing.Point(355, 126);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(200, 31);
            this.cmbReportType.TabIndex = 1;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Location = new System.Drawing.Point(355, 166);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(200, 31);
            this.cmbCustomer.TabIndex = 2;
            // 
            // btnLoadReport
            // 
            this.btnLoadReport.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnLoadReport.ForeColor = System.Drawing.Color.White;
            this.btnLoadReport.Location = new System.Drawing.Point(612, 142);
            this.btnLoadReport.Name = "btnLoadReport";
            this.btnLoadReport.Size = new System.Drawing.Size(148, 48);
            this.btnLoadReport.TabIndex = 3;
            this.btnLoadReport.Text = "Load Report";
            this.btnLoadReport.UseVisualStyleBackColor = false;
            this.btnLoadReport.Click += new System.EventHandler(this.btnLoadReport_Click);
            // 
            // btnPrintBill
            // 
            this.btnPrintBill.Location = new System.Drawing.Point(610, 639);
            this.btnPrintBill.Name = "btnPrintBill";
            this.btnPrintBill.Size = new System.Drawing.Size(179, 42);
            this.btnPrintBill.TabIndex = 9;
            this.btnPrintBill.Text = "Print Bill";
            this.btnPrintBill.UseVisualStyleBackColor = true;
            this.btnPrintBill.Click += new System.EventHandler(this.BtnPrintBill_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.Green;
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(155, 639);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(164, 42);
            this.btnExportExcel.TabIndex = 5;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // lblReportType
            // 
            this.lblReportType.Location = new System.Drawing.Point(137, 126);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(212, 24);
            this.lblReportType.TabIndex = 6;
            this.lblReportType.Text = "Select Report Type:";
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(137, 166);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(195, 24);
            this.lblCustomer.TabIndex = 7;
            this.lblCustomer.Text = "Select Customer:";
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotalRevenue.Location = new System.Drawing.Point(135, 215);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(214, 46);
            this.lblTotalRevenue.TabIndex = 8;
            this.lblTotalRevenue.Text = "Total Revenue: 0.00";
            // 
            // btnSaveBillPDF
            // 
            this.btnSaveBillPDF.BackColor = System.Drawing.Color.Red;
            this.btnSaveBillPDF.ForeColor = System.Drawing.Color.White;
            this.btnSaveBillPDF.Location = new System.Drawing.Point(384, 639);
            this.btnSaveBillPDF.Name = "btnSaveBillPDF";
            this.btnSaveBillPDF.Size = new System.Drawing.Size(164, 42);
            this.btnSaveBillPDF.TabIndex = 10;
            this.btnSaveBillPDF.Text = "Save Bill PDF";
            this.btnSaveBillPDF.UseVisualStyleBackColor = false;
            this.btnSaveBillPDF.Click += new System.EventHandler(this.BtnSaveBillPDF_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.Gray;
            this.panelHeader.Controls.Add(this.lblHeader);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(982, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(433, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(327, 51);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Reports Management";
            // 
            // ReportsForm
            // 
            this.ClientSize = new System.Drawing.Size(982, 719);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.cmbReportType);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.btnLoadReport);
            this.Controls.Add(this.btnPrintBill);
            this.Controls.Add(this.btnSaveBillPDF);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.lblReportType);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblTotalRevenue);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReportsForm";
            this.Text = "Reports Management";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
