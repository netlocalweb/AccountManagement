using AccountManagement.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
