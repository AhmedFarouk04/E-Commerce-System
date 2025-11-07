using System.Collections.Generic;
using ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Data
{
    public class FakeDbContext
    {
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
        public List<Admin> Admins { get; set; } = new();
        public List<Order> Orders { get; set; } = new();

        public FakeDbContext()
        {
            var cat1 = new Category(1, "Electronics", "Devices and accessories");
            var cat2 = new Category(2, "Clothing", "Men and Women Apparel");

            Categories.AddRange(new[] { cat1, cat2 });

            Products.AddRange(new[]
            {
                new Product(1, "Laptop", "Gaming Laptop", 2500, 10, cat1),
                new Product(2, "Headphones", "Wireless Headset", 400, 25, cat1),
                new Product(3, "T-Shirt", "Cotton Shirt", 150, 40, cat2)
            });
        }
      
        public void ResetUsers()
        {
            Customers.Clear();
            Admins.Clear();
            Orders.Clear();
        }
        public void ResetAll()
        {
            Customers.Clear();
            Admins.Clear();
            Orders.Clear();
            Products.Clear();
            Categories.Clear();

            var cat1 = new Category(1, "Electronics", "Devices and accessories");
            var cat2 = new Category(2, "Clothing", "Men and Women Apparel");

            Categories.AddRange(new[] { cat1, cat2 });

            Products.AddRange(new[]
            {
        new Product(1, "Laptop", "Gaming Laptop", 2500, 10, cat1),
        new Product(2, "Headphones", "Wireless Headset", 400, 25, cat1),
        new Product(3, "T-Shirt", "Cotton Shirt", 150, 40, cat2)
    });
        }


    }
}
