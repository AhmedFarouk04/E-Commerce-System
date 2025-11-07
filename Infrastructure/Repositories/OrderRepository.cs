using System.Collections.Generic;
using System.Linq;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FakeDbContext _context;

        public OrderRepository(FakeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll() => _context.Orders;

        public Order? GetById(int id) => _context.Orders.FirstOrDefault(o => o.Id == id);

        public void Add(Order entity) => _context.Orders.Add(entity);

        public void Update(Order entity)
        {
            var existing = GetById(entity.Id);
            if (existing == null) return;
            _context.Orders.Remove(existing);
            _context.Orders.Add(entity);
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null) _context.Orders.Remove(order);
        }

        public IEnumerable<Order> GetByCustomerId(int customerId) =>
            _context.Orders.Where(o => o.Customer.Id == customerId);

        public IEnumerable<Order> GetByStatus(string status) =>
            _context.Orders.Where(o => o.Status.ToLower() == status.ToLower());
    }
}
