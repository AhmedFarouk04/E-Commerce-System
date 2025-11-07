
namespace ECommerce.Core.Entities
{
    public class Cart
    {
        private readonly List<OrderItem> _items = new();
        public int Id { get; private set; }
        public Customer Owner { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Cart(Customer owner)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Id = new Random().Next(1000, 9999); // just for mock purposes
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            var existingItem = _items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                _items.Add(new OrderItem(product, quantity));
            }
        }

        public void RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.Product.Id == productId);
            if (item == null)
                throw new InvalidOperationException("Item not found in cart.");
            _items.Remove(item);
        }

        public decimal GetTotalPrice()
        {
            return _items.Sum(i => i.Subtotal);
        }

        public void ClearCart() => _items.Clear();

        public override string ToString() => $"{_items.Count} items - Total: {GetTotalPrice():C}";
    }
}
