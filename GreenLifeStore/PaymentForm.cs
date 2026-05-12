using System;
using System.Windows.Forms;
using GreenLifeStore.Sub_class;

namespace GreenLifeStore
{
    public partial class PaymentForm : Form
    {
        private Payment currentPayment;

        public PaymentForm(decimal amount)
        {
            InitializeComponent();
            currentPayment = new Payment(amount, PaymentType.Cash);
            txtAmount.Text = amount.ToString("0.00");
            txtAmount.ReadOnly = true;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            rdoCash.Checked = true;
            grpCardDetails.Visible = false;
        }

        private void rdoCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCash.Checked)
            {
                currentPayment = new Payment(currentPayment.Amount, PaymentType.Cash);
                grpCardDetails.Visible = false;
            }
        }

        private void rdoCard_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCard.Checked)
            {
                currentPayment = new Payment(currentPayment.Amount, PaymentType.Card);
                grpCardDetails.Visible = true;
            }
            else
            {
                grpCardDetails.Visible = false;
            }
        }


        private void btnPay_Click_1(object sender, EventArgs e)
        {
           
            if (currentPayment.Type == PaymentType.Card)
            {
                if (!currentPayment.ValidateCardDetails(txtCardNumber.Text, txtCardHolder.Text, txtExpiry.Text, txtCVV.Text, out string error))
                {
                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            currentPayment.CompletePayment();

            MessageBox.Show(
                $"Payment Successful!\nMethod: {currentPayment.Type}\nAmount: Rs {currentPayment.Amount:0.00}\nRef: {currentPayment.TransactionRef}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}