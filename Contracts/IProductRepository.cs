
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        List<Product> FindByCategoryId(int id);
    }
}
