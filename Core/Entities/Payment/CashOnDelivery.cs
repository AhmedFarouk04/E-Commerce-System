namespace ECommerce.Core.Entities.Payment
{
    public class CashOnDelivery : IPayment
    {
        public decimal DeliveryFee { get; private set; }

        public CashOnDelivery(decimal deliveryFee = 20)
        {
            if (deliveryFee < 0)
                throw new ArgumentException("Delivery fee cannot be negative.");
            DeliveryFee = deliveryFee;
        }

        public bool ProcessPayment(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Payment amount must be positive.");

            var total = amount + DeliveryFee;
            Console.WriteLine($"Payment will be collected on delivery. Total due: {total:C}");
            return true;
        }
    }
}
