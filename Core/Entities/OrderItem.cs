namespace ECommerce.Core.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Subtotal => Product.Price * Quantity;

        public OrderItem(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Id = new Random().Next(1000, 9999);
            Product = product;
            Quantity = quantity;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            if (amount > Quantity)
                throw new InvalidOperationException("Cannot decrease more than current quantity.");
            Quantity -= amount;
        }

        public override string ToString() => $"{Product.Name} x{Quantity} = {Subtotal:C}";
    }
}
