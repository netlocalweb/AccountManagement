using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal sealed class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
    await FindAll(trackChanges)
        .OrderBy(c => c.Code)
        .ToListAsync();

        public async Task<Category?> GetCategoryByIdAsync(int id, bool trackChanges) =>
    await FindByCondition(c => c.Id.Equals(id), trackChanges)
    .SingleOrDefaultAsync();

        public void CreateCategory(Category category) => Create(category);

        public void DeleteCategory(Category category) => Delete(category);

        public void UpdateCategory(Category category) => Update(category);

        public async Task<Category> GetCategoryByCodeAsync(string code, bool trackChanges)
        {
            return await FindByCondition(c => c.Code.ToUpper() == code.ToUpper(),trackChanges)
              .FirstOrDefaultAsync();
        }
    }
}
