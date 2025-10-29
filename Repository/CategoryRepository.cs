using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RepositoryContext context;

        public CategoryRepository(RepositoryContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<bool> ExistsByCodeAsync(string code, int? ignoreId = null)
        {
            code = code.ToUpper();
            if (ignoreId.HasValue)
                return await context.Categories.AnyAsync(c => c.Code == code && c.Id != ignoreId.Value);
            else
                return await context.Categories.AnyAsync(c => c.Code == code);
        }

        public async Task<Category> AddAsync(Category category)
        {
            category.Code = category.Code.ToUpper();
            category.DateCreated = DateTime.UtcNow;

            if (await ExistsByCodeAsync(category.Code))
                throw new Exception("Category code must be unique.");

            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            var existing = await context.Categories.FindAsync(id);
            if (existing == null) 
                throw new Exception("Category not found.");

            category.Code = category.Code.ToUpper();

            if (await ExistsByCodeAsync(category.Code, id))
                throw new Exception("Category code must be unique.");

            existing.Code = category.Code;
            existing.Description = category.Description;
            existing.DateModified = DateTime.UtcNow;

            context.Categories.Update(existing);
            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null) 
                return false;

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
