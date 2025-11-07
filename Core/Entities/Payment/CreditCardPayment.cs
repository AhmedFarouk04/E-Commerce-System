using System.Text.RegularExpressions;

namespace ECommerce.Core.Entities.Payment
{
    public class CreditCardPayment : IPayment
    {
        private readonly string _cardNumber;
        private readonly string _expiryDate;
        private readonly string _cvv;

        public CreditCardPayment(string cardNumber, string expiryDate, string cvv)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || !Regex.IsMatch(cardNumber, @"^\d{16}$"))
                throw new ArgumentException("Invalid card number.");
            if (string.IsNullOrWhiteSpace(expiryDate))
                throw new ArgumentException("Expiry date cannot be empty.");
            if (string.IsNullOrWhiteSpace(cvv) || cvv.Length != 3)
                throw new ArgumentException("Invalid CVV.");

            _cardNumber = cardNumber;
            _expiryDate = expiryDate;
            _cvv = cvv;
        }

        public bool ProcessPayment(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero.");

            Console.WriteLine($"Processing credit card payment of {amount:C}...");
            // 🔒 Simulate secure transaction (in real world: integrate payment gateway)
            return true;
        }
    }
}
