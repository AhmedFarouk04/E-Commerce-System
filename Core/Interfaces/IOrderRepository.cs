using ECommerce.Core.Entities;
using System.Collections.Generic;

namespace ECommerce.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetByCustomerId(int customerId);
        IEnumerable<Order> GetByStatus(string status);
    }
}
