using System.Collections.Generic;
using System.Linq;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FakeDbContext _context;

        public ProductRepository(FakeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll() => _context.Products;

        public Product? GetById(int id) => _context.Products.FirstOrDefault(p => p.Id == id);

        public void Add(Product entity) => _context.Products.Add(entity);

        public void Update(Product entity)
        {
            var existing = GetById(entity.Id);
            if (existing == null) return;
            _context.Products.Remove(existing);
            _context.Products.Add(entity);
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null) _context.Products.Remove(item);
        }

        public IEnumerable<Product> GetByCategoryId(int categoryId) =>
            _context.Products.Where(p => p.Category.Id == categoryId);

        public IEnumerable<Product> SearchByName(string keyword) =>
            _context.Products.Where(p => p.Name.ToLower().Contains(keyword.ToLower()));
    }
}
