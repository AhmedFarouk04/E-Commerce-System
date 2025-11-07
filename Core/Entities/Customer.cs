
namespace ECommerce.Core.Entities
{
    public class Customer : User
    {
        public Cart Cart { get; private set; }
        private readonly List<Order> _orders = new();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        public Customer(int id, string name, string email, string password)
            : base(id, name, email, password)
        {
            Cart = new Cart(this);
        }

        public void PlaceOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _orders.Add(order);
        }

        public override string GetRole() => "Customer";
    }
}
