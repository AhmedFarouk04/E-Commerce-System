namespace ECommerce.Core.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public Category Category { get; private set; }

        public Product(int id, string name, string description, decimal price, int stockQuantity, Category category)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");
            if (stockQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(stockQuantity), "Stock quantity cannot be negative.");
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            Category = category;
        }

        public void ReduceStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            if (amount > StockQuantity)
                throw new InvalidOperationException("Not enough stock available.");
            StockQuantity -= amount;
        }

        public void IncreaseStock(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            StockQuantity += amount;
        }

        public override string ToString() => $"{Name} - {Price:C} ({StockQuantity} in stock)";
    }
}
