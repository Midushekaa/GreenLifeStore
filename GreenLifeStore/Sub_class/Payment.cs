using System;

namespace GreenLifeStore.Sub_class
{
    public enum PaymentType { Cash, Card, Online }
    public enum PaymentStatus { Pending, Completed, Failed }

    public class Payment
    {
        public decimal Amount { get; private set; }
        public PaymentType Type { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string TransactionRef { get; private set; }
        public DateTime PaymentDate { get; private set; }

        // Constructor
        public Payment(decimal amount, PaymentType type)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");

            Amount = amount;
            Type = type;
            Status = PaymentStatus.Pending;
            PaymentDate = DateTime.Now;
            TransactionRef = GenerateTransactionRef();
        }

        // Generate unique transaction reference
        private string GenerateTransactionRef()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
        }

        // Complete payment
        public void CompletePayment()
        {
            Status = PaymentStatus.Completed;
            PaymentDate = DateTime.Now;
        }

        // Fail payment
        public void FailPayment()
        {
            Status = PaymentStatus.Failed;
            PaymentDate = DateTime.Now;
        }

        // Validate card details (only for Card payments)
        public bool ValidateCardDetails(string cardNumber, string cardHolder, string expiry, string cvv, out string errorMessage)
        {
            errorMessage = "";

            if (Type != PaymentType.Card) return true;

            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(cardHolder) ||
                string.IsNullOrWhiteSpace(expiry) || string.IsNullOrWhiteSpace(cvv))
            {
                errorMessage = "Please fill all card details.";
                return false;
            }

            if (cardNumber.Length != 16 || !long.TryParse(cardNumber, out _))
            {
                errorMessage = "Card number must be 16 digits.";
                return false;
            }

            if (cvv.Length != 3 || !int.TryParse(cvv, out _))
            {
                errorMessage = "CVV must be 3 digits.";
                return false;
            }

            return true;
        }
    }
}