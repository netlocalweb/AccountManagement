using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(int id,Category category);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByCodeAsync(string code, int? ignoreId = null);
    }
}
