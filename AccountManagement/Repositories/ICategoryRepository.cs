using AccountManagement.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(int id,Category category);
        Task<bool> DeleteAsync(int id);
    }
}
