using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
       Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category?>GetCategoryByIdAsync(int id,bool trackChanges);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);

    }
}
