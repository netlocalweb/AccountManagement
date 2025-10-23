using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Products>
    {
        Task<IEnumerable<Products>> GetAllProductsAsync(bool trackchanges);
        Task<Products?>GetProductsByIdAsync(int id,bool trackchanges);
        


    }
}
