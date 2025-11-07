using System.Collections.Generic;
using System.Linq;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FakeDbContext _context;

        public UserRepository(FakeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll() => _context.Customers.Cast<User>().Concat(_context.Admins);

        public User? GetById(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            var admin = _context.Admins.FirstOrDefault(a => a.Id == id);
            return (User?)customer ?? (User?)admin;
        }


        public void Add(User entity)
        {
            if (entity is Customer c) _context.Customers.Add(c);
            else if (entity is Admin a) _context.Admins.Add(a);
        }

        public void Update(User entity)
        {
            Delete(entity.Id);
            Add(entity);
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user is Customer c) _context.Customers.Remove(c);
            else if (user is Admin a) _context.Admins.Remove(a);
        }

        public User? GetByEmail(string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email);
            return (User?)customer ?? (User?)admin;
        }


        public IEnumerable<Customer> GetAllCustomers() => _context.Customers;

        public IEnumerable<Admin> GetAllAdmins() => _context.Admins;
    }
}
