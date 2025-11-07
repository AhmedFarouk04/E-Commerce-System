using ECommerce.Core.Entities.Payment;

namespace ECommerce.Services
{
    public class PaymentService
    {
        public IPayment CreatePayment(string type, params string[] data)
        {
            type = type.ToLower();
            if (type == "creditcard")
            {
                if (data.Length < 3)
                    throw new ArgumentException("Missing credit card data.");
                return new CreditCardPayment(data[0], data[1], data[2]);
            }

            if (type == "cash")
                return new CashOnDelivery();

            throw new ArgumentException("Unsupported payment type.");
        }
    }
}
