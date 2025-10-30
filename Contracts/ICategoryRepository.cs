using Entities.Models;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
       Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category?>GetCategoryByIdAsync(int id,bool trackChanges);
        Task<Category>GetCategoryByCodeAsync(string code,bool trackChanges);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);

    }
}
