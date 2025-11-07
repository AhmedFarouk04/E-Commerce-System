
namespace ECommerce.Core.Entities
{
    public class Category
    {
        private readonly List<Product> _products = new();
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public Category(int id, string name, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.");

            Id = id;
            Name = name;
            Description = description;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            _products.Add(product);
        }

        public override string ToString() => $"{Name} ({_products.Count} items)";
    }
}
