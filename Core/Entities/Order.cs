using ECommerce.Core.Entities.Payment;

namespace ECommerce.Core.Entities
{
    public class Order
    {
        private readonly List<OrderItem> _items = new();
        public int Id { get; private set; }
        public Customer Customer { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public decimal TotalAmount => _items.Sum(i => i.Subtotal);
        public IPayment PaymentMethod { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Order(Customer customer, IEnumerable<OrderItem> items, IPayment paymentMethod)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            if (items == null || !items.Any())
                throw new ArgumentException("Order must have at least one item.");
            PaymentMethod = paymentMethod ?? throw new ArgumentNullException(nameof(paymentMethod));

            Id = new Random().Next(10000, 99999);
            _items = items.ToList();
            Status = "Pending";
            CreatedAt = DateTime.UtcNow;
        }

        public bool ProcessPayment()
        {
            var success = PaymentMethod.ProcessPayment(TotalAmount);
            Status = success ? "Completed" : "Failed";
            return success;
        }

        public void CancelOrder()
        {
            if (Status == "Completed")
                throw new InvalidOperationException("Cannot cancel a completed order.");
            Status = "Cancelled";
        }

        public override string ToString() => $"Order #{Id} - {Status} - {TotalAmount:C}";
    }
}
