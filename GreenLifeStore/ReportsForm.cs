using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using ClosedXML.Excel;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using GreenLifeStore.DataLayer;

namespace GreenLifeStore
{
    public partial class ReportsForm : Form
    {
        private readonly DatabaseHelper db = new DatabaseHelper();

        private PrintDocument printDocument = new PrintDocument();
        private string reportTitle = "Report";

        private PrintDocument printBillDocument = new PrintDocument();
        private string billText = "";

        public ReportsForm()
        {
            InitializeComponent();
            SetFixedSize();

            cmbReportType.Items.AddRange(new string[] 
            { 
                "Sales Report", 
                "Stock Report", 
                "Customer Order History" 
            });

            cmbReportType.SelectedIndexChanged += CmbReportType_SelectedIndexChanged;

            LoadCustomers();
            cmbCustomer.Visible = false;
            lblCustomer.Visible = false;

            printDocument.PrintPage += PrintDocument_PrintPage;
            printBillDocument.PrintPage += PrintBillDocument_PrintPage;
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            cmbReportType.SelectedIndex = 0;
        }

        private void SetFixedSize()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 650);
        }

        private void CmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool showCustomer = cmbReportType.SelectedItem.ToString() == "Customer Order History";
            cmbCustomer.Visible = showCustomer;
            lblCustomer.Visible = showCustomer;
        }

        // ================= LOAD CUSTOMERS =================
        private void LoadCustomers()
        {
            string query = "SELECT Id, Full_Name FROM [User] WHERE Role='CUSTOMER'";
            DataTable dt = db.ExecuteQuery(query);

            cmbCustomer.DisplayMember = "Full_Name";
            cmbCustomer.ValueMember = "Id";
            cmbCustomer.DataSource = dt;
        }

        // ================= LOAD REPORT =================
        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            string reportType = cmbReportType.SelectedItem?.ToString();
            if (reportType == null)
            {
                MessageBox.Show("Select a report type!");
                return;
            }

            switch (reportType)
            {
                case "Sales Report":
                    reportTitle = "Sales Report";
                    LoadSalesReport();
                    break;

                case "Stock Report":
                    reportTitle = "Stock Report";
                    LoadStockReport();
                    break;

                case "Customer Order History":
                    if (cmbCustomer.SelectedValue != null)
                    {
                        int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                        reportTitle = $"Customer Order History - {cmbCustomer.Text}";
                        LoadCustomerOrderHistory(customerId);
                    }
                    break;
            }

            UpdateTotalRevenue();
        }

        private void LoadSalesReport()
        {
            string query = @"
                SELECT o.id AS OrderID,
                       u.full_name AS CustomerName,
                       os.status_name AS Status,
                       o.order_date AS OrderDate,
                       o.total_amount AS TotalAmount
                FROM orders o
                INNER JOIN [user] u ON o.customer_id = u.id
                INNER JOIN order_status os ON o.order_status_id = os.id
                ORDER BY o.order_date DESC";

            dgvReport.DataSource = db.ExecuteQuery(query);
        }

        //STOCK REPORT 
        private void LoadStockReport()
        {
            string query = @"
                SELECT p.Name AS ProductName,
                       p.Category,
                       i.quantity_in_stock AS Stock,
                       s.supplier_name AS Supplier
                FROM Product p
                JOIN Supplier s ON p.supplier_id = s.id
                LEFT JOIN Inventory i ON p.id = i.product_id
                ORDER BY i.quantity_in_stock ASC";

            dgvReport.DataSource = db.ExecuteQuery(query);
        }

        //CUSTOMER HISTORY 
        private void LoadCustomerOrderHistory(int customerId)
        {
            string query = @"
                SELECT o.id AS OrderID,
                       o.order_date AS OrderDate,
                       os.status_name AS Status,
                       p.name AS ProductName,
                       o.quantity AS Quantity,
                       p.price AS UnitPrice
                FROM orders o
                INNER JOIN order_status os ON o.order_status_id = os.id
                INNER JOIN product p ON o.product_id = p.id
                WHERE o.customer_id = @CustomerId
                ORDER BY o.order_date DESC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@CustomerId", customerId)
            };

            dgvReport.DataSource = db.ExecuteQuery(query, parameters);

            if (dgvReport.Columns["UnitPrice"] != null)
                dgvReport.Columns["UnitPrice"].DefaultCellStyle.Format = "\"Rs\" #,##0.00";
        }

        // TOTAL REVENUE 
        private void UpdateTotalRevenue()
        {
            if (dgvReport.Rows.Count == 0)
            {
                lblTotalRevenue.Text = "";
                return;
            }

            decimal total = 0;

            if (cmbReportType.SelectedItem.ToString() == "Customer Order History")
            {
                foreach (DataGridViewRow row in dgvReport.Rows)
                {
                    if (row.IsNewRow) continue;

                    int qty = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0);
                    decimal price = Convert.ToDecimal(row.Cells["UnitPrice"].Value ?? 0);
                    total += qty * price;
                }
            }
            else
            {
                if (dgvReport.Columns.Contains("TotalAmount"))
                {
                    foreach (DataGridViewRow row in dgvReport.Rows)
                    {
                        if (row.Cells["TotalAmount"].Value != null)
                            total += Convert.ToDecimal(row.Cells["TotalAmount"].Value);
                    }
                }
            }

            lblTotalRevenue.Text = $"Total: LKR {total:N2}";
        }

        // PRINT REPORT 
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 900,
                Height = 600
            };
            preview.ShowDialog();
        }


        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = 50;
            int x = 50;

            System.Drawing.Font headerFont = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            System.Drawing.Font rowFont = new System.Drawing.Font("Segoe UI", 12);

            e.Graphics.DrawString(reportTitle, new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold), Brushes.Black, x, y);
            y += 40;

            foreach (DataGridViewColumn col in dgvReport.Columns)
            {
                e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black, x, y);
                x += col.Width;
            }

            y += 30;
            x = 50;

            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (row.IsNewRow) continue;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    e.Graphics.DrawString(cell.Value?.ToString() ?? "", rowFont, Brushes.Black, x, y);
                    x += cell.OwningColumn.Width;
                }

                x = 50;
                y += 25;
            }

            y += 20;
            e.Graphics.DrawString(lblTotalRevenue.Text, rowFont, Brushes.DarkGreen, 50, y);
        }

        private void BtnSaveBillPDF_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0 ||
                cmbReportType.SelectedItem?.ToString() != "Customer Order History")
            {
                MessageBox.Show("No customer order data to export!");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "PDF Files|*.pdf",
                FileName = $"Bill_{cmbCustomer.Text}_{DateTime.Now:yyyyMMddHHmm}.pdf"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Create document
                    var doc = new MigraDoc.DocumentObjectModel.Document();
                    var section = doc.AddSection();

                    // Header
                    var header = section.AddParagraph("GreenLife Organic Store");
                    header.Format.Font.Size = 16;
                    header.Format.Font.Bold = true;
                    header.Format.SpaceAfter = 5;

                    section.AddParagraph($"Customer: {cmbCustomer.Text}");
                    section.AddParagraph($"Date: {DateTime.Now}");
                    section.AddParagraph("\n");

                    // Create table
                    var table = section.AddTable();
                    table.Borders.Width = 0.75;

                    // Define columns (widths in points)
                    Column colProduct = table.AddColumn(Unit.FromCentimeter(6));
                    Column colQty = table.AddColumn(Unit.FromCentimeter(2));
                    Column colUnitPrice = table.AddColumn(Unit.FromCentimeter(3));
                    Column colTotal = table.AddColumn(Unit.FromCentimeter(3));

                    // Header row
                    Row row = table.AddRow();
                    row.Shading.Color = Colors.LightGray;
                    row.Cells[0].AddParagraph("Product");
                    row.Cells[1].AddParagraph("Qty");
                    row.Cells[2].AddParagraph("Unit Price");
                    row.Cells[3].AddParagraph("Total");
                    row.Format.Font.Bold = true;

                    decimal grandTotal = 0;

                    // Add data rows
                    foreach (DataGridViewRow dgvRow in dgvReport.Rows)
                    {
                        if (dgvRow.IsNewRow) continue;

                        string product = dgvRow.Cells["ProductName"].Value?.ToString() ?? "";
                        int qty = Convert.ToInt32(dgvRow.Cells["Quantity"].Value ?? 0);
                        decimal price = Convert.ToDecimal(dgvRow.Cells["UnitPrice"].Value ?? 0);
                        decimal total = qty * price;
                        grandTotal += total;

                        Row dataRow = table.AddRow();
                        dataRow.Cells[0].AddParagraph(product);
                        dataRow.Cells[1].AddParagraph(qty.ToString());
                        dataRow.Cells[2].AddParagraph(price.ToString("N2"));
                        dataRow.Cells[3].AddParagraph(total.ToString("N2"));
                    }

                    // Add Grand Total
                    var totalPara = section.AddParagraph($"\nGRAND TOTAL: {grandTotal:N2}");
                    totalPara.Format.Font.Bold = true;
                    totalPara.Format.Alignment = ParagraphAlignment.Right;

                    // Render PDF
                    var renderer = new PdfDocumentRenderer(true) { Document = doc };
                    renderer.RenderDocument();
                    renderer.PdfDocument.Save(sfd.FileName);

                    MessageBox.Show("Customer bill saved as PDF successfully!");
                }
            }
        }

        private void BtnPrintBill_Click(object sender, EventArgs e)
        {
            GenerateBillText();

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printBillDocument,
                Width = 800,
                Height = 600
            };

            preview.ShowDialog();
        }
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0) return;

            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|*.xlsx"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new ClosedXML.Excel.XLWorkbook())
                    {
                        var ws = workbook.Worksheets.Add("Report");

                        for (int i = 0; i < dgvReport.Columns.Count; i++)
                            ws.Cell(1, i + 1).Value = dgvReport.Columns[i].HeaderText;

                        for (int i = 0; i < dgvReport.Rows.Count; i++)
                            for (int j = 0; j < dgvReport.Columns.Count; j++)
                                ws.Cell(i + 2, j + 1).Value =
                                    dgvReport.Rows[i].Cells[j].Value?.ToString();

                        workbook.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Report exported successfully!");
                }
            }
        }

        // ================= GENERATE BILL =================
        private void GenerateBillText()
        {
            if (dgvReport.Rows.Count == 0) return;

            billText = "GreenLife Organic Store\n";
            billText += "----------------------------------------\n";
            billText += $"Customer: {cmbCustomer.Text}\n";
            billText += $"Date: {DateTime.Now}\n";
            billText += "----------------------------------------\n";

            // Column headings with fixed width
            billText += $"{"Product",-20} {"Qty",5} {"Unit Price",12} {"Total",12}\n";
            billText += "----------------------------------------\n";

            decimal grandTotal = 0;

            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                if (row.IsNewRow) continue;

                string product = row.Cells["ProductName"].Value?.ToString() ?? "";
                int qty = Convert.ToInt32(row.Cells["Quantity"].Value ?? 0);
                decimal price = Convert.ToDecimal(row.Cells["UnitPrice"].Value ?? 0);
                decimal total = qty * price;

                grandTotal += total;

                // Align each column properly
                billText += $"{product,-20} {qty,5} {price,12:N2} {total,12:N2}\n";
            }

            billText += "----------------------------------------\n";
            billText += $"GRAND TOTAL: {grandTotal,30:N2}\n";
        }

        private void PrintBillDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            System.Drawing.Font font = new System.Drawing.Font("Courier New", 10);
            e.Graphics.DrawString(billText, font, Brushes.Black, new PointF(50, 50));
        }
    }
}