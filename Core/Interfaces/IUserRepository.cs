using ECommerce.Core.Entities;
using System.Collections.Generic;

namespace ECommerce.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByEmail(string email);
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Admin> GetAllAdmins();
    }
}
