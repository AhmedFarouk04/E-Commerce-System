using ECommerce.Core.Entities;
using System.Collections.Generic;

namespace ECommerce.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByCategoryId(int categoryId);
        IEnumerable<Product> SearchByName(string keyword);
    }
}
