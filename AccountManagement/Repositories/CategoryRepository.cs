using AccountManagement.API.Data;
using AccountManagement.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
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

        public async Task<Category> AddAsync(Category category)
        {
            category.DateCreated = DateTime.UtcNow;
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            category.DateModified = DateTime.UtcNow;
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return category;
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
